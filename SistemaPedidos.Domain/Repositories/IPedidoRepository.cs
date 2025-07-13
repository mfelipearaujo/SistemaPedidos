using SistemaPedidos.Domain.Entities;

namespace SistemaPedidos.Domain.Repositories;

public interface IPedidoRepository
{
    Task AddAsync(Pedido pedido);
    Task<IEnumerable<Pedido>> GetAllAsync();
    Task<Pedido?> GetByIdAsync(Guid id);
    Task<IEnumerable<Pedido>> GetAllWithDetailsAsync();
    Task<Pedido?> GetByIdWithDetailsAsync(Guid id);
    void Update(Pedido pedido);
    void Delete(Pedido pedido);
    void RemoveItens(Pedido pedido);
}