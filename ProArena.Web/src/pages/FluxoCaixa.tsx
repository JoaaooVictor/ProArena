import { FormEvent, useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import Loading from '../components/Loading/Loading';
import {
  IMovimentacaoFinanceira,
  IRegistraMovimentacao,
  IResumoFluxoCaixa,
  TipoMovimentacao
} from '../interfaces/IFluxoCaixa';
import { ICampeonato } from '../interfaces/ICampeonato';
import { BuscaTodosCampeonatos } from '../services/CampeonatoService';
import {
  BuscaResumoFluxoCaixa,
  BuscaTodasMovimentacoes,
  RegistraMovimentacao,
  RemoveMovimentacao
} from '../services/FluxoCaixaService';
import { FormataData, FormataMoeda } from '../utils/Formatacao';
import '../styles/crud.css';
import '../components/Modal/Modal.css';

export default function FluxoCaixa() {
  const [movimentacoes, setMovimentacoes] = useState<IMovimentacaoFinanceira[]>([]);
  const [resumo, setResumo] = useState<IResumoFluxoCaixa | null>(null);
  const [campeonatos, setCampeonatos] = useState<ICampeonato[]>([]);
  const [loading, setLoading] = useState(true);
  const [modalAberto, setModalAberto] = useState(false);

  const [descricao, setDescricao] = useState('');
  const [valor, setValor] = useState('');
  const [data, setData] = useState(new Date().toISOString().slice(0, 10));
  const [tipo, setTipo] = useState<TipoMovimentacao>(TipoMovimentacao.Entrada);
  const [categoria, setCategoria] = useState('');
  const [campeonatoId, setCampeonatoId] = useState('');

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

  function limparFormulario() {
    setDescricao('');
    setValor('');
    setData(new Date().toISOString().slice(0, 10));
    setTipo(TipoMovimentacao.Entrada);
    setCategoria('');
    setCampeonatoId('');
  }

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();

    if (!descricao.trim() || !valor || !data) {
      toast.error('Preencha descrição, valor e data.');
      return;
    }

    const payload: IRegistraMovimentacao = {
      Descricao: descricao,
      Valor: Number(valor),
      Data: data,
      Tipo: tipo,
      Categoria: categoria || undefined,
      CampeonatoId: campeonatoId ? Number(campeonatoId) : undefined,
    };

    try {
      setLoading(true);
      const response = await RegistraMovimentacao(payload);

      if (response.data.erro) {
        toast.error(response.data.mensagem);
        return;
      }

      toast.success(response.data.mensagem);
      setModalAberto(false);
      limparFormulario();
      await carregarDados();
    } catch {
      toast.error('Erro ao registrar movimentação.');
    } finally {
      setLoading(false);
    }
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
          <button className="btn btn-primary" onClick={() => setModalAberto(true)}>
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
                      <span className={item.tipo === TipoMovimentacao.Entrada ? 'badge-entrada' : 'badge-saida'}>
                        {item.tipo === TipoMovimentacao.Entrada ? 'Entrada' : 'Saída'}
                      </span>
                    </td>
                    <td>{FormataMoeda(item.valor)}</td>
                    <td>
                      <button
                        className="btn btn-sm btn-outline-danger"
                        onClick={() => handleRemover(item.movimentacaoFinanceiraId)}
                      >
                        Excluir
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
          <article className="custom-modal modal-lg">
            <header className="modal-header">
              <h2>Nova movimentação</h2>
              <button className='btn btn-outline-danger' type="button" onClick={() => setModalAberto(false)}>X</button>
            </header>

            <form onSubmit={handleSubmit}>
              <section className="modal-content ">
                <section>
                  <label>Descrição</label>
                  <input className="form-control" value={descricao} onChange={(e) => setDescricao(e.target.value)} />
                </section>
                <section>
                  <label>Valor (R$)</label>
                  <input
                    type="number"
                    min="0.01"
                    step="0.01"
                    className="form-control"
                    value={valor}
                    onChange={(e) => setValor(e.target.value)}
                  />
                </section>
                <section>
                  <label>Data</label>
                  <input type="date" className="form-control" value={data} onChange={(e) => setData(e.target.value)} />
                </section>
                <section>
                  <label>Tipo</label>
                  <select className="form-select" value={tipo} onChange={(e) => setTipo(Number(e.target.value))}>
                    <option value={TipoMovimentacao.Entrada}>Entrada</option>
                    <option value={TipoMovimentacao.Saida}>Saída</option>
                  </select>
                </section>
                <section>
                  <label>Categoria</label>
                  <input
                    className="form-control"
                    value={categoria}
                    onChange={(e) => setCategoria(e.target.value)}
                    placeholder="Ex: Aluguel, Inscrição, Manutenção"
                  />
                </section>
                <section>
                  <label>Campeonato (opcional)</label>
                  <select className="form-select" value={campeonatoId} onChange={(e) => setCampeonatoId(e.target.value)}>
                    <option value="">Nenhum</option>
                    {campeonatos.map((camp) => (
                      <option key={camp.campeonatoId} value={camp.campeonatoId}>
                        {camp.nome}
                      </option>
                    ))}
                  </select>
                </section>
              </section>

              <footer className="modal-footer">
                <button type="button" className="btn btn-outline-secondary" onClick={() => setModalAberto(false)}>
                  Cancelar
                </button>
                <button type="submit" className="btn btn-primary">Salvar</button>
              </footer>
            </form>
          </article>
        </section>
      )}
    </>
  );
}
