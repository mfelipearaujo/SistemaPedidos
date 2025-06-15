using AutoMapper;
using SistemaPedidos.Application.DTOs.Produto;
using SistemaPedidos.Domain.Entities;

namespace SistemaPedidos.Application.Profiles;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<Produto, ProdutoReadDTO>();
        CreateMap<ProdutoCreateDTO, Produto>();
        CreateMap<ProdutoUpdateDTO, Produto>();
        CreateMap<Produto, ProdutoUpdateDTO>();
    }
}
