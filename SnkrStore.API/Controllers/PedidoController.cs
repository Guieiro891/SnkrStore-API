using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly AppDbContext _context;

    public PedidoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var listaAtual = await _context.Pedidos.ToListAsync();
        return Ok(listaAtual);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Pedido>> Buscar(int id)
    {
        var busca = await _context.Pedidos.FirstOrDefaultAsync(p => p.Id == id);
        if (busca == null)
            return NotFound();
        return Ok(busca);
    }

    [HttpGet("cliente/{clienteId}")]
    public async Task<IActionResult> ListarPorCliente(int clienteId)
    {
        var pedidos = await _context.Pedidos
            .Where(p => p.ClienteId == clienteId)
            .ToListAsync();
        return Ok(pedidos);
    }

    [HttpPost]
    public async Task<ActionResult<Pedido>> Criar([FromBody] Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Listar), new { id = pedido.Id }, pedido);
    }

    [HttpPost("{id}/finalizar")]
    public async Task<ActionResult<Pedido>> Finalizar(int id)
    {
        var pedidoFinalizado = await _context.Pedidos.FirstOrDefaultAsync(p => p.Id == id);
        if (pedidoFinalizado == null)
            return NotFound();

        var listaItens = await _context.ItensPedidos.Where(i => i.PedidoId == id).ToListAsync();
        if (!listaItens.Any())
            return BadRequest("O pedido não possui itens");

        var subTotal = listaItens.Sum(item => item.PrecoPago * item.Quantidade);
        var valorTotal = (subTotal - pedidoFinalizado.Desconto) + pedidoFinalizado.Frete;
        pedidoFinalizado.ValorTotal = valorTotal;
        pedidoFinalizado.Status = StatusPedido.Pago;

        await _context.SaveChangesAsync();
        return Ok(pedidoFinalizado);
    }

    [HttpPost("{id}/cancelar")]
    public async Task<IActionResult> Cancelar(int id)
    {
        var pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.Id == id);
        if (pedido == null)
            return NotFound();

        if (pedido.Status == StatusPedido.Cancelado)
            return BadRequest("Este pedido já foi cancelado");

        // Devolve o estoque de cada item
        var itens = await _context.ItensPedidos.Where(i => i.PedidoId == id).ToListAsync();
        foreach (var item in itens)
        {
            var tenis = await _context.Tenis.FirstOrDefaultAsync(t => t.Id == item.TenisId);
            if (tenis != null)
                tenis.Estoque += item.Quantidade;
        }

        pedido.Status = StatusPedido.Cancelado;
        await _context.SaveChangesAsync();

        return Ok(pedido);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] Pedido pedido)
    {
        var pedidoAtualizado = await _context.Pedidos.FirstOrDefaultAsync(p => p.Id == id);
        if (pedidoAtualizado == null)
            return NotFound();

        pedidoAtualizado.ClienteId = pedido.ClienteId;
        pedidoAtualizado.ComprovanteFiscal = pedido.ComprovanteFiscal;
        pedidoAtualizado.Data = pedido.Data;
        pedidoAtualizado.Desconto = pedido.Desconto;
        pedidoAtualizado.Entrega = pedido.Entrega;
        pedidoAtualizado.Frete = pedido.Frete;
        pedidoAtualizado.Status = pedido.Status;
        pedidoAtualizado.Cupom = pedido.Cupom;

        await _context.SaveChangesAsync();
        return Ok(pedidoAtualizado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var pedidoDeletado = await _context.Pedidos.FirstOrDefaultAsync(p => p.Id == id);
        if (pedidoDeletado == null)
            return NotFound();
        _context.Pedidos.Remove(pedidoDeletado);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}