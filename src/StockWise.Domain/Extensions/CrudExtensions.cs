using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockWise.Domain.Extensions;

public static class CrudExtensions
{
    /// <summary>
    /// Executa uma chamada de API de forma segura, tratando exceções e centralizando o log.
    /// </summary>
    public static async Task<List<T>> CarregarDadosSafeAsync<T>(
        Func<Task<List<T>>> busca,
        string nomeEntidade)
    {
        try
        {
            return await busca();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{nomeEntidade}] Erro ao carregar dados: {ex.Message}");
            return new List<T>();
        }
    }

    /// <summary>
    /// Executa uma ação de salvamento de forma segura.
    /// </summary>
    public static async Task<bool> SalvarSafeAsync(
        Func<Task> salvar,
        string nomeEntidade)
    {
        try
        {
            await salvar();
            Console.WriteLine($"[{nomeEntidade}] Salvo com sucesso.");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{nomeEntidade}] Erro ao salvar: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Executa uma ação de atualização de forma segura.
    /// </summary>
    public static async Task<bool> AtualizarSafeAsync(
        Func<Task> atualizar,
        string nomeEntidade)
    {
        try
        {
            await atualizar();
            Console.WriteLine($"[{nomeEntidade}] Atualizado com sucesso.");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{nomeEntidade}] Erro ao atualizar: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Exibe confirmação e executa uma ação de exclusão de forma segura.
    /// </summary>
    public static async Task<bool> DeletarSafeAsync(
        Func<Task> deletar,
        string nomeEntidade)
    {
        try
        {
            await deletar();
            Console.WriteLine($"[{nomeEntidade}] Deletado com sucesso.");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{nomeEntidade}] Erro ao deletar: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Filtra uma coleção de objetos com base em um termo de busca, tratando acentos e ignorando maiúsculas/minúsculas.
    /// </summary>
    public static IEnumerable<T> FiltrarPorTexto<T>(
        this IEnumerable<T>? fonte,
        string termoBusca,
        Func<T, string?> seletorCampo)
    {
        if (fonte == null)
            return Enumerable.Empty<T>();

        if (string.IsNullOrWhiteSpace(termoBusca))
            return fonte;

        var termoNormalizado = termoBusca.RemoverAcentos();

        return fonte.Where(item =>
        {
            var valor = seletorCampo(item);
            return valor != null && valor.RemoverAcentos().Contains(termoNormalizado);
        });
    }
}