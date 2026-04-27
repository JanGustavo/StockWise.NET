using Microsoft.AspNetCore.Mvc;
using StockWise.Application.Services;
using StockWise.Domain.Entities;

namespace StockWise.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FrutasController : ControllerBase
{
    private readonly FrutaService _frutaService;

    public FrutasController(FrutaService frutaService)
    {
        _frutaService = frutaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Fruta>>> Get()
    {
        var frutas = await _frutaService.ListarAsync();
        return Ok(frutas);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Fruta fruta)
    {
        try
        {
            await _frutaService.CadastrarAsync(fruta.Nome!, fruta.Preco, fruta.Quantidade);
            return Ok(new { message = "Fruta cadastrada com sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{nome}")]
    public async Task<ActionResult> Put(string nome, [FromBody] Fruta fruta)
    {
        try
        {
            await _frutaService.AtualizarAsync(nome, fruta.Preco, fruta.Quantidade);
            return Ok(new { message = "Fruta atualizada com sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{nome}")]
    public async Task<ActionResult> Delete(string nome)
    {
        try
        {
            await _frutaService.RemoverAsync(nome);
            return Ok(new { message = "Fruta removida com sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
