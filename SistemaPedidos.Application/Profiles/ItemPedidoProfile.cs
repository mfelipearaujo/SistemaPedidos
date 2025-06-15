using AutoMapper;
using SistemaPedidos.Domain.Entities;
using SistemaPedidos.Application.DTOs.ItemPedido;

namespace SistemaPedidos.Application.Profiles;

public class ItemPedidoProfile : Profile
{
    public ItemPedidoProfile()
    {
        CreateMap<ItemPedido, ItemPedidoReadDTO>();
        CreateMap<ItemPedidoCreateDTO, ItemPedido>();
    }
}
