using System.Net.Http.Json;
using StockWise.Domain.Entities;

namespace StockWise.Blazor.Services;

public class FrutaApiService
{
    private readonly HttpClient _http;

    public FrutaApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Fruta>> GetFrutasAsync()
    {
        return await _http.GetFromJsonAsync<List<Fruta>>("api/frutas") ?? new();
    }

    public async Task CadastrarAsync(Fruta fruta)
    {
        var r = await _http.PostAsJsonAsync("api/frutas", fruta);
        r.EnsureSuccessStatusCode();
    }

    public async Task AtualizarAsync(Fruta fruta)
    {
        var r = await _http.PutAsJsonAsync($"api/frutas/{fruta.Nome}", fruta);
        r.EnsureSuccessStatusCode();
    }

    public async Task RemoverAsync(string nome)
    {
        var r = await _http.DeleteAsync($"api/frutas/{nome}");
        r.EnsureSuccessStatusCode();
    }
}
