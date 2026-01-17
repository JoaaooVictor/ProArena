import { api } from '../../api/http'
import type { ResponseCampeonatoDTO } from '../../dtos/ResponseCampeonatoDTO';
import type { ResultadoOperacaoDTO } from '../../dtos/ResultadoOperacaoDTO'

const CampeonatoService = {
  async BuscaCampeonatos() {
    return await api.get<ResultadoOperacaoDTO<ResponseCampeonatoDTO[]>>('api/campeonato/busca-todos-campeonatos');
  },

  async BuscaCampeonatoPorId(id: number) {
    return await api.get<ResultadoOperacaoDTO<ResponseCampeonatoDTO>>('api/campeonato/busca-campeonato-id', {
      params: { id },
    });
  }
}

export default CampeonatoService