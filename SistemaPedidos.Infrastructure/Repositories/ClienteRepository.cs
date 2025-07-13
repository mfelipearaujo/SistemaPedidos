using Microsoft.EntityFrameworkCore;
using SistemaPedidos.Domain.Entities;
using SistemaPedidos.Domain.Repositories;
using SistemaPedidos.Infrastructure.Data;

namespace SistemaPedidos.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        return await _context.Clientes
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Cliente?> GetByIdAsync(Guid id)
    {
        return await _context.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public void Update(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        _context.SaveChanges();
    }

    public void Delete(Cliente cliente)
    {
        _context.Clientes.Remove(cliente);
        _context.SaveChanges();
    }
}
