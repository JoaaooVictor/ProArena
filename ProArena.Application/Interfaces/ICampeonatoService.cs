using ProArena.Application.Utils;
using ProArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProArena.Application.Interfaces
{
    public interface ICampeonatoService
    {
        Task<ResultadoOperacao> BuscaCampeonatoPorId(int id);
        Task<ResultadoOperacao> BuscaTodosCampeonatos();
        Task AdicionaCampeonato(Campeonato campeonato);
    }
}
