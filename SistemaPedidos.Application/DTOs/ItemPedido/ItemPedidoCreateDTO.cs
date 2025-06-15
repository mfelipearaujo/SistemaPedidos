using System.ComponentModel.DataAnnotations;

namespace SistemaPedidos.Application.DTOs.ItemPedido;

public class ItemPedidoCreateDTO
{
    [Required(ErrorMessage = "O produto é obrigatório.")]
    public int ProdutoId { get; set; }

    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    public int Quantidade { get; set; }
}