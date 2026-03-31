using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBasica;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClientesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/clientes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetAllAsync()
    {
        var clientes = await _context.Clientes
            .AsNoTracking()
            .ToListAsync();

        return Ok(clientes);
    }

    // GET: api/clientes/1
    [HttpGet("{id:int}", Name = "GetClienteById")]
    public async Task<ActionResult<Cliente>> GetByIdAsync(int id)
    {
        var cliente = await _context.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cliente is null)
        {
            return NotFound();
        }

        return Ok(cliente);
    }

    // POST: api/clientes
    [HttpPost]
    public async Task<ActionResult<Cliente>> CreateAsync(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        return CreatedAtRoute("GetClienteById", new { id = cliente.Id }, cliente);
    }

    // PUT: api/clientes/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, Cliente cliente)
    {
        if (id != cliente.Id)
            return BadRequest("Id do corpo diferente do parâmetro");

        var existe = await _context.Clientes.FindAsync(id);

        if (existe is null)
            return NotFound();

        _context.Entry(existe).State = EntityState.Detached;

        _context.Entry(cliente).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/clientes/1
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);

        if (cliente is null)
            return NotFound();

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}