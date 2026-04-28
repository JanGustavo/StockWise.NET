using System.Net.Http.Json;
using StockWise.Domain.Entities;

namespace StockWise.Blazor.Services;

public class ClienteApiService
{
    private readonly HttpClient _http;
    public ClienteApiService(HttpClient http) => _http = http;

    public async Task<List<Cliente>> GetAllAsync() => await _http.GetFromJsonAsync<List<Cliente>>("api/clientes") ?? new();
    public async Task CreateAsync(Cliente cliente) { var r = await _http.PostAsJsonAsync("api/clientes", cliente); r.EnsureSuccessStatusCode(); }
    public async Task UpdateAsync(Cliente cliente) { var r = await _http.PutAsJsonAsync($"api/clientes/{cliente.Id}", cliente); r.EnsureSuccessStatusCode(); }
    public async Task DeleteAsync(int id) { var r = await _http.DeleteAsync($"api/clientes/{id}"); r.EnsureSuccessStatusCode(); }
}

public class FuncionarioApiService
{
    private readonly HttpClient _http;
    public FuncionarioApiService(HttpClient http) => _http = http;

    public async Task<List<Funcionario>> GetAllAsync() => await _http.GetFromJsonAsync<List<Funcionario>>("api/funcionarios") ?? new();
    public async Task CreateAsync(Funcionario funcionario) { var r = await _http.PostAsJsonAsync("api/funcionarios", funcionario); r.EnsureSuccessStatusCode(); }
    public async Task UpdateAsync(Funcionario funcionario) { var r = await _http.PutAsJsonAsync($"api/funcionarios/{funcionario.Id}", funcionario); r.EnsureSuccessStatusCode(); }
    public async Task DeleteAsync(int id) { var r = await _http.DeleteAsync($"api/funcionarios/{id}"); r.EnsureSuccessStatusCode(); }
}

public class PedidoApiService
{
    private readonly HttpClient _http;
    public PedidoApiService(HttpClient http) => _http = http;

    public async Task<List<Pedido>> GetAllAsync() => await _http.GetFromJsonAsync<List<Pedido>>("api/pedidos") ?? new();
    public async Task CreateAsync(Pedido pedido) { var r = await _http.PostAsJsonAsync("api/pedidos", pedido); r.EnsureSuccessStatusCode(); }
    public async Task UpdateAsync(Pedido pedido) { var r = await _http.PutAsJsonAsync($"api/pedidos/{pedido.Id}", pedido); r.EnsureSuccessStatusCode(); }
    public async Task DeleteAsync(int id) { var r = await _http.DeleteAsync($"api/pedidos/{id}"); r.EnsureSuccessStatusCode(); }
}
