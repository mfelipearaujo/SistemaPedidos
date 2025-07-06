using SistemaPedidos.Domain.Entities;

namespace SistemaPedidos.Domain.Repositories;

public interface IClienteRepository
{
    Task AddAsync(Cliente cliente);
    Task<IEnumerable<Cliente>> GetAllAsync();
    Task<Cliente?> GetByIdAsync(int id);
    void Update(Cliente cliente);
    void Delete(Cliente cliente);
}