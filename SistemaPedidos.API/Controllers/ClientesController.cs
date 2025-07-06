using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPedidos.Application.DTOs.Cliente;
using SistemaPedidos.Domain.Entities;
using SistemaPedidos.Domain.Repositories;

namespace SistemaPedidos.API.Controllers;

[ApiController]
[Route("api/v1/clientes")]
public class ClientesController : ControllerBase
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;

    public ClientesController(IClienteRepository clienteRepository, IMapper mapper)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
    }

    // POST: api/v1/clientes
    [HttpPost]
    public async Task<ActionResult<ClienteReadDTO>> Create(ClienteCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var cliente = _mapper.Map<Cliente>(dto);
        await _clienteRepository.AddAsync(cliente);

        var clienteReadDto = _mapper.Map<ClienteReadDTO>(cliente);

        return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, clienteReadDto);
    }

    // GET: api/v1/clientes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteReadDTO>>> GetAll()
    {
        var clientes = await _clienteRepository.GetAllAsync();

        var clientesDto = _mapper.Map<List<ClienteReadDTO>>(clientes);

        return Ok(clientesDto);
    }

    // GET: api/v1/clientes/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteReadDTO>> GetById(int id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);

        if (cliente is null)
            return NotFound();

        var clienteDto = _mapper.Map<ClienteReadDTO>(cliente);

        return Ok(clienteDto);
    }

    // PUT: api/v1/clientes/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ClienteUpdateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id != dto.Id)
            return BadRequest("ID da URL e do corpo da requisição devem ser os mesmos.");

        var cliente = await _clienteRepository.GetByIdAsync(id);
        if (cliente is null)
            return NotFound();

        _mapper.Map(dto, cliente);

        _clienteRepository.Update(cliente);

        return NoContent();
    }

    // DELETE: api/v1/clientes/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);

        if (cliente is null)
            return NotFound();

        _clienteRepository.Delete(cliente);

        return NoContent();
    }
}
