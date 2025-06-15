using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPedidos.Application.DTOs.Pedido;
using SistemaPedidos.Domain.Entities;
using SistemaPedidos.Infrastructure.Data;

namespace SistemaPedidos.API.Controllers;

[ApiController]
[Route("api/v1/pedidos")]
public class PedidosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public PedidosController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // POST: api/v1/pedidos
    [HttpPost]
    public async Task<ActionResult<PedidoReadDTO>> Create(PedidoCreateDTO pedidoDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var cliente = await _context.Clientes.FindAsync(pedidoDto.ClienteId);
        if (cliente == null)
            return BadRequest($"Cliente com id {pedidoDto.ClienteId} não encontrado.");

        // Mapear DTO para entidade Pedido
        var pedido = _mapper.Map<Pedido>(pedidoDto);

        // Preencher preços unitários e calcular total (lógica de negócio)
        foreach (var item in pedido.Itens)
        {
            var produto = await _context.Produtos.FindAsync(item.ProdutoId);
            if (produto == null)
                return BadRequest($"Produto com id {item.ProdutoId} não encontrado.");

            item.PrecoUnitario = produto.Preco;
        }

        pedido.CalcularValorTotal();

        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();

        // Recarregar pedido com relacionamentos para o retorno
        var pedidoCriado = await _context.Pedidos
            .Include(p => p.Cliente)
            .Include(p => p.Itens)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == pedido.Id);

        var pedidoReadDto = _mapper.Map<PedidoReadDTO>(pedidoCriado!);

        return CreatedAtAction(nameof(GetById), new { id = pedido.Id }, pedidoReadDto);
    }

    // GET: api/v1/pedidos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PedidoReadDTO>>> GetAll()
    {
        var pedidos = await _context.Pedidos
            .Include(p => p.Cliente)
            .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
            .AsNoTracking()
            .ToListAsync();

        var pedidosDto = _mapper.Map<List<PedidoReadDTO>>(pedidos);

        return Ok(pedidosDto);
    }

    // GET: api/v1/pedidos/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<PedidoReadDTO>> GetById(int id)
    {
        var pedido = await _context.Pedidos
            .Include(p => p.Cliente)
            .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pedido is null)
            return NotFound();

        var pedidoDto = _mapper.Map<PedidoReadDTO>(pedido);

        return Ok(pedidoDto);
    }

    // PUT: api/v1/pedidos/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PedidoUpdateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var pedido = await _context.Pedidos
            .Include(p => p.Itens)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pedido is null)
            return NotFound();

        var cliente = await _context.Clientes.FindAsync(dto.ClienteId);
        if (cliente is null)
            return BadRequest($"Cliente com id {dto.ClienteId} não encontrado.");

        // Atualizar campos simples
        pedido.ClienteId = dto.ClienteId;
        pedido.Observacoes = dto.Observacoes;

        // Remover itens antigos
        _context.ItensPedido.RemoveRange(pedido.Itens);
        pedido.Itens.Clear();

        // Mapear itens do DTO para itens da entidade e preencher preço
        var novosItens = _mapper.Map<List<ItemPedido>>(dto.Itens);
        foreach (var item in novosItens)
        {
            var produto = await _context.Produtos.FindAsync(item.ProdutoId);
            if (produto is null)
                return BadRequest($"Produto com id {item.ProdutoId} não encontrado.");

            item.PrecoUnitario = produto.Preco;
            pedido.Itens.Add(item);
        }

        pedido.CalcularValorTotal();

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/v1/pedidos/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var pedido = await _context.Pedidos.FindAsync(id);

        if (pedido is null)
            return NotFound();

        _context.Pedidos.Remove(pedido);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
