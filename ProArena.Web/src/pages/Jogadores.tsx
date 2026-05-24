import { useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import Loading from '../components/Loading/Loading';
import { IAtualizaJogador, IJogador, IRegistraJogador } from '../interfaces/IJogador';
import { AtualizaJogador, BuscaTodosJogadores, RegistraJogador } from '../services/JogadorService';
import { ModalJogador } from '../components/Modal/ModalJogador';
import '../styles/crud.css';

export default function Jogadores() {
  const [jogadores, setJogadores] = useState<IJogador[]>([]);
  const [loading, setLoading] = useState(true);
  const [modalAberto, setModalAberto] = useState(false);
  const [editando, setEditando] = useState<IJogador | null>(null);

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
    setModalAberto(true);
  }

  function abrirEdicao(jogador: IJogador) {
    setEditando(jogador);
    setModalAberto(true);
  }

  async function handleSalvar(dados: IRegistraJogador | IAtualizaJogador) {
    try {
      if (editando) {
        const response = await AtualizaJogador(dados as IAtualizaJogador);
        if (response.data.erro) {
          toast.error(response.data.mensagem);
          return;
        }
        toast.success(response.data.mensagem);
      } else {
        const response = await RegistraJogador(dados as IRegistraJogador);
        if (response.data.erro) {
          toast.error(response.data.mensagem);
          return;
        }
        toast.success(response.data.mensagem);
      }
    } catch {
      toast.error('Erro ao salvar jogador.');
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

      <ModalJogador
        isOpen={modalAberto}
        onClose={() => setModalAberto(false)}
        onSuccess={carregarJogadores}
        jogadorEditando={editando}
        onSalvar={handleSalvar}
      />
    </>
  );
}
