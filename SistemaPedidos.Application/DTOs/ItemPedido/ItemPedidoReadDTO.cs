namespace SistemaPedidos.Application.DTOs.ItemPedido;

public class ItemPedidoReadDTO
{
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
}