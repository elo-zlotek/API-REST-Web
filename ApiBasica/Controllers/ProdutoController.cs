using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBasica.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/produtos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> GetAllAsync()
    {
        // Retorna todos os produtos sem rastreamento (mais rápido para leitura)
        return await _context.Produtos.AsNoTracking().ToListAsync();
    }

    // GET: api/produtos/1
    [HttpGet("{id:int}", Name = "GetProdutoById")]
    public async Task<ActionResult<Produto>> GetByIdAsync(int id)
    {
        var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        if (produto is null)
        {
            return NotFound();
        }

        return Ok(produto);
    }

    // POST: api/produtos
    [HttpPost]
    public async Task<ActionResult<Produto>> CreateAsync(Produto produto)
    {
        // O .NET valida as Data Annotations automaticamente aqui.
        // Se o preço for 0 ou estoque negativo, ele já retorna 400 Bad Request.
        
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        // Retorna 201 Created e o link para o produto criado
        return CreatedAtRoute("GetProdutoById", new { id = produto.Id }, produto);
    }

    // PUT: api/produtos/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, Produto produto)
    {
        if (id != produto.Id)
            return BadRequest("O ID no corpo do JSON não confere com o ID da URL.");

        var existe = await _context.Produtos.AsNoTracking().AnyAsync(p => p.Id == id);

        if (!existe)
            return NotFound();

        _context.Entry(produto).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        // Retorna 204 No Content conforme exigido no PDF
        return NoContent();
    }

    // DELETE: api/produtos/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);

        if (produto is null)
            return NotFound();

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        // Retorna 204 No Content para exclusão bem-sucedida
        return NoContent();
    }
}