using AutoMapper;
using SistemaPedidos.Domain.Entities;
using SistemaPedidos.Application.DTOs.Pedido;

namespace SistemaPedidos.Application.Profiles;

public class PedidoProfile : Profile
{
    public PedidoProfile()
    {
        CreateMap<Pedido, PedidoReadDTO>()
            .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => src.Cliente))
            .ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src.Itens));

        CreateMap<PedidoCreateDTO, Pedido>();
        CreateMap<PedidoUpdateDTO, Pedido>();
    }
}
