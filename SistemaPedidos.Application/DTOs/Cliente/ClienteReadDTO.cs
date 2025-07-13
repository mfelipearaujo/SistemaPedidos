namespace SistemaPedidos.Application.DTOs.Cliente;

public class ClienteReadDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Telefone { get; set; }
    public string? Endereco { get; set; }
    public DateTime DataCadastro { get; set; }
    public bool Ativo { get; set; }
}