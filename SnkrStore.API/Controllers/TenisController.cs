using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/[controller]")]
public class TenisController : ControllerBase
{
    private readonly AppDbContext _context;
    public TenisController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var tenis = await _context.Tenis.ToListAsync();
        return Ok(tenis);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tenis>> Buscar(int id)
    {
        var buscar = await _context.Tenis.FirstOrDefaultAsync(b => b.Id == id);
        if (buscar == null)
            return NotFound();
        return Ok(buscar);
    }

    [HttpPost]
    public async Task<ActionResult<Tenis>> Criar([FromBody] Tenis tenis)
    {
        _context.Tenis.Add(tenis);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Listar), new { id = tenis.Id }, tenis);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] Tenis tenis)
    {
        var tenisAtualizar = await _context.Tenis.FirstOrDefaultAsync(t => t.Id == id);
        if (tenisAtualizar == null)
            return NotFound();
        tenisAtualizar.Modelo = tenis.Modelo;
        tenisAtualizar.Preco = tenis.Preco;
        tenisAtualizar.Tamanho = tenis.Tamanho;
        tenisAtualizar.Cor = tenis.Cor;
        tenisAtualizar.Estoque = tenis.Estoque;
        tenisAtualizar.MarcaId = tenis.MarcaId;
        await _context.SaveChangesAsync();
        return Ok(tenisAtualizar);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar (int id)
    {
        var tenisDeletar = await _context.Tenis.FirstOrDefaultAsync(d => d.Id == id);
        if (tenisDeletar == null)
            return NotFound();
        _context.Tenis.Remove(tenisDeletar);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}