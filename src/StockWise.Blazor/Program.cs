using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StockWise.Blazor;
using StockWise.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configura o HttpClient para apontar para a nossa API (dinâmico para deploy)
builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress + "api/") 
});

builder.Services.AddScoped<FrutaApiService>();
builder.Services.AddScoped<ClienteApiService>();
builder.Services.AddScoped<FuncionarioApiService>();
builder.Services.AddScoped<PedidoApiService>();

await builder.Build().RunAsync();
