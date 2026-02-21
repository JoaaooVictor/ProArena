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
                .ForMember(r => r.Descricao, opt => opt.MapFrom(c => c.Descricao))
                .ForMember(r => r.DataInicio, opt => opt.MapFrom(c => c.DataInicio))
                .ForMember(r => r.DataFim, opt => opt.MapFrom(c => c.DataFim))
                .ForMember(dest => dest.Partidas, opt => opt.Ignore());

            CreateMap<RegistraUsuarioDTO, Usuario>()
                .ForMember(r => r.Senha, opt => opt.MapFrom(u => u.Senha))
                .ForMember(r => r.Email, opt => opt.MapFrom(u => u.Email))
                .ForMember(r => r.Nome, opt => opt.MapFrom(u => u.Nome))
                .ForMember(r => r.TipoUsuario, opt => opt.MapFrom(u => u.TipoUsuario));

            CreateMap<RegistraJogadorDTO, Jogador>()
                .ForMember(r => r.Nome, opt => opt.MapFrom(u => u.Nome))
                .ForMember(r => r.Cpf, opt => opt.MapFrom(u => u.Cpf))
                .ForMember(r => r.Idade, opt => opt.MapFrom(u => u.Idade))
                .ForMember(dest => dest.Equipes, opt => opt.Ignore());

            CreateMap<AtualizaJogadorDTO, Jogador>()
                .ForMember(dest => dest.JogadorId, opt => opt.Ignore())
                .ForMember(dest => dest.Cpf, opt => opt.Ignore())
                .ForMember(dest => dest.Equipes, opt => opt.Ignore());
        }
    }
}
