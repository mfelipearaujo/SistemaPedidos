using System.ComponentModel.DataAnnotations;

namespace SistemaPedidos.Domain.Entities;

public class Cliente
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
    public string Email { get; set; } = string.Empty;

    [Phone(ErrorMessage = "O telefone informado não é válido.")]
    [StringLength(20)]
    public string? Telefone { get; set; }

    [StringLength(200)]
    public string? Endereco { get; set; }

    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    public bool Ativo { get; set; } = true;
}
