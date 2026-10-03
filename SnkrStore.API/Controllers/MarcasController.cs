using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]

public class MarcasController : ControllerBase
{
    private readonly AppDbContext _context;

    public MarcasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task <IActionResult> Listar()
    {
        var marcas =  await _context.Marcas.ToListAsync();
        return Ok(marcas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Marca>>Busca(int id)
    {
        var marca = await _context.Marcas.FirstOrDefaultAsync(busca => busca.Id == id);
        if (marca == null)
            return NotFound();
        
        return Ok(marca);
    }

    [HttpPost]
    public async Task<ActionResult<Marca>> Criar([FromBody] Marca marca)
    {
        _context.Marcas.Add(marca);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Listar), new { id = marca.Id }, marca);
    }

    [HttpPut("{id}")]
    public  async Task<IActionResult> Atualizar(int id, [FromBody] Marca marcaAtualizada)
    {
        var marcaExistente = await _context.Marcas.FirstOrDefaultAsync(m => m.Id == id);
        if (marcaExistente == null)
            return NotFound();
        marcaExistente.Nome = marcaAtualizada.Nome;
        marcaExistente.Disponivel = marcaAtualizada.Disponivel;
        await _context.SaveChangesAsync();
        return Ok(marcaExistente);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar (int id)
    {
        var marcaDeletar = await _context.Marcas.FirstOrDefaultAsync(d => d.Id == id);
        if (marcaDeletar == null)
            return NotFound();
        _context.Marcas.Remove(marcaDeletar);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}