using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPedidos.Application.DTOs.Cliente;
using SistemaPedidos.Domain.Entities;
using SistemaPedidos.Infrastructure.Data;

namespace SistemaPedidos.API.Controllers;

[ApiController]
[Route("api/v1/clientes")]
public class ClientesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ClientesController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // POST: api/v1/clientes
    [HttpPost]
    public async Task<ActionResult<ClienteReadDTO>> Create(ClienteCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var cliente = _mapper.Map<Cliente>(dto);
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        var clienteReadDto = _mapper.Map<ClienteReadDTO>(cliente);

        return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, clienteReadDto);
    }

    // GET: api/v1/clientes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteReadDTO>>> GetAll()
    {
        var clientes = await _context.Clientes
            .AsNoTracking()
            .ToListAsync();

        var clientesDto = _mapper.Map<List<ClienteReadDTO>>(clientes);

        return Ok(clientesDto);
    }

    // GET: api/v1/clientes/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteReadDTO>> GetById(int id)
    {
        var cliente = await _context.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

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

        var clienteExistente = await _context.Clientes.FindAsync(id);
        if (clienteExistente is null)
            return NotFound();

        _mapper.Map(dto, clienteExistente);

        _context.Entry(clienteExistente).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/v1/clientes/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);

        if (cliente is null)
            return NotFound();

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
