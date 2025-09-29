using System.ComponentModel.DataAnnotations;

namespace SistemaPedidos.Application.DTOs.Cliente;

public class ClienteCreateDTO
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public string Nome { get; set; } = null!;

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    public int CPF { get; set; }

    [Phone(ErrorMessage = "O telefone informado não é válido.")]
    [StringLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres.")]
    public string? Telefone { get; set; }

    [StringLength(200, ErrorMessage = "O endereço deve ter no máximo 200 caracteres.")]
    public string? Endereco { get; set; }

    public bool Ativo { get; set; }
}
