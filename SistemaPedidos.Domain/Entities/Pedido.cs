using System.ComponentModel.DataAnnotations;

namespace SistemaPedidos.Domain.Entities;

public class Pedido
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O cliente é obrigatório.")]
    public Guid ClienteId { get; set; }

    public Cliente Cliente { get; set; } = null!;

    [StringLength(500)]
    public string? Observacoes { get; set; }

    public decimal ValorTotal { get; private set; } = 0m;

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "O pedido deve conter pelo menos um produto.")]
    public ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();

    public void CalcularValorTotal()
    {
        ValorTotal = Itens.Sum(item => item.Quantidade * item.PrecoUnitario);
    }
}
