namespace StockWise.Domain.Entities;

public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string NumeroTelefone { get; set; } = string.Empty;
    public int QuantidadePedidos { get; set; } = 0;

    public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
