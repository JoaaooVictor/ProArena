import { useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import Loading from '../components/Loading/Loading';
import {
  IMovimentacaoFinanceira,
  IRegistraMovimentacao,
  IAtualizaMovimentacao,
  IResumoFluxoCaixa,
  TipoMovimentacaoFinanceira
} from '../interfaces/IFluxoCaixa';
import { ICampeonato } from '../interfaces/ICampeonato';
import { BuscaTodosCampeonatos } from '../services/CampeonatoService';
import {
  BuscaResumoFluxoCaixa,
  BuscaTodasMovimentacoes,
  RegistraMovimentacao,
  AtualizaMovimentacao,
  RemoveMovimentacao
} from '../services/FluxoCaixaService';
import { ModalFluxoCaixa } from '../components/Modal/ModalFluxoCaixa';
import { FormataData, FormataMoeda } from '../utils/Formatacao';
import '../styles/crud.css';

export default function FluxoCaixa() {
  const [movimentacoes, setMovimentacoes] = useState<IMovimentacaoFinanceira[]>([]);
  const [resumo, setResumo] = useState<IResumoFluxoCaixa | null>(null);
  const [campeonatos, setCampeonatos] = useState<ICampeonato[]>([]);
  const [loading, setLoading] = useState(true);
  const [modalAberto, setModalAberto] = useState(false);
  const [editando, setEditando] = useState<IMovimentacaoFinanceira | null>(null);

  async function carregarDados() {
    try {
      setLoading(true);
      const [movResponse, resumoResponse, campResponse] = await Promise.all([
        BuscaTodasMovimentacoes(),
        BuscaResumoFluxoCaixa(),
        BuscaTodosCampeonatos(),
      ]);

      if (!movResponse.data.erro) {
        setMovimentacoes(movResponse.data.objeto ?? []);
      }

      if (!resumoResponse.data.erro) {
        setResumo(resumoResponse.data.objeto ?? null);
      }

      if (!campResponse.data.erro) {
        setCampeonatos(campResponse.data.objeto ?? []);
      }
    } catch {
      toast.error('Erro ao carregar fluxo de caixa.');
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    carregarDados();
  }, []);

  async function handleSalvar(dados: IRegistraMovimentacao | IAtualizaMovimentacao) {
    try {
      if (editando) {
        const response = await AtualizaMovimentacao(dados as IAtualizaMovimentacao);
        if (response.data.erro) {
          toast.error(response.data.mensagem);
          return;
        }
        toast.success(response.data.mensagem);
      } else {
        const response = await RegistraMovimentacao(dados as IRegistraMovimentacao);
        if (response.data.erro) {
          toast.error(response.data.mensagem);
          return;
        }
        toast.success(response.data.mensagem);
      }
    } catch {
      toast.error('Erro ao salvar movimentação.');
    }
  }

  function abrirNovo() {
    setEditando(null);
    setModalAberto(true);
  }

  function abrirEdicao(movimentacao: IMovimentacaoFinanceira) {
    setEditando(movimentacao);
    setModalAberto(true);
  }

  async function handleRemover(id: number) {
    if (!window.confirm('Deseja remover esta movimentação?')) return;

    try {
      setLoading(true);
      const response = await RemoveMovimentacao(id);
      if (response.data.erro) {
        toast.error(response.data.mensagem);
        return;
      }
      toast.success(response.data.mensagem);
      await carregarDados();
    } catch {
      toast.error('Erro ao remover movimentação.');
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
            <h1 className="crud-title">Fluxo de Caixa</h1>
            <p className="crud-subtitle">Controle entradas e saídas financeiras da quadra</p>
          </section>
          <button className="btn btn-primary" onClick={abrirNovo}>
            <i className="fa-solid fa-plus me-2" />
            Nova movimentação
          </button>
        </header>

        <section className="resumo-cards">
          <article className="resumo-card">
            <small>Entradas</small>
            <strong className="text-success">{FormataMoeda(resumo?.totalEntradas ?? 0)}</strong>
          </article>
          <article className="resumo-card">
            <small>Saídas</small>
            <strong className="text-danger">{FormataMoeda(resumo?.totalSaidas ?? 0)}</strong>
          </article>
          <article className="resumo-card">
            <small>Saldo</small>
            <strong>{FormataMoeda(resumo?.saldo ?? 0)}</strong>
          </article>
        </section>

        <section className="crud-card">
          {movimentacoes.length === 0 ? (
            <p className="crud-empty">Nenhuma movimentação registrada.</p>
          ) : (
            <table className="crud-table">
              <thead>
                <tr>
                  <th>Data</th>
                  <th>Descrição</th>
                  <th>Categoria</th>
                  <th>Tipo</th>
                  <th>Valor</th>
                  <th>Ações</th>
                </tr>
              </thead>
              <tbody>
                {movimentacoes.map((item) => (
                  <tr key={item.movimentacaoFinanceiraId}>
                    <td>{FormataData(item.data)}</td>
                    <td>{item.descricao}</td>
                    <td>{item.categoria || '-'}</td>
                    <td>
                      <span className={item.tipo === TipoMovimentacaoFinanceira.Entrada ? 'badge-entrada' : 'badge-saida'}>
                        {item.tipo === TipoMovimentacaoFinanceira.Entrada ? 'Entrada' : 'Saída'}
                      </span>
                    </td>
                    <td>{FormataMoeda(item.valor)}</td>
                    <td>
                      <div className="crud-actions">
                        <button
                          className="btn btn-sm btn-outline-primary"
                          onClick={() => abrirEdicao(item)}
                        >
                          Editar
                        </button>
                        <button
                          className="btn btn-sm btn-outline-danger"
                          onClick={() => handleRemover(item.movimentacaoFinanceiraId)}
                        >
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

      <ModalFluxoCaixa
        isOpen={modalAberto}
        onClose={() => setModalAberto(false)}
        onSuccess={carregarDados}
        campeonatos={campeonatos}
        movimentacaoEditando={editando}
        onSalvar={handleSalvar}
      />
    </>
  );
}
