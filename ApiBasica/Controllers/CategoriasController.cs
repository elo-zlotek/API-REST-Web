using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBasica.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/categorias
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetAsync()
    {
        return await _context.Categorias.AsNoTracking().ToListAsync();
    }

    // GET: api/categorias/1
    [HttpGet("{id:int}", Name = "GetCategoriaById")]
    public async Task<ActionResult<Categoria>> GetByIdAsync(int id)
    {
        var categoria = await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return categoria is null ? NotFound() : Ok(categoria);
    }

    // POST: api/categorias
    [HttpPost]
    public async Task<ActionResult<Categoria>> CreateAsync(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        // Retorna 201 Created conforme exigido no PDF
        return CreatedAtRoute("GetCategoriaById", new { id = categoria.Id }, categoria);
    }

    // PUT: api/categorias/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, Categoria categoria)
    {
        if (id != categoria.Id) return BadRequest("IDs não conferem.");

        var existe = await _context.Categorias.AsNoTracking().AnyAsync(c => c.Id == id);
        if (!existe) return NotFound();

        _context.Entry(categoria).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        // Retorna 204 No Content conforme exigido no PDF
        return NoContent();
    }

    // DELETE: api/categorias/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria is null) return NotFound();

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}