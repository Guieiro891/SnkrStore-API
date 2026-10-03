using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]

public class PagamentoController : ControllerBase
{
    private readonly AppDbContext _context;
    public PagamentoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var listaAtual = await _context.Pagamentos.ToListAsync();
        return Ok(listaAtual);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Pagamento>> Buscar(int id)
    {
        var busca = await _context.Pagamentos.FirstOrDefaultAsync(p => p.Id == id);
        if (busca == null)
            return NotFound();
        return Ok(busca);
    }

    [HttpPost]
    public async Task <ActionResult<Pagamento>> Criar([FromBody] Pagamento pagamento)
    {
        _context.Pagamentos.Add(pagamento);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Listar), new { id = pagamento.Id }, pagamento);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] Pagamento pagamento)
    {
        var pagamentoAtualizado = await _context.Pagamentos.FirstOrDefaultAsync(p => p.Id == id);
        if (pagamentoAtualizado == null)
            return NotFound();
        
        pagamentoAtualizado.PedidoId = pagamento.PedidoId;
        pagamentoAtualizado.Tipo = pagamento.Tipo;
        pagamentoAtualizado.StatusP = pagamento.StatusP;
        pagamentoAtualizado.Valor = pagamento.Valor;

        await _context.SaveChangesAsync();
        return Ok(pagamentoAtualizado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var pagamentoDeletar = await _context.Pagamentos.FirstOrDefaultAsync(p => p.Id == id);
        if (pagamentoDeletar == null)
            return NotFound();

        _context.Pagamentos.Remove(pagamentoDeletar);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}