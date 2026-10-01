using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]

public class PedidoController : ControllerBase
{
    private readonly AppDbContext _context;
    public PedidoController (AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var listaAtual = await _context.Pedidos.ToListAsync();
        return Ok(listaAtual);
    }

    [HttpPost]
    public async Task<ActionResult<Pedido>> Criar([FromBody] Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Listar), new { id = pedido.Id }, pedido);
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
        pedidoAtualizado.ValorTotal = pedido.ValorTotal;
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