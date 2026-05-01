using System;
using System.Net.Http;
using System.Threading.Tasks;
using StockWise.Blazor.Services;

namespace StockWise.Blazor.Services;

public static class ApiServiceHelper
{
    public static async Task<string> GetErrorMessageAsync(HttpResponseMessage response)
    {
        try
        {
            var error = await response.Content.ReadAsStringAsync();
            return !string.IsNullOrWhiteSpace(error) ? error : response.ReasonPhrase ?? "Erro desconhecido";
        }
        catch
        {
            return response.ReasonPhrase ?? "Erro desconhecido";
        }
    }
}
