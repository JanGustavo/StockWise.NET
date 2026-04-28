namespace StockWise.Domain.Entities;


public class Funcionario
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;

    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    public string? Cargo { get; set; }
    public decimal Salario { get; set; }
}
