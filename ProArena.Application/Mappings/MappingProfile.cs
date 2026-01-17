using AutoMapper;
using ProArena.Application.DTOs;
using ProArena.Domain.Entities;

namespace ProArena.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<RegistraCampeonatoDTO, Campeonato>()
                .ForMember(r => r.Descricao, opt => opt.MapFrom(c => c.Descricao));
        }
    }
}
