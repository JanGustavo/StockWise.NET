using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockWise.Domain.Entities;
using StockWise.Infrastructure.Persistence;
using BCrypt.Net;
using Npgsql;

namespace StockWise.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FuncionariosController : ControllerBase
{
    private readonly AppDbContext _context;

    public FuncionariosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Funcionario>>> Get()
    {
        return await _context.Funcionarios.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Funcionario>> Post(Funcionario funcionario)
    {
        funcionario.DataCadastro = DateTime.SpecifyKind(funcionario.DataCadastro, DateTimeKind.Utc);
        funcionario.Senha = BCrypt.Net.BCrypt.HashPassword(funcionario.Senha);
        
        _context.Funcionarios.Add(funcionario);
        await _context.SaveChangesAsync();
        return Ok(funcionario);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        if (id == 1) return BadRequest("O usuário administrador padrão não pode ser excluído.");

        var funcionario = await _context.Funcionarios.FindAsync(id);
        if (funcionario == null) return NotFound();

        try 
        {
            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "P0001")
        {
            // Captura o RAISE EXCEPTION da Trigger do banco
            return BadRequest(pgEx.MessageText);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao deletar: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, Funcionario funcionario)
    {
        if (id == 1) return BadRequest("O usuário administrador padrão não pode ser editado.");
        if (id != funcionario.Id) return BadRequest();

        var funcionarioExistente = await _context.Funcionarios.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
        if (funcionarioExistente == null) return NotFound();

        if (funcionario.Senha != funcionarioExistente.Senha)
        {
            funcionario.Senha = BCrypt.Net.BCrypt.HashPassword(funcionario.Senha);
        }

        funcionario.DataCadastro = DateTime.SpecifyKind(funcionario.DataCadastro, DateTimeKind.Utc);
        _context.Entry(funcionario).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FuncionarioExists(id)) return NotFound();
            else throw;
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "P0001")
        {
            // Captura o RAISE EXCEPTION da Trigger do banco
            return BadRequest(pgEx.MessageText);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao atualizar: {ex.Message}");
        }

        return NoContent();
    }

    private bool FuncionarioExists(int id)
    {
        return _context.Funcionarios.Any(e => e.Id == id);
    }
}
