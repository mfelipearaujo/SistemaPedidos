using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaPedidos.Domain.Entities;

public class ItemPedido
{
    [Required(ErrorMessage = "O pedido é obrigatório.")]
    public Guid PedidoId { get; set; }

    public Pedido Pedido { get; set; } = null!;

    [Required(ErrorMessage = "O produto é obrigatório.")]
    public Guid ProdutoId { get; set; }

    public Produto Produto { get; set; } = null!;

    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser pelo menos 1.")]
    public int Quantidade { get; set; }

    [Required(ErrorMessage = "O preço unitário é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço unitário deve ser maior que zero.")]
    public decimal PrecoUnitario { get; set; }

    [NotMapped]
    public decimal Total => Quantidade * PrecoUnitario;
}
