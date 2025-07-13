using Microsoft.EntityFrameworkCore;
using SistemaPedidos.Domain.Entities;
using SistemaPedidos.Domain.Repositories;
using SistemaPedidos.Infrastructure.Data;

namespace SistemaPedidos.Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly AppDbContext _context;

    public PedidoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Pedido pedido)
    {
        await _context.Pedidos.AddAsync(pedido);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Pedido>> GetAllAsync()
    {
        return await _context.Pedidos
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Pedido?> GetByIdAsync(Guid id)
    {
        return await _context.Pedidos
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Pedido>> GetAllWithDetailsAsync()
    {
        return await _context.Pedidos
            .Include(p => p.Cliente)
            .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Pedido?> GetByIdWithDetailsAsync(Guid id)
    {
        return await _context.Pedidos
            .Include(p => p.Cliente)
            .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public void Update(Pedido pedido)
    {
        _context.Pedidos.Update(pedido);
        _context.SaveChanges();
    }

    public void Delete(Pedido pedido)
    {
        _context.Pedidos.Remove(pedido);
        _context.SaveChanges();
    }

    public void RemoveItens(Pedido pedido)
    {
        _context.ItensPedido.RemoveRange(pedido.Itens);
        _context.SaveChanges();
    }
}
