import { FormEvent, useState, useEffect } from 'react';
import { ICampeonato } from '../../interfaces/ICampeonato';
import {
  IAtualizaMovimentacao,
  IMovimentacaoFinanceira,
  IRegistraMovimentacao,
  TipoMovimentacaoFinanceira
} from '../../interfaces/IFluxoCaixa';
import './Modal.css';

interface ModalFluxoCaixaProps {
  isOpen: boolean;
  onClose: () => void;
  onSuccess: () => void;
  campeonatos: ICampeonato[];
  movimentacaoEditando?: IMovimentacaoFinanceira | null;
  onSalvar: (dados: IRegistraMovimentacao | IAtualizaMovimentacao) => Promise<void>;
}

export function ModalFluxoCaixa({ isOpen, onClose, onSuccess, campeonatos, movimentacaoEditando, onSalvar }: ModalFluxoCaixaProps) {
  const [descricao, setDescricao] = useState('');
  const [valor, setValor] = useState('');
  const [data, setData] = useState(new Date().toISOString().slice(0, 10));
  const [tipo, setTipo] = useState<TipoMovimentacaoFinanceira>(TipoMovimentacaoFinanceira.Entrada);
  const [categoria, setCategoria] = useState('');
  const [campeonatoId, setCampeonatoId] = useState('');
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (isOpen) {
      if (movimentacaoEditando) {
        setDescricao(movimentacaoEditando.descricao);
        setValor(String(movimentacaoEditando.valor));
        setData(movimentacaoEditando.data.slice(0, 10));
        setTipo(movimentacaoEditando.tipo as TipoMovimentacaoFinanceira);
        setCategoria(movimentacaoEditando.categoria || '');
        setCampeonatoId(movimentacaoEditando.campeonatoId ? String(movimentacaoEditando.campeonatoId) : '');
      } else {
        setDescricao('');
        setValor('');
        setData(new Date().toISOString().slice(0, 10));
        setTipo(TipoMovimentacaoFinanceira.Entrada);
        setCategoria('');
        setCampeonatoId('');
      }
    }
  }, [isOpen, movimentacaoEditando]);

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();

    if (!descricao.trim() || !valor || !data) {
      return;
    }

    try {
      setLoading(true);

      const basePayload = {
        Descricao: descricao,
        Valor: Number(valor),
        Data: data,
        Tipo: tipo,
        Categoria: categoria || undefined,
        CampeonatoId: campeonatoId ? Number(campeonatoId) : undefined,
      };

      const payload = movimentacaoEditando
        ? { ...basePayload, MovimentacaoFinanceiraId: movimentacaoEditando.movimentacaoFinanceiraId } as IAtualizaMovimentacao
        : basePayload as IRegistraMovimentacao;

      await onSalvar(payload);
      onSuccess();
      onClose();
    } finally {
      setLoading(false);
    }
  }

  if (!isOpen) return null;

  return (
    <section className="modal-overlay">
      <article className="custom-modal modal-lg">
        <header className="modal-header">
          <h2>{movimentacaoEditando ? 'Editar movimentação' : 'Nova movimentação'}</h2>
          <button type="button" onClick={onClose}>X</button>
        </header>

        <form onSubmit={handleSubmit}>
          <section className="modal-content crud-form-grid">
            <section>
              <label>Descrição</label>
              <input
                className="form-control"
                value={descricao}
                onChange={(e) => setDescricao(e.target.value)}
                placeholder="Descrição da movimentação"
              />
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
                placeholder="0,00"
              />
            </section>
            <section>
              <label>Data</label>
              <input
                type="date"
                className="form-control"
                value={data}
                onChange={(e) => setData(e.target.value)}
              />
            </section>
            <section>
              <label>Tipo</label>
              <select
                className="form-select"
                value={tipo}
                onChange={(e) => setTipo(Number(e.target.value) as TipoMovimentacaoFinanceira)}
              >
                <option value={TipoMovimentacaoFinanceira.Entrada}>Entrada</option>
                <option value={TipoMovimentacaoFinanceira.Saida}>Saída</option>
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
              <select
                className="form-select"
                value={campeonatoId}
                onChange={(e) => setCampeonatoId(e.target.value)}
              >
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
            <button type="button" className="btn btn-outline-secondary" onClick={onClose}>
              Cancelar
            </button>
            <button type="submit" className="btn btn-primary" disabled={loading}>
              {loading ? 'Salvando...' : (movimentacaoEditando ? 'Atualizar' : 'Salvar')}
            </button>
          </footer>
        </form>
      </article>
    </section>
  );
}
