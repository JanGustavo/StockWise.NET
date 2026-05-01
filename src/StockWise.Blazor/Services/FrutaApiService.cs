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

    public async Task<HttpResponseMessage> CadastrarAsync(Fruta fruta)
    {
        return await _http.PostAsJsonAsync("api/frutas", fruta);
    }

    public async Task<HttpResponseMessage> AtualizarAsync(Fruta fruta)
    {
        return await _http.PutAsJsonAsync($"api/frutas/{fruta.Nome}", fruta);
    }

    public async Task<HttpResponseMessage> RemoverAsync(string nome)
    {
        return await _http.DeleteAsync($"api/frutas/{nome}");
    }
}
