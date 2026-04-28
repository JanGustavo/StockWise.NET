using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace StockWise.Domain.Extensions;

public static class StringExtensions
{
    public static string RemoverAcentos(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC).ToLowerInvariant();
    }

    public static string ObterEmojiFruta(this string nome)
    {
        var s = nome.RemoverAcentos();

        if (s.Contains("laranja")) return "🍊";
        if (s.Contains("banana")) return "🍌";
        if (s.Contains("maca")) return "🍎";
        if (s.Contains("morango")) return "🍓";
        if (s.Contains("uva")) return "🍇";
        if (s.Contains("limao")) return "🍋";
        if (s.Contains("abacaxi")) return "🍍";
        if (s.Contains("melancia")) return "🍉";
        if (s.Contains("pera")) return "🍐";
        if (s.Contains("pessego")) return "🍑";
        if (s.Contains("cereja")) return "🍒";
        if (s.Contains("mamao")) return "🥭";
        if (s.Contains("kiwi")) return "🥝";
        if (s.Contains("tomate")) return "🍅";
        if (s.Contains("coco")) return "🥥";
        if (s.Contains("abacate")) return "🥑";

        return "📦"; // Novo fallback para identificar se não houve match
    }
}
