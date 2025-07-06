using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaPedidos.Application.DTOs.Pedido;
using SistemaPedidos.Domain.Entities;
using SistemaPedidos.Domain.Repositories;

namespace SistemaPedidos.API.Controllers;

[ApiController]
[Route("api/v1/pedidos")]
public class PedidosController : ControllerBase
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;

    public PedidosController(
        IPedidoRepository pedidoRepository,
        IClienteRepository clienteRepository,
        IProdutoRepository produtoRepository,
        IMapper mapper)
    {
        _pedidoRepository = pedidoRepository;
        _clienteRepository = clienteRepository;
        _produtoRepository = produtoRepository;
        _mapper = mapper;
    }

    // POST: api/v1/pedidos
    [HttpPost]
    public async Task<ActionResult<PedidoReadDTO>> Create(PedidoCreateDTO pedidoDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var cliente = await _clienteRepository.GetByIdAsync(pedidoDto.ClienteId);
        if (cliente == null)
            return BadRequest($"Cliente com id {pedidoDto.ClienteId} não encontrado.");

        // Mapear DTO para entidade Pedido
        var pedido = _mapper.Map<Pedido>(pedidoDto);

        // Preencher preços unitários e calcular total (lógica de negócio)
        foreach (var item in pedido.Itens)
        {
            var produto = await _produtoRepository.GetByIdAsync(item.ProdutoId);
            if (produto == null)
                return BadRequest($"Produto com id {item.ProdutoId} não encontrado.");

            item.PrecoUnitario = produto.Preco;
        }

        pedido.CalcularValorTotal();

        await _pedidoRepository.AddAsync(pedido);

        var pedidoCriado = await _pedidoRepository.GetByIdWithDetailsAsync(pedido.Id);

        var pedidoReadDto = _mapper.Map<PedidoReadDTO>(pedidoCriado!);

        return CreatedAtAction(nameof(GetById), new { id = pedido.Id }, pedidoReadDto);
    }

    // GET: api/v1/pedidos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PedidoReadDTO>>> GetAll()
    {
        var pedidos = await _pedidoRepository.GetAllWithDetailsAsync();
        var pedidosDto = _mapper.Map<List<PedidoReadDTO>>(pedidos);

        return Ok(pedidosDto);
    }

    // GET: api/v1/pedidos/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<PedidoReadDTO>> GetById(int id)
    {
        var pedido = await _pedidoRepository.GetByIdWithDetailsAsync(id);

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

        var pedido = await _pedidoRepository.GetByIdWithDetailsAsync(id);

        if (pedido is null)
            return NotFound();

        var cliente = await _clienteRepository.GetByIdAsync(dto.ClienteId);
        if (cliente is null)
            return BadRequest($"Cliente com id {dto.ClienteId} não encontrado.");

        // Atualizar campos simples
        pedido.ClienteId = dto.ClienteId;
        pedido.Observacoes = dto.Observacoes;

        // Remover itens antigos
        _pedidoRepository.RemoveItens(pedido);
        pedido.Itens.Clear();

        // Mapear itens do DTO para itens da entidade e preencher preço
        var novosItens = _mapper.Map<List<ItemPedido>>(dto.Itens);
        foreach (var item in novosItens)
        {
            var produto = await _produtoRepository.GetByIdAsync(item.ProdutoId);
            if (produto is null)
                return BadRequest($"Produto com id {item.ProdutoId} não encontrado.");

            item.PrecoUnitario = produto.Preco;
            pedido.Itens.Add(item);
        }

        pedido.CalcularValorTotal();

        _pedidoRepository.Update(pedido);

        return NoContent();
    }

    // DELETE: api/v1/pedidos/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var pedido = await _pedidoRepository.GetByIdAsync(id);

        if (pedido is null)
            return NotFound();

        _pedidoRepository.Delete(pedido);

        return NoContent();
    }
}
