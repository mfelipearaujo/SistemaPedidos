using Microsoft.EntityFrameworkCore;
using SistemaPedidos.Domain.Entities;
using SistemaPedidos.Domain.Repositories;
using SistemaPedidos.Infrastructure.Data;

namespace SistemaPedidos.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Produto produto)
    {
        await _context.Produtos.AddAsync(produto);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Produto>> GetAllAsync()
    {
        return await _context.Produtos
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Produto?> GetByIdAsync(int id)
    {
        return await _context.Produtos
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public void Update(Produto produto)
    {
        _context.Produtos.Update(produto);
        _context.SaveChanges();
    }

    public void Delete(Produto produto)
    {
        _context.Produtos.Remove(produto);
        _context.SaveChanges();
    }
}
