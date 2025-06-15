using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPedidos.Application.DTOs.Produto;
using SistemaPedidos.Domain.Entities;
using SistemaPedidos.Infrastructure.Data;

namespace SistemaPedidos.API.Controllers;

[ApiController]
[Route("api/v1/produtos")]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProdutosController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // POST: api/v1/produtos
    [HttpPost]
    public async Task<ActionResult<ProdutoReadDTO>> Create(ProdutoCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var produto = _mapper.Map<Produto>(dto);
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        var produtoReadDto = _mapper.Map<ProdutoReadDTO>(produto);

        return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produtoReadDto);
    }

    // GET: api/v1/produtos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoReadDTO>>> GetAll()
    {
        var produtos = await _context.Produtos.AsNoTracking().ToListAsync();

        var produtosDto = _mapper.Map<List<ProdutoReadDTO>>(produtos);

        return Ok(produtosDto);
    }

    // GET: api/v1/produtos/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoReadDTO>> GetById(int id)
    {
        var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        if (produto is null)
            return NotFound();

        var produtoDto = _mapper.Map<ProdutoReadDTO>(produto);

        return Ok(produtoDto);
    }

    // PUT: api/v1/produtos/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProdutoUpdateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id != dto.Id)
            return BadRequest("ID da URL e do corpo da requisição devem ser os mesmos.");

        var produto = await _context.Produtos.FindAsync(id);

        if (produto is null)
            return NotFound();

        _mapper.Map(dto, produto);

        _context.Entry(produto).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/v1/produtos/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);

        if (produto is null)
            return NotFound();

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
