using AutoMapper;
using ProArena.Application.DTOs;
using ProArena.Application.DTOs.Usuarios;
using ProArena.Domain.Entities;

namespace ProArena.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<RegistraCampeonatoDTO, Campeonato>()
                .ForMember(r => r.Descricao, opt => opt.MapFrom(c => c.Descricao));

            CreateMap<RegistraUsuarioDTO, Usuario>()
                .ForMember(r => r.Senha, opt => opt.MapFrom(u => u.Senha))
                .ForMember(r => r.Email, opt => opt.MapFrom(u => u.Email))
                .ForMember(r => r.Nome, opt => opt.MapFrom(u => u.Nome));
        }
    }
}
