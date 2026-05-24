import { FormEvent, useState } from 'react';
import { toast } from 'react-toastify';
import { ICriaCampeonato } from '../../interfaces/ICampeonato';
import { CriaCampeonato } from '../../services/CampeonatoService';
import './Modal.css';

interface ModalCampeonatoProps {
  isOpen: boolean;
  onClose: () => void;
  onSuccess?: () => void;
}

const formInicial: ICriaCampeonato = {
  Nome: '',
  Descricao: '',
  DataInicio: '',
  DataFim: '',
  DataInicioInscricao: '',
  DataFimInscricao: '',
};

export function ModalCampeonato({ isOpen, onClose, onSuccess }: ModalCampeonatoProps) {
  const [form, setForm] = useState<ICriaCampeonato>(formInicial);
  const [loading, setLoading] = useState(false);

  if (!isOpen) return null;

  function atualizaCampo(campo: keyof ICriaCampeonato, valor: string) {
    setForm((atual) => ({ ...atual, [campo]: valor }));
  }

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();

    if (!form.Nome.trim()) {
      toast.error('Informe o nome do campeonato.');
      return;
    }

    try {
      setLoading(true);
      const response = await CriaCampeonato(form);

      if (response.data.erro) {
        toast.error(response.data.mensagem);
        return;
      }

      toast.success(response.data.mensagem);
      setForm(formInicial);
      onSuccess?.();
      onClose();
    } catch {
      toast.error('Erro ao criar campeonato.');
    } finally {
      setLoading(false);
    }
  }

  return (
    <section className="modal-overlay">
      <article className="custom-modal modal-lg">
        <header className="modal-header">
          <h2>Novo Campeonato</h2>
          <button className='btn btn-outline-danger' type="button" onClick={onClose}>X</button>
        </header>

        <form onSubmit={handleSubmit}>
          <section className="modal-content crud-form-grid">
            <section>
              <label>Nome</label>
              <input
                className="form-control"
                value={form.Nome}
                onChange={(e) => atualizaCampo('Nome', e.target.value)}
                placeholder="Ex: Copa Verão 2026"
              />
            </section>

            <section>
              <label>Descrição</label>
              <textarea
                className="form-control"
                rows={3}
                value={form.Descricao}
                onChange={(e) => atualizaCampo('Descricao', e.target.value)}
                placeholder="Detalhes do campeonato"
              />
            </section>

            <section className="row g-3">
              <section className="col-md-6">
                <label>Início das inscrições</label>
                <input
                  type="date"
                  className="form-control"
                  value={form.DataInicioInscricao}
                  onChange={(e) => atualizaCampo('DataInicioInscricao', e.target.value)}
                />
              </section>
              <section className="col-md-6">
                <label>Fim das inscrições</label>
                <input
                  type="date"
                  className="form-control"
                  value={form.DataFimInscricao}
                  onChange={(e) => atualizaCampo('DataFimInscricao', e.target.value)}
                />
              </section>
            </section>

            <section className="row g-3">
              <section className="col-md-6">
                <label>Data de início</label>
                <input
                  type="date"
                  className="form-control"
                  value={form.DataInicio}
                  onChange={(e) => atualizaCampo('DataInicio', e.target.value)}
                />
              </section>
              <section className="col-md-6">
                <label>Data de término</label>
                <input
                  type="date"
                  className="form-control"
                  value={form.DataFim}
                  onChange={(e) => atualizaCampo('DataFim', e.target.value)}
                />
              </section>
            </section>
          </section>

          <footer className="modal-footer">
            <button type="button" className="btn btn-outline-secondary" onClick={onClose}>
              Cancelar
            </button>
            <button type="submit" className="btn btn-primary" disabled={loading}>
              {loading ? 'Salvando...' : 'Criar campeonato'}
            </button>
          </footer>
        </form>
      </article>
    </section>
  );
}
