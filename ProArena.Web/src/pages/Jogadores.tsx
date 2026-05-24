import { FormEvent, useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import Loading from '../components/Loading/Loading';
import { IAtualizaJogador, IJogador, IRegistraJogador } from '../interfaces/IJogador';
import { AtualizaJogador, BuscaTodosJogadores, RegistraJogador } from '../services/JogadorService';
import { AplicaMascaraCpf } from '../utils/Formatacao';
import '../styles/crud.css';
import '../components/Modal/Modal.css';

export default function Jogadores() {
  const [jogadores, setJogadores] = useState<IJogador[]>([]);
  const [loading, setLoading] = useState(true);
  const [modalAberto, setModalAberto] = useState(false);
  const [editando, setEditando] = useState<IJogador | null>(null);
  const [nome, setNome] = useState('');
  const [cpf, setCpf] = useState('');
  const [idade, setIdade] = useState('');
  const [ativo, setAtivo] = useState(true);

  async function carregarJogadores() {
    try {
      setLoading(true);
      const response = await BuscaTodosJogadores();
      if (!response.data.erro) {
        setJogadores(response.data.objeto ?? []);
      }
    } catch {
      toast.error('Erro ao carregar jogadores.');
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    carregarJogadores();
  }, []);

  function abrirNovo() {
    setEditando(null);
    setNome('');
    setCpf('');
    setIdade('');
    setAtivo(true);
    setModalAberto(true);
  }

  function abrirEdicao(jogador: IJogador) {
    setEditando(jogador);
    setNome(jogador.nome);
    setCpf(jogador.cpf);
    setIdade(String(jogador.idade));
    setAtivo(jogador.ativo);
    setModalAberto(true);
  }

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();

    if (!nome.trim() || !cpf.trim() || !idade) {
      toast.error('Preencha todos os campos obrigatórios.');
      return;
    }

    try {
      setLoading(true);

      if (editando) {
        const payload: IAtualizaJogador = {
          JogadorId: editando.jogadorId,
          Nome: nome,
          Idade: Number(idade),
          Ativo: ativo,
        };
        const response = await AtualizaJogador(payload);
        if (response.data.erro) {
          toast.error(response.data.mensagem);
          return;
        }
        toast.success(response.data.mensagem);
      } else {
        const payload: IRegistraJogador = {
          Nome: nome,
          Cpf: cpf,
          Idade: Number(idade),
        };
        const response = await RegistraJogador(payload);
        if (response.data.erro) {
          toast.error(response.data.mensagem);
          return;
        }
        toast.success(response.data.mensagem);
      }

      setModalAberto(false);
      await carregarJogadores();
    } catch {
      toast.error('Erro ao salvar jogador.');
    } finally {
      setLoading(false);
    }
  }

  return (
    <>
      {loading && <Loading />}

      <section className="crud-page">
        <header className="crud-top">
          <section>
            <h1 className="crud-title">Jogadores</h1>
            <p className="crud-subtitle">Cadastre e gerencie os jogadores da arena</p>
          </section>
          <button className="btn btn-primary" onClick={abrirNovo}>
            <i className="fa-solid fa-plus me-2" />
            Novo jogador
          </button>
        </header>

        <section className="crud-card">
          {jogadores.length === 0 ? (
            <p className="crud-empty">Nenhum jogador cadastrado.</p>
          ) : (
            <table className="crud-table">
              <thead>
                <tr>
                  <th>Nome</th>
                  <th>CPF</th>
                  <th>Idade</th>
                  <th>Status</th>
                  <th>Ações</th>
                </tr>
              </thead>
              <tbody>
                {jogadores.map((jogador) => (
                  <tr key={jogador.jogadorId}>
                    <td>{jogador.nome}</td>
                    <td>{jogador.cpf}</td>
                    <td>{jogador.idade}</td>
                    <td>
                      <span className={jogador.ativo ? 'badge-ativo' : 'badge-inativo'}>
                        {jogador.ativo ? 'Ativo' : 'Inativo'}
                      </span>
                    </td>
                    <td>
                      <button className="btn btn-sm btn-outline-primary" onClick={() => abrirEdicao(jogador)}>
                        Editar
                      </button>
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
          <article className="custom-modal">
            <header className="modal-header">
              <h2>{editando ? 'Editar jogador' : 'Novo jogador'}</h2>
              <button className='btn btn-outline-danger' type="button" onClick={() => setModalAberto(false)}>X</button>
            </header>

            <form onSubmit={handleSubmit}>
              <section className="modal-content crud-form-grid">
                <section>
                  <label>Nome</label>
                  <input className="form-control" value={nome} onChange={(e) => setNome(e.target.value)} />
                </section>
                <section>
                  <label>CPF</label>
                  <input
                    className="form-control"
                    value={cpf}
                    disabled={!!editando}
                    onChange={(e) => setCpf(AplicaMascaraCpf(e.target.value))}
                  />
                </section>
                <section>
                  <label>Idade</label>
                  <input
                    type="number"
                    min={1}
                    className="form-control"
                    value={idade}
                    onChange={(e) => setIdade(e.target.value)}
                  />
                </section>
                {editando && (
                  <section>
                    <label>Status</label>
                    <select className="form-select" value={ativo ? '1' : '0'} onChange={(e) => setAtivo(e.target.value === '1')}>
                      <option value="1">Ativo</option>
                      <option value="0">Inativo</option>
                    </select>
                  </section>
                )}
              </section>

              <footer className="modal-footer">
            <button type="button" className="btn btn-outline-secondary" onClick={() =>setModalAberto(false)}>
              Cancelar
            </button>
            <button type="submit" className="btn btn-primary" disabled={loading}>
              {loading ? 'Salvando...' : 'Criar jogador'}
            </button>
          </footer>
            </form>
          </article>
        </section>
      )}
    </>
  );
}
