using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockWise.Domain.Entities;
using StockWise.Infrastructure.Persistence;

namespace StockWise.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly AppDbContext _context;

    public PedidosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pedido>>> Get()
    {
        return await _context.Pedidos
            .Include(p => p.Cliente)
            .Include(p => p.Itens)
                .ThenInclude(i => i.Fruta)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Pedido>> Post(Pedido pedido)
    {
        pedido.Data = DateTime.SpecifyKind(pedido.Data, DateTimeKind.Utc);
        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();
        return Ok(pedido);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var pedido = await _context.Pedidos.FindAsync(id);
        if (pedido == null) return NotFound();

        _context.Pedidos.Remove(pedido);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, Pedido pedido)
    {
        if (id != pedido.Id) return BadRequest();

        pedido.Data = DateTime.SpecifyKind(pedido.Data, DateTimeKind.Utc);
        _context.Entry(pedido).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PedidoExists(id)) return NotFound();
            else throw;
        }

        return NoContent();
    }

    private bool PedidoExists(int id)
    {
        return _context.Pedidos.Any(e => e.Id == id);
    }
}
