using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StockWise.Blazor;
using StockWise.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configura o HttpClient para apontar para a nossa API
// Em produção, isso viria de um arquivo de configuração
builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri("http://localhost:5202/") 
});

builder.Services.AddScoped<FrutaApiService>();

await builder.Build().RunAsync();
