import { FormEvent, useState, useEffect } from 'react';
import { IAtualizaJogador, IJogador, IRegistraJogador } from '../../interfaces/IJogador';
import { AplicaMascaraCpf } from '../../utils/Formatacao';
import './Modal.css';

interface ModalJogadorProps {
  isOpen: boolean;
  onClose: () => void;
  onSuccess: () => void;
  jogadorEditando?: IJogador | null;
  onSalvar: (dados: IRegistraJogador | IAtualizaJogador) => Promise<void>;
}

export function ModalJogador({ isOpen, onClose, onSuccess, jogadorEditando, onSalvar }: ModalJogadorProps) {
  const [nome, setNome] = useState('');
  const [cpf, setCpf] = useState('');
  const [idade, setIdade] = useState('');
  const [ativo, setAtivo] = useState(true);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (jogadorEditando) {
      setNome(jogadorEditando.nome);
      setCpf(jogadorEditando.cpf);
      setIdade(String(jogadorEditando.idade));
      setAtivo(jogadorEditando.ativo);
    } else {
      setNome('');
      setCpf('');
      setIdade('');
      setAtivo(true);
    }
  }, [jogadorEditando, isOpen]);

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();

    if (!nome.trim() || !cpf.trim() || !idade) {
      return;
    }

    try {
      setLoading(true);

      if (jogadorEditando) {
        const payload: IAtualizaJogador = {
          JogadorId: jogadorEditando.jogadorId,
          Nome: nome,
          Idade: Number(idade),
          Ativo: ativo,
        };
        await onSalvar(payload);
      } else {
        const payload: IRegistraJogador = {
          Nome: nome,
          Cpf: cpf,
          Idade: Number(idade),
        };
        await onSalvar(payload);
      }

      onSuccess();
      onClose();
    } finally {
      setLoading(false);
    }
  }

  if (!isOpen) return null;

  return (
    <section className="modal-overlay">
      <article className="custom-modal">
        <header className="modal-header">
          <h2>{jogadorEditando ? 'Editar jogador' : 'Novo jogador'}</h2>
          <button type="button" onClick={onClose}>X</button>
        </header>

        <form onSubmit={handleSubmit}>
          <section className="modal-content">
            <section>
              <label>Nome</label>
              <input
                className="form-control"
                value={nome}
                onChange={(e) => setNome(e.target.value)}
                placeholder="Nome do jogador"
              />
            </section>
            <section>
              <label>CPF</label>
              <input
                className="form-control"
                value={cpf}
                disabled={!!jogadorEditando}
                onChange={(e) => setCpf(AplicaMascaraCpf(e.target.value))}
                placeholder="xxx.xxx.xxx-xx"
              />
            </section>
            <section>
              <label>Idade</label>
              <input
                type="number"
                min={1}
                max={120}
                className="form-control"
                value={idade}
                onChange={(e) => setIdade(e.target.value)}
                placeholder="Idade"
              />
            </section>
            {jogadorEditando && (
              <section>
                <label>Status</label>
                <select
                  className="form-select"
                  value={ativo ? '1' : '0'}
                  onChange={(e) => setAtivo(e.target.value === '1')}
                >
                  <option value="1">Ativo</option>
                  <option value="0">Inativo</option>
                </select>
              </section>
            )}
          </section>

          <footer className="modal-footer">
            <button type="button" className="btn btn-outline-secondary" onClick={onClose}>
              Cancelar
            </button>
            <button type="submit" className="btn btn-primary" disabled={loading}>
              {loading ? 'Salvando...' : (jogadorEditando ? 'Atualizar' : 'Criar jogador')}
            </button>
          </footer>
        </form>
      </article>
    </section>
  );
}
