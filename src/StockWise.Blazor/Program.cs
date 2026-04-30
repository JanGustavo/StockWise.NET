using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StockWise.Blazor;
using StockWise.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configura o HttpClient para apontar para a nossa API
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5000/";
builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri(apiBaseUrl) 
});

builder.Services.AddScoped<FrutaApiService>();
builder.Services.AddScoped<ClienteApiService>();
builder.Services.AddScoped<FuncionarioApiService>();
builder.Services.AddScoped<PedidoApiService>();

await builder.Build().RunAsync();
