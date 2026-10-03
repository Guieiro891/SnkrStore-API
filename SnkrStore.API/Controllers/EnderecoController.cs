using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]

public class EnderecoController : ControllerBase
{
    private readonly AppDbContext _context;
    public EnderecoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var enderecoLista = await _context.Enderecos.ToListAsync();
        return Ok(enderecoLista);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Endereco>> Buscar(int id)
    {
        var busca = await _context.Enderecos.FirstOrDefaultAsync(e => e.Id == id);
        if (busca == null)
            return NotFound();
        return Ok(busca);
    }

    [HttpPost]
    public async Task<ActionResult<Endereco>> Criar([FromBody] Endereco endereco)
    {
        _context.Enderecos.Add(endereco);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Listar), new { id = endereco.Id }, endereco);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] Endereco endereco)
    {
        var enderecoAtualizar = await _context.Enderecos.FirstOrDefaultAsync(e => e.Id == id);
        if (enderecoAtualizar == null)
            return NotFound();
        enderecoAtualizar.ClienteId = endereco.ClienteId;
        enderecoAtualizar.Rua = endereco.Rua;
        enderecoAtualizar.Numero = endereco.Numero;
        enderecoAtualizar.Cidade = endereco.Cidade;
        enderecoAtualizar.Estado = endereco.Estado;
        enderecoAtualizar.CEP = endereco.CEP;

        await _context.SaveChangesAsync();
        return Ok(enderecoAtualizar);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar (int id)
    {
        var enderecoDelete = await _context.Enderecos.FirstOrDefaultAsync(d => d.Id == id);
        if (enderecoDelete == null)
            return NotFound();
        _context.Enderecos.Remove(enderecoDelete);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}