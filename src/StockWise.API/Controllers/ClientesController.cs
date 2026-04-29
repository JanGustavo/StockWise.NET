using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockWise.Domain.Entities;
using StockWise.Infrastructure.Persistence;

namespace StockWise.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClientesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> Get()
    {
        var counts = await _context.Pedidos
            .GroupBy(p => p.ClienteId)
            .Select(g => new { ClienteId = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.ClienteId, x => x.Count);

        var clientes = await _context.Clientes.ToListAsync();

        foreach (var c in clientes)
        {
            c.QuantidadePedidos = counts.TryGetValue(c.Id, out int count) ? count : 0;
        }

        return clientes;
    }

    [HttpPost]
    public async Task<ActionResult<Cliente>> Post(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
        return Ok(cliente);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null) return NotFound();

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, Cliente cliente)
    {
        if (id != cliente.Id) return BadRequest();

        _context.Entry(cliente).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ClienteExists(id)) return NotFound();
            else throw;
        }

        return NoContent();
    }

    private bool ClienteExists(int id)
    {
        return _context.Clientes.Any(e => e.Id == id);
    }
}
