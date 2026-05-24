import { useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import Loading from '../components/Loading/Loading';
import { ModalCampeonato } from '../components/Modal/ModalCampeonato';
import { ICampeonato } from '../interfaces/ICampeonato';
import { BuscaTodosCampeonatos } from '../services/CampeonatoService';
import { FormataData } from '../utils/Formatacao';
import '../styles/crud.css';

export default function Campeonatos() {
  const [campeonatos, setCampeonatos] = useState<ICampeonato[]>([]);
  const [loading, setLoading] = useState(true);
  const [modalAberto, setModalAberto] = useState(false);

  async function carregarCampeonatos() {
    try {
      setLoading(true);
      const response = await BuscaTodosCampeonatos();
      if (!response.data.erro) {
        setCampeonatos(response.data.objeto ?? []);
      }
    } catch {
      toast.error('Erro ao carregar campeonatos.');
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    carregarCampeonatos();
  }, []);

  return (
    <>
      {loading && <Loading />}

      <section className="crud-page">
        <header className="crud-top">
          <section>
            <h1 className="crud-title">Campeonatos</h1>
            <p className="crud-subtitle">Organize campeonatos e acompanhe inscrições</p>
          </section>
          <button className="btn btn-primary" onClick={() => setModalAberto(true)}>
            <i className="fa-solid fa-plus me-2" />
            Novo campeonato
          </button>
        </header>

        <section className="crud-card">
          {campeonatos.length === 0 ? (
            <p className="crud-empty">Nenhum campeonato cadastrado.</p>
          ) : (
            <table className="crud-table">
              <thead>
                <tr>
                  <th>Nome</th>
                  <th>Período</th>
                  <th>Inscrições</th>
                  <th>Status</th>
                </tr>
              </thead>
              <tbody>
                {campeonatos.map((campeonato) => (
                  <tr key={campeonato.campeonatoId}>
                    <td>
                      <strong>{campeonato.nome}</strong>
                      {campeonato.descricao && <small className="d-block text-muted">{campeonato.descricao}</small>}
                    </td>
                    <td>
                      {FormataData(campeonato.dataInicio)} - {FormataData(campeonato.dataFim)}
                    </td>
                    <td>
                      {FormataData(campeonato.dataInicioInscricao)} - {FormataData(campeonato.dataFimInscricao)}
                    </td>
                    <td>
                      <span className={campeonato.ativo ? 'badge-ativo' : 'badge-inativo'}>
                        {campeonato.ativo ? 'Ativo' : 'Inativo'}
                      </span>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          )}
        </section>
      </section>

      <ModalCampeonato
        isOpen={modalAberto}
        onClose={() => setModalAberto(false)}
        onSuccess={carregarCampeonatos}
      />
    </>
  );
}
