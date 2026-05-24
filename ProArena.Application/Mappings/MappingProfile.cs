using AutoMapper;
using ProArena.Application.DTOs.Campeonatos;
using ProArena.Application.DTOs.Equipes;
using ProArena.Application.DTOs.Jogadores;
using ProArena.Application.DTOs.Movimentacao;
using ProArena.Application.DTOs.Usuarios;
using ProArena.Domain.Entities;

namespace ProArena.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistraCampeonatoDTO, Campeonato>()
                .ForMember(dest => dest.Partidas, opt => opt.Ignore())
                .ForMember(dest => dest.Inscricoes, opt => opt.Ignore())
                .ForMember(dest => dest.CampeonatoId, opt => opt.Ignore())
                .ForMember(dest => dest.Ativo, opt => opt.Ignore());

            CreateMap<RegistraEquipeDTO, Equipe>()
                .ForMember(dest => dest.EquipeId, opt => opt.Ignore())
                .ForMember(dest => dest.Jogadores, opt => opt.Ignore())
                .ForMember(dest => dest.Partidas, opt => opt.Ignore())
                .ForMember(dest => dest.Inscricao, opt => opt.Ignore())
                .ForMember(dest => dest.NomeExibicao, opt => opt.Ignore());

            CreateMap<RegistraUsuarioDTO, Usuario>()
                .ForMember(r => r.Senha, opt => opt.MapFrom(u => u.Senha))
                .ForMember(r => r.Email, opt => opt.MapFrom(u => u.Email))
                .ForMember(r => r.Nome, opt => opt.MapFrom(u => u.Nome))
                .ForMember(r => r.TipoUsuario, opt => opt.MapFrom(u => u.TipoUsuario));

            CreateMap<RegistraJogadorDTO, Jogador>()
                .ForMember(r => r.Nome, opt => opt.MapFrom(u => u.Nome))
                .ForMember(r => r.Cpf, opt => opt.MapFrom(u => u.Cpf))
                .ForMember(r => r.Idade, opt => opt.MapFrom(u => u.Idade))
                .ForMember(dest => dest.Equipes, opt => opt.Ignore())
                .ForMember(dest => dest.Ativo, opt => opt.MapFrom(_ => true));

            CreateMap<AtualizaJogadorDTO, Jogador>()
                .ForMember(dest => dest.JogadorId, opt => opt.Ignore())
                .ForMember(dest => dest.Cpf, opt => opt.Ignore())
                .ForMember(dest => dest.Equipes, opt => opt.Ignore());

            CreateMap<RegistraMovimentacaoFinanceiraDTO, MovimentacaoFinanceira>()
                .ForMember(dest => dest.MovimentacaoFinanceiraId, opt => opt.Ignore())
                .ForMember(dest => dest.Campeonato, opt => opt.Ignore());

            CreateMap<AtualizaMovimentacaoFinanceiraDTO, MovimentacaoFinanceira>()
                .ForMember(dest => dest.Campeonato, opt => opt.Ignore());

            CreateMap<RegistraPartidaDTO, Partida>()
                .ForMember(dest => dest.PartidaId, opt => opt.Ignore())
                .ForMember(dest => dest.Equipes, opt => opt.Ignore());

            CreateMap<AtualizaPartidaDTO, Partida>()
                .ForMember(dest => dest.Equipes, opt => opt.Ignore());
        }
    }
}
