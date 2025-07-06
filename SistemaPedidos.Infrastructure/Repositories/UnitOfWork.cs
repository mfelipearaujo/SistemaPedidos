using SistemaPedidos.Domain.Repositories;
using SistemaPedidos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace SistemaPedidos.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;

    public IClienteRepository Clientes { get; }
    public IProdutoRepository Produtos { get; }
    public IPedidoRepository Pedidos { get; }

    public UnitOfWork(AppDbContext context,
                      IClienteRepository clienteRepository,
                      IProdutoRepository produtoRepository,
                      IPedidoRepository pedidoRepository)
    {
        _context = context;
        Clientes = clienteRepository;
        Produtos = produtoRepository;
        Pedidos = pedidoRepository;
    }

    public async Task CommitAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
        else
        {
            await _context.SaveChangesAsync();
        }
    }

    public async Task RollbackAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task BeginTransactionAsync()
    {
        if (_transaction == null)
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}