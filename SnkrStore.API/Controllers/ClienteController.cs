using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]

public class ClienteController : ControllerBase
{
    private readonly AppDbContext _context;
    public  ClienteController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var cliente = await _context.Clientes.ToListAsync();
        return Ok(cliente);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> Buscar(int id)
    {
        var busca = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
        if (busca == null)
            return NotFound();
        return Ok(busca);
    }

    [HttpPost]
    public async Task<ActionResult<Cliente>> Criar([FromBody] Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Listar), new { id = cliente.Id }, cliente);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] Cliente cliente)
    {
        var clienteAtualizado = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
        if (clienteAtualizado == null)
            return NotFound();
        clienteAtualizado.Nome = cliente.Nome;
        clienteAtualizado.Numero = cliente.Numero;
        clienteAtualizado.Email = cliente.Email;
        clienteAtualizado.CPF = cliente.CPF;
        clienteAtualizado.SenhaHash = cliente.SenhaHash;

        await _context.SaveChangesAsync();
        return Ok(clienteAtualizado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var clienteDelete = await _context.Clientes.FirstOrDefaultAsync(d => d.Id == id);
        if (clienteDelete == null)
            return NotFound();
        _context.Clientes.Remove(clienteDelete);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}