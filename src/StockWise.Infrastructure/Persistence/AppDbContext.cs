using Microsoft.EntityFrameworkCore;
using StockWise.Domain.Entities;

namespace StockWise.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Fruta> Frutas { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<ItemPedido> ItensPedido { get; set; }

    public DbSet<Funcionario> Funcionarios { get; set; }

    public DbSet<Cliente> Clientes { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public AppDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração para Pedido - Cliente (1:N)
        modelBuilder.Entity<Pedido>()
            .HasOne(p => p.Cliente)
            .WithMany(c => c.Pedidos)
            .HasForeignKey(p => p.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuração de precisão decimal
        modelBuilder.Entity<Pedido>()
            .Property(p => p.ValorTotal)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Funcionario>()
            .Property(f => f.Salario)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Fruta>()
            .Property(f => f.Preco)
            .HasPrecision(10, 2);
            
        // Configuração ItemPedido
        modelBuilder.Entity<ItemPedido>()
            .HasOne(ip => ip.Pedido)
            .WithMany(p => p.Itens)
            .HasForeignKey(ip => ip.PedidoId);

        modelBuilder.Entity<ItemPedido>()
            .HasOne(ip => ip.Fruta)
            .WithMany()
            .HasForeignKey(ip => ip.FrutaId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Busca o .env na raiz da solução (subindo até encontrar ou parar na raiz do sistema)
            var currentDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            string? envPath = null;

            while (currentDir != null)
            {
                var testPath = Path.Combine(currentDir.FullName, ".env");
                if (File.Exists(testPath))
                {
                    envPath = testPath;
                    break;
                }
                currentDir = currentDir.Parent;
            }
            
            if (envPath != null)
            {
                DotNetEnv.Env.Load(envPath);
                Console.WriteLine($"[DB] .env carregado de: {envPath}");
            }
            else
            {
                Console.WriteLine("[DB] Aviso: Arquivo .env não encontrado. Usando padrões.");
            }

            var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
            var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
            var database = Environment.GetEnvironmentVariable("DB_NAME") ?? "frutas_db";
            var username = Environment.GetEnvironmentVariable("DB_USER") ?? "postgres";
            var password = (Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "").Trim().Replace("\"", "");

            Console.WriteLine($"[DB] Tentando conectar como: {username} em {host}:{port}");
            Console.WriteLine($"[DB] Senha carregada (tamanho): {password.Length} caracteres");

            var connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password}";
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
