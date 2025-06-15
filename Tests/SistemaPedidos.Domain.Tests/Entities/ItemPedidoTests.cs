using SistemaPedidos.Domain.Entities;

namespace SistemaPedidos.Domain.Tests.Entities;

public class ItemPedidoTests
{
    [Fact]
    public void Total_DeveRetornarMultiplicacaoDeQuantidadeEPrecoUnitario()
    {
        // Arrange: Criamos um item com quantidade e preço definidos
        var item = new ItemPedido
        {
            Quantidade = 3,
            PrecoUnitario = 15.5m
        };

        // Act: Acessamos a propriedade Total
        var total = item.Total;

        // Assert: Verificamos se o total está correto
        Assert.Equal(46.5m, total);
    }
}
