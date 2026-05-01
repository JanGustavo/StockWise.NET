using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StockWise.Blazor;
using StockWise.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configura o HttpClient para apontar para a nova base da API no Nginx
builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri("https://jangustavo.me/apis/stockwise/") 
});

builder.Services.AddScoped<FrutaApiService>();
builder.Services.AddScoped<ClienteApiService>();
builder.Services.AddScoped<FuncionarioApiService>();
builder.Services.AddScoped<PedidoApiService>();
builder.Services.AddScoped<NotificationService>();

await builder.Build().RunAsync();
