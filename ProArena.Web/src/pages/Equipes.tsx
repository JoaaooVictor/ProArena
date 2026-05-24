import { useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import Loading from '../components/Loading/Loading';
import { IEquipe, IRegistraEquipe, IAtualizaEquipe } from '../interfaces/IEquipe';
import { IJogador } from '../interfaces/IJogador';
import { ICampeonato } from '../interfaces/ICampeonato';
import {
  BuscaTodasEquipes,
  RegistraEquipe,
  AtualizaEquipe,
  RemoveEquipe
} from '../services/EquipeService';
import { BuscaTodosJogadores } from '../services/JogadorService';
import { BuscaTodosCampeonatos } from '../services/CampeonatoService';
import '../styles/crud.css';

export default function Equipes() {
  const [equipes, setEquipes] = useState<IEquipe[]>([]);
  const [jogadores, setJogadores] = useState<IJogador[]>([]);
  const [campeonatos, setCampeonatos] = useState<ICampeonato[]>([]);
  const [loading, setLoading] = useState(true);
  const [modalAberto, setModalAberto] = useState(false);
  const [editando, setEditando] = useState<IEquipe | null>(null);
  const [nome, setNome] = useState('');
  const [campeonatoId, setCampeonatoId] = useState('');
  const [jogadoresSelecionados, setJogadoresSelecionados] = useState<number[]>([]);

  async function carregarDados() {
    try {
      setLoading(true);
      const [eqResponse, jogResponse, campResponse] = await Promise.all([
        BuscaTodasEquipes(),
        BuscaTodosJogadores(),
        BuscaTodosCampeonatos(),
      ]);

      console.log('Equipes response:', eqResponse);
      console.log('Jogadores response:', jogResponse);
      console.log('Campeonatos response:', campResponse);

      if (!eqResponse.data.erro) {
        setEquipes(eqResponse.data.objeto ?? []);
      } else {
        console.error('Erro ao carregar equipes:', eqResponse.data.mensagem);
        toast.error(eqResponse.data.mensagem || 'Erro ao carregar equipes.');
      }

      if (!jogResponse.data.erro) {
        setJogadores(jogResponse.data.objeto ?? []);
      } else {
        console.error('Erro ao carregar jogadores:', jogResponse.data.mensagem);
        toast.error(jogResponse.data.mensagem || 'Erro ao carregar jogadores.');
      }

      if (!campResponse.data.erro) {
        setCampeonatos(campResponse.data.objeto ?? []);
      } else {
        console.error('Erro ao carregar campeonatos:', campResponse.data.mensagem);
        toast.error(campResponse.data.mensagem || 'Erro ao carregar campeonatos.');
      }
    } catch (error) {
      console.error('Erro ao carregar dados:', error);
      toast.error('Erro ao carregar equipes.');
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    carregarDados();
  }, []);

  function abrirNovo() {
    setEditando(null);
    setNome('');
    setCampeonatoId('');
    setJogadoresSelecionados([]);
    setModalAberto(true);
  }

  function abrirEdicao(equipe: IEquipe) {
    setEditando(equipe);
    setNome(equipe.nome);
    setCampeonatoId(equipe.campeonatoId ? String(equipe.campeonatoId) : '');
    setJogadoresSelecionados(equipe.jogadores.map(j => j.jogadorId));
    setModalAberto(true);
  }

  async function handleSalvar(e: React.FormEvent) {
    e.preventDefault();

    if (!nome.trim() || !campeonatoId || jogadoresSelecionados.length === 0) {
      toast.error('Preencha nome, campeonato e selecione pelo menos um jogador.');
      return;
    }

    try {
      setLoading(true);

      if (editando) {
        const payload: IAtualizaEquipe = {
          EquipeId: editando.equipeId,
          Nome: nome,
          JogadorIds: jogadoresSelecionados,
        };
        const response = await AtualizaEquipe(payload);
        if (response.data.erro) {
          toast.error(response.data.mensagem);
          return;
        }
        toast.success(response.data.mensagem);
      } else {
        const payload: IRegistraEquipe = {
          Nome: nome,
          JogadorIds: jogadoresSelecionados,
          CampeonatoId: Number(campeonatoId),
        };
        const response = await RegistraEquipe(payload);
        if (response.data.erro) {
          toast.error(response.data.mensagem);
          return;
        }
        toast.success(response.data.mensagem);
      }

      setModalAberto(false);
      await carregarDados();
    } catch {
      toast.error('Erro ao salvar equipe.');
    } finally {
      setLoading(false);
    }
  }

  async function handleRemover(id: number) {
    if (!window.confirm('Deseja remover esta equipe?')) return;

    try {
      setLoading(true);
      const response = await RemoveEquipe(id);
      if (response.data.erro) {
        toast.error(response.data.mensagem);
        return;
      }
      toast.success(response.data.mensagem);
      await carregarDados();
    } catch {
      toast.error('Erro ao remover equipe.');
    } finally {
      setLoading(false);
    }
  }

  function toggleJogador(jogadorId: number) {
    if (jogadoresSelecionados.includes(jogadorId)) {
      setJogadoresSelecionados(prev => prev.filter(id => id !== jogadorId));
    } else {
      setJogadoresSelecionados(prev => [...prev, jogadorId]);
    }
  }

  return (
    <>
      {loading && <Loading />}

      <section className="crud-page">
        <header className="crud-top">
          <section>
            <h1 className="crud-title">Equipes</h1>
            <p className="crud-subtitle">Gerencie as equipes dos campeonatos</p>
          </section>
          <button className="btn btn-primary" onClick={abrirNovo}>
            <i className="fa-solid fa-plus me-2" />
            Nova equipe
          </button>
        </header>

        <section className="crud-card">
          {equipes.length === 0 ? (
            <p className="crud-empty">Nenhuma equipe cadastrada.</p>
          ) : (
            <table className="crud-table">
              <thead>
                <tr>
                  <th>Nome</th>
                  <th>Campeonato</th>
                  <th>Jogadores</th>
                  <th>Ações</th>
                </tr>
              </thead>
              <tbody>
                {equipes.map((equipe) => (
                  <tr key={equipe.equipeId}>
                    <td>{equipe.nome}</td>
                    <td>{campeonatos.find(c => c.campeonatoId === equipe.campeonatoId)?.nome || '-'}</td>
                    <td>{equipe.jogadores.length} jogadores</td>
                    <td>
                      <div className="crud-actions">
                        <button className="btn btn-sm btn-outline-primary" onClick={() => abrirEdicao(equipe)}>
                          Editar
                        </button>
                        <button className="btn btn-sm btn-outline-danger" onClick={() => handleRemover(equipe.equipeId)}>
                          Excluir
                        </button>
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          )}
        </section>
      </section>

      {modalAberto && (
        <section className="modal-overlay">
          <article className="custom-modal modal-lg">
            <header className="modal-header">
              <h2>{editando ? 'Editar equipe' : 'Nova equipe'}</h2>
              <button type="button" onClick={() => setModalAberto(false)}>X</button>
            </header>

            <form onSubmit={handleSalvar}>
              <section className="modal-content crud-form-grid">
                <section>
                  <label>Nome da equipe</label>
                  <input
                    className="form-control"
                    value={nome}
                    onChange={(e) => setNome(e.target.value)}
                    placeholder="Nome da equipe"
                  />
                </section>
                <section>
                  <label>Campeonato</label>
                  <select
                    className="form-select"
                    value={campeonatoId}
                    onChange={(e) => setCampeonatoId(e.target.value)}
                    disabled={!!editando}
                  >
                    <option value="">Selecione...</option>
                    {campeonatos.map((camp) => (
                      <option key={camp.campeonatoId} value={camp.campeonatoId}>
                        {camp.nome}
                      </option>
                    ))}
                  </select>
                </section>
                <section style={{ gridColumn: '1 / -1' }}>
                  <label>Jogadores</label>
                  <div className="jogadores-list">
                    {jogadores.map((jogador) => (
                      <label key={jogador.jogadorId} className="jogador-checkbox">
                        <input
                          type="checkbox"
                          checked={jogadoresSelecionados.includes(jogador.jogadorId)}
                          onChange={() => toggleJogador(jogador.jogadorId)}
                        />
                        <span>{jogador.nome}</span>
                      </label>
                    ))}
                  </div>
                </section>
              </section>

              <footer className="modal-footer">
                <button type="button" className="btn btn-outline-secondary" onClick={() => setModalAberto(false)}>
                  Cancelar
                </button>
                <button type="submit" className="btn btn-primary" disabled={loading}>
                  {loading ? 'Salvando...' : 'Salvar'}
                </button>
              </footer>
            </form>
          </article>
        </section>
      )}
    </>
  );
}
