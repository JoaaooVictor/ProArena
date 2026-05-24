import { ICampeonato, ICriaCampeonato } from '../interfaces/ICampeonato';
import { IResultadoOperacao } from '../interfaces/IResultadoOperacao';
import { api } from './api';

const PATH = '/campeonato';

export function CriaCampeonato(criaCampeonato: ICriaCampeonato) {
    return api.post<IResultadoOperacao<ICampeonato>>(`${PATH}/cria-campeonato`, criaCampeonato);
}

export function BuscaTodosCampeonatos() {
    return api.get<IResultadoOperacao<ICampeonato[]>>(`${PATH}/busca-todos-campeonatos`);
}

export function BuscaCampeonatoPorId(id: number) {
    return api.get<IResultadoOperacao<ICampeonato>>(`${PATH}/busca-campeonato-id?id=${id}`);
}
