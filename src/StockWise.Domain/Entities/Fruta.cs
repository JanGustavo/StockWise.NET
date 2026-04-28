using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace StockWise.Domain.Entities;

public class Fruta
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }


    public static bool IsPrecoValido(decimal preco) => preco >= 0 && preco <= 1000;


    public static bool IsQuantidadeValida(int quantidade)
    {
        return quantidade > 0 && quantidade < 1000;
    }

    public static bool IsNomeValido(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            return false;
        }

        Regex regex = new(@"^[A-Za-zÀ-ú\s]+$");
        return regex.IsMatch(nome) && nome.Length <= 40 && nome.Length > 2;
    }

    Dictionary<string, string> Emojis = new()
    {
        { "Maçã", "🍎" },
        { "Banana", "🍌" },
        { "Laranja", "🍊" },
        { "Uva", "🍇" },
        { "Abacaxi", "🍍" },
        { "Manga", "🥭" },
        { "Morango", "🍓" },
        { "Melancia", "🍉" },
        { "Pêssego", "🍑" },
        { "Cereja", "🍒" },
        { "Limão", "🍋" },
        { "Kiwi", "🥝" },
        { "Melão", "🍈" },
        { "Coco", "🥥" },
        { "Pera", "🍐" }
    };
}
