using AutoMapper;
using SistemaPedidos.Domain.Entities;
using SistemaPedidos.Application.DTOs.Cliente;

namespace SistemaPedidos.Application.Profiles;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        CreateMap<Cliente, ClienteReadDTO>();
        CreateMap<ClienteCreateDTO, Cliente>();
        CreateMap<Cliente, ClienteUpdateDTO>().ReverseMap();
    }
}
