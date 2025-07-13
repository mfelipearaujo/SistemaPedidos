using SistemaPedidos.Domain.Entities;

namespace SistemaPedidos.Domain.Repositories;

public interface IProdutoRepository
{
    Task AddAsync(Produto produto);
    Task<IEnumerable<Produto>> GetAllAsync();
    Task<Produto?> GetByIdAsync(Guid id);
    void Update(Produto produto);
    void Delete(Produto produto);
}