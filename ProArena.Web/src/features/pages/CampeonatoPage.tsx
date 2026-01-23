import { useEffect, useState } from 'react';
import '../styles/pages-styles/CampeonatoPage.css';
import CampeonatoService from '../services/CampeonatoService';
import type { ResultadoOperacaoDTO } from '../../dtos/ResultadoOperacaoDTO';
import type { ResponseCampeonatoDTO } from '../../dtos/ResponseCampeonatoDTO';
import { Navbar } from '../components/Navbar';
import { Footer } from '../components/Footer';

export function CampeonatoPage() {
  const [resultadoOperacaoDTO, setResultadoOperacaoDTO] = useState<ResultadoOperacaoDTO<ResponseCampeonatoDTO[]> | null>(null)
  const [mostrarModal, setMostrarModal] = useState(false);

  useEffect(() => {
    CampeonatoService.BuscaCampeonatos()
      .then(response => setResultadoOperacaoDTO(response.data))
      .catch(error => console.error(error))
  }, [])

  return (
    <>
      <Navbar />
      <main>
        <section>
          {!resultadoOperacaoDTO && <p>Carregando...</p>}
        </section>

        <section>
          {resultadoOperacaoDTO && !resultadoOperacaoDTO.objeto && (
            <p>{resultadoOperacaoDTO.mensagem}</p>
          )}
        </section>

        <section>
          <button onClick={() => setMostrarModal(true)}>Novo Campeonato</button>
          {mostrarModal && (
            <div className="modal-overlay">
              <div className="modal">
                <h3>Novo Campeonato</h3>
                <form>
                  <div className="form-group">
                    <label>Nome</label>
                    <input type="text" />
                  </div>

                  <div className="form-group">
                    <label>Data</label>
                    <input type="date" />
                  </div>

                  <div className="form-group">
                    <label>Local</label>
                    <input type="text" />
                  </div>

                  <div className="modal-actions">
                    <button type="submit">Salvar</button>
                    <button
                      type="button"
                      onClick={() => setMostrarModal(false)}
                    >
                      Cancelar
                    </button>
                  </div>
                </form>
              </div>
            </div>
          )}

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
                    <tr key={campeonato.CampeonatoId}>
                      <td>{campeonato.Descricao}</td>
                      <td>{campeonato.DataFim}</td>
                      <td>{campeonato.DataFim}</td>
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
      <Footer />
    </>
  )
}
