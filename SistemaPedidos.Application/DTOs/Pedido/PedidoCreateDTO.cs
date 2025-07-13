using System.ComponentModel.DataAnnotations;
using SistemaPedidos.Application.DTOs.ItemPedido;

namespace SistemaPedidos.Application.DTOs.Pedido;

public class PedidoCreateDTO
{
    [Required(ErrorMessage = "O cliente é obrigatório.")]
    public Guid ClienteId { get; set; }

    public string? Observacoes { get; set; }

    [Required(ErrorMessage = "O pedido deve conter pelo menos um produto.")]
    [MinLength(1, ErrorMessage = "O pedido deve conter pelo menos um produto.")]
    public List<ItemPedidoCreateDTO> Itens { get; set; } = new();
}