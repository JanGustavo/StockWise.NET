using System.Net.Http.Json;
using StockWise.Domain.Entities;

namespace StockWise.Blazor.Services;

public class ClienteApiService
{
    private readonly HttpClient _http;
    public ClienteApiService(HttpClient http) => _http = http;

    public async Task<List<Cliente>> GetAllAsync() => await _http.GetFromJsonAsync<List<Cliente>>("api/clientes") ?? new();
    public async Task<HttpResponseMessage> CreateAsync(Cliente cliente) => await _http.PostAsJsonAsync("api/clientes", cliente);
    public async Task<HttpResponseMessage> UpdateAsync(Cliente cliente) => await _http.PutAsJsonAsync($"api/clientes/{cliente.Id}", cliente);
    public async Task<HttpResponseMessage> DeleteAsync(int id) => await _http.DeleteAsync($"api/clientes/{id}");
}

public class FuncionarioApiService
{
    private readonly HttpClient _http;
    public FuncionarioApiService(HttpClient http) => _http = http;

    public async Task<List<Funcionario>> GetAllAsync() => await _http.GetFromJsonAsync<List<Funcionario>>("api/funcionarios") ?? new();
    public async Task<HttpResponseMessage> CreateAsync(Funcionario funcionario) => await _http.PostAsJsonAsync("api/funcionarios", funcionario);
    public async Task<HttpResponseMessage> UpdateAsync(Funcionario funcionario) => await _http.PutAsJsonAsync($"api/funcionarios/{funcionario.Id}", funcionario);
    public async Task<HttpResponseMessage> DeleteAsync(int id) => await _http.DeleteAsync($"api/funcionarios/{id}");
}

public class PedidoApiService
{
    private readonly HttpClient _http;
    public PedidoApiService(HttpClient http) => _http = http;

    public async Task<List<Pedido>> GetAllAsync() => await _http.GetFromJsonAsync<List<Pedido>>("api/pedidos") ?? new();
    public async Task<HttpResponseMessage> CreateAsync(Pedido pedido) => await _http.PostAsJsonAsync("api/pedidos", pedido);
    public async Task<HttpResponseMessage> UpdateAsync(Pedido pedido) => await _http.PutAsJsonAsync($"api/pedidos/{pedido.Id}", pedido);
    public async Task<HttpResponseMessage> DeleteAsync(int id) => await _http.DeleteAsync($"api/pedidos/{id}");
}
