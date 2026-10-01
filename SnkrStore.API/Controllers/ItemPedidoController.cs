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
        _context.ItensPedidos.Remove(itemDeletar);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}