using StockWise.Application.Services;
using StockWise.Infrastructure.Persistence;
using StockWise.Infrastructure.Repositories;
using StockWise.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
// Novo padrão .NET 9/10 para documentação
builder.Services.AddOpenApi();

// Configure Context & DI
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<IFrutaRepository, FrutaRepository>();
builder.Services.AddScoped<FrutaService>();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// Mapeia o endpoint da OpenAPI (gera o JSON da documentação)
app.MapOpenApi();
app.MapScalarApiReference();

app.UseCors("AllowAll");
// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
