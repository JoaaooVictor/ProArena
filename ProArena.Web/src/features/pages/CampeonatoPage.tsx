import { useEffect, useState } from 'react';
import '../styles/CampeonatoPage.css';
import CampeonatoService from '../services/CampeonatoService';
import type { ResultadoOperacaoDTO } from '../../dtos/ResultadoOperacaoDTO';
import type { ResponseCampeonatoDTO } from '../../dtos/ResponseCampeonatoDTO';
import { Navbar } from '../components/Navbar';

export function CampeonatoPage() {
  const [resultadoOperacaoDTO, setResultadoOperacaoDTO] =
    useState<ResultadoOperacaoDTO<ResponseCampeonatoDTO[]> | null>(null)

  useEffect(() => {
    CampeonatoService.BuscaCampeonatos()
      .then(response => setResultadoOperacaoDTO(response.data))
      .catch(error => console.error(error))
  }, [])

  return (
    <>
      <header className="header">
        <Navbar />
      </header>

      <main> 
        <section className="hero">
          {!resultadoOperacaoDTO && <p>Carregando...</p>}
        </section>
        
        <section className="hero">
          {resultadoOperacaoDTO && !resultadoOperacaoDTO.objeto && (
            <p>{resultadoOperacaoDTO.mensagem}</p>
          )}
        </section>
        
        <section className="hero">
          {resultadoOperacaoDTO && resultadoOperacaoDTO.objeto && (
            <>
              <h3>Campeonatos</h3>

              <table className='table-bordered'>
                <thead>
                  <tr>
                    <th>Nome</th>
                    <th>Data</th>
                    <th>Local</th>
                    <th>Ações</th>
                  </tr>
                </thead>

                <tbody>
                  {resultadoOperacaoDTO.objeto.map(campeonato => (
                    <tr key={campeonato.id}>
                      <td>{campeonato.nome}</td>
                      <td>{campeonato.dataInicio}</td>
                      <td>{campeonato.local}</td>
                      <td>
                        <button title="Visualizar">🔍</button>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </>
          )}
        </section>
      </main>
    </>
  )
}
