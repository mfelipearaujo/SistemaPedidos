namespace SistemaPedidos.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    IClienteRepository Clientes { get; }
    IProdutoRepository Produtos { get; }
    IPedidoRepository Pedidos { get; }

    Task CommitAsync();
    Task RollbackAsync();
}