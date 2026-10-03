using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]

public class ItemPedidoController : ControllerBase
{
    private readonly AppDbContext _context;
    public ItemPedidoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var item = await _context.ItensPedidos.ToListAsync();
        return Ok(item);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemPedido>> Buscar(int id)
    {
        var busca = await _context.ItensPedidos.FirstOrDefaultAsync(i => i.Id == id);
        if (busca == null)
            return NotFound();
        return Ok(busca);
    }

    [HttpPost]
    public async Task<ActionResult<ItemPedido>> Criar([FromBody]  ItemPedido itemPedido)
    {
        var itemP = await _context.Tenis.FirstOrDefaultAsync(t => t.Id == itemPedido.TenisId);
        if (itemP == null)
            return NotFound ("Tenis não encontrado");

        if (itemP.Estoque < itemPedido.Quantidade)
            return BadRequest("Estoque insuficiente");

        itemP.Estoque -= itemPedido.Quantidade;
        _context.ItensPedidos.Add(itemPedido);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Listar), new { id = itemPedido.Id }, itemPedido);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] ItemPedido itemPedido)
    {
        var item = await _context.ItensPedidos.FirstOrDefaultAsync(i => i.Id == id);
        if (item == null)
            return NotFound();
        var tenis = await _context.Tenis.FirstOrDefaultAsync(t => t.Id == item.TenisId);
        if (tenis == null)
            return NotFound();
        var quantidadeAntiga = item.Quantidade;
        item.Quantidade = itemPedido.Quantidade;
        var diferenca = itemPedido.Quantidade - quantidadeAntiga;
        if (diferenca > 0 && tenis.Estoque < diferenca)
            return BadRequest("Estoque insuficiente para alteracao");
        tenis.Estoque -= diferenca;

        item.PedidoId = itemPedido.PedidoId;
        item.PrecoPago = itemPedido.PrecoPago;
        item.Quantidade = itemPedido.Quantidade;
        item.TenisId = itemPedido.TenisId;

        await _context.SaveChangesAsync();
        return Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var itemDeletar = await _context.ItensPedidos.FirstOrDefaultAsync(i => i.Id == id);
        if (itemDeletar == null)
            return NotFound();
        var tenisDevolver = await _context.Tenis.FirstOrDefaultAsync(t => t.Id == itemDeletar.TenisId);
        if (tenisDevolver == null)
            return NotFound();
        tenisDevolver.Estoque += itemDeletar.Quantidade;
        _context.ItensPedidos.Remove(itemDeletar);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}