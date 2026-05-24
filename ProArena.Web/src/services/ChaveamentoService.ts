import axios from 'axios';
import { IChaveamento } from '../interfaces/IChaveamento';

const API_URL = import.meta.env.VITE_API_URL || 'https://localhost:7164/api';

export const IniciaCampeonato = async (campeonatoId: number) => {
  const token = localStorage.getItem('token');
  console.log('Iniciando campeonato:', campeonatoId, 'Token:', token ? 'Presente' : 'Ausente');
  
  try {
    const response = await axios.post(
      `${API_URL}/campeonato/inicia-campeonato?campeonatoId=${campeonatoId}`,
      {},
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );
    console.log('Resposta do servidor ao iniciar campeonato:', response);
    return response;
  } catch (error) {
    console.error('Erro ao chamar endpoint de iniciar campeonato:', error);
    throw error;
  }
};

export const BuscaChaveamento = async (campeonatoId: number) => {
  const token = localStorage.getItem('token');
  const response = await axios.get(
    `${API_URL}/campeonato/busca-chaveamento?campeonatoId=${campeonatoId}`,
    {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }
  );
  return response;
};
