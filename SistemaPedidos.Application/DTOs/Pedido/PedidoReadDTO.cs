using SistemaPedidos.Application.DTOs.Cliente;
using SistemaPedidos.Application.DTOs.ItemPedido;

namespace SistemaPedidos.Application.DTOs.Pedido;

public class PedidoReadDTO
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string? Observacoes { get; set; }
    public decimal ValorTotal { get; set; }
    public DateTime DataCriacao { get; set; }
    public ClienteReadDTO Cliente { get; set; } = null!;
    public List<ItemPedidoReadDTO> Itens { get; set; } = new();
}