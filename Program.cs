using StockWise.Domain.Entities;
using StockWise.Domain.Exceptions;
using StockWise.Application.Services;
using StockWise.Infrastructure.Persistence;
using StockWise.Infrastructure.Repositories;
using StockWise.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

var services = new ServiceCollection()
    .AddDbContext<AppDbContext>()
    .AddScoped<IFrutaRepository, FrutaRepository>()
    .AddScoped<FrutaService>()
    .BuildServiceProvider();

var frutaService = services.GetRequiredService<FrutaService>();
var db = services.GetRequiredService<AppDbContext>();

bool isRunning = true;

while (isRunning)
{
    var menu = new StringBuilder();
    menu.AppendLine("\n-- 🍉 Frutas do Seu Zé (Clean Architecture) 🍉 --");
    menu.AppendLine("1. Cadastrar fruta");
    menu.AppendLine("2. Listar frutas");
    menu.AppendLine("3. Atualizar fruta");
    menu.AppendLine("4. Deletar fruta");
    menu.AppendLine("5. Registrar pedido (Venda ou Reposição)");
    menu.AppendLine("6. Resumo de pedidos");
    menu.AppendLine("7. Sair");
    menu.Append("Escolha uma opção: ");
    Console.Write(menu);

    string? opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            Console.Write("\nNome da fruta: ");
            string nome = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Preço: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal preco)) break;
            Console.Write("Quantidade: ");
            if (!int.TryParse(Console.ReadLine(), out int quantidade)) break;

            try {
                await frutaService.CadastrarAsync(nome, preco, quantidade);
                Console.WriteLine("\n✅ Fruta cadastrada com sucesso!");
            } catch (Exception ex) { Console.WriteLine($"❌ Erro: {ex.Message}"); }
            break;

        case "2":
            Console.Clear();
            var frutas = await frutaService.ListarAsync();
            Console.WriteLine("-- Lista de Frutas --");
            foreach (var fruta in frutas)
                Console.WriteLine($" Pineapple {fruta.Nome} | Preço: R${fruta.Preco:F2} | Quantidade: {fruta.Quantidade}kg");
            break;

        case "3":
            Console.Write("Nome da fruta a ser atualizada: ");
            string nomeAt = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Novo preço: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal nPreco)) break;
            Console.Write("Nova quantidade: ");
            if (!int.TryParse(Console.ReadLine(), out int nQtd)) break;
            
            try {
                await frutaService.AtualizarAsync(nomeAt, nPreco, nQtd);
                Console.WriteLine("✅ Atualizado!");
            } catch (Exception ex) { Console.WriteLine($"❌ Erro: {ex.Message}"); }
            break;

        case "4":
            Console.Write("Nome da fruta a ser removida: ");
            string nomeRem = Console.ReadLine()?.Trim() ?? "";
            try {
                await frutaService.RemoverAsync(nomeRem);
                Console.WriteLine($"✅ Fruta '{nomeRem}' removida!");
            } catch (Exception ex) { Console.WriteLine($"❌ Erro: {ex.Message}"); }
            break;

        case "5":
            try {
                Console.Write("Tipo (Venda/Reposição): ");
                string tipo = Console.ReadLine()?.ToLower() ?? "";
                if (tipo != "venda" && tipo != "reposição") { Console.WriteLine("Tipo inválido."); break; }

                var pedido = new Pedido { Data = DateTime.UtcNow, Tipo = tipo };
                db.Pedidos.Add(pedido);
                await db.SaveChangesAsync();

                bool addMore = true;
                decimal total = 0;
                while (addMore) {
                    Console.Write("Nome da fruta: ");
                    string nF = Console.ReadLine()?.Trim() ?? "";
                    var fruta = await db.Frutas.FirstOrDefaultAsync(f => f.Nome == nF);
                    if (fruta == null) { Console.WriteLine("Não encontrada."); continue; }

                    Console.Write("Quantidade: ");
                    if (!int.TryParse(Console.ReadLine(), out int qItem)) continue;

                    var item = new ItemPedido { PedidoId = pedido.Id, FrutaId = fruta.Id, Quantidade = qItem };
                    db.ItensPedido.Add(item);

                    if (tipo == "venda") fruta.Quantidade -= qItem;
                    else fruta.Quantidade += qItem;

                    total += qItem * fruta.Preco;
                    await db.SaveChangesAsync();

                    Console.Write("Adicionar mais? (s/n): ");
                    addMore = Console.ReadLine()?.ToLower() == "s";
                }
                pedido.ValorTotal = total;
                await db.SaveChangesAsync();
                Console.WriteLine("✅ Pedido registrado!");
            } catch (Exception ex) { Console.WriteLine($"❌ Erro: {ex.Message}"); }
            break;

        case "6":
            var pedidos = await db.Pedidos.OrderByDescending(p => p.Data).ToListAsync();
            foreach (var p in pedidos)
                Console.WriteLine($"🧾 #{p.Id} | {p.Tipo.ToUpper()} | R${p.ValorTotal:F2}");
            break;

        case "7":
            isRunning = false;
            break;
    }
}
