using SistemaPedidos.Domain.Entities;

namespace SistemaPedidos.Domain.Tests.Entities;

public class PedidoTests
{
    [Fact]
    public void CalcularValorTotal_DeveSomarCorretamenteOsValoresDosItens()
    {
        // Arrange: Criamos um pedido com dois itens de exemplo
        var pedido = new Pedido
        {
            Itens = new List<ItemPedido>
            {
                new() { Quantidade = 2, PrecoUnitario = 10.0m },
                new() { Quantidade = 1, PrecoUnitario = 20.0m }
            }
        };

        // Act: Chamamos o método que calcula o valor total
        pedido.CalcularValorTotal();

        // Assert: Verificamos se o valor total foi calculado corretamente
        Assert.Equal(40.0m, pedido.ValorTotal);
    }
}
