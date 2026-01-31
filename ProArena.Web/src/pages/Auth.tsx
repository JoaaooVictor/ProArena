import '../styles/login.css'
import bgLogin from '../assets/dashboard-futvolei.png'
import { LoginUsuario } from '../services/AuthService';
import { useState } from 'react';
import { toast } from 'react-toastify';

export default function Auth() {

  const [email, setEmail] = useState('');
  const [senha, setSenha] = useState('');

  async function Login() {
    try {
      const response = await LoginUsuario({ email, senha })

      if (!response.data.erro) {
        localStorage.setItem('token', response.data.objeto)
        toast.success(response.data.mensagem);
      }

    } catch (erro: any) {
      const mensagem = erro.response?.data?.mensagem ?? 'Erro inesperado'
      toast.error(mensagem);
    }
  }

  return (
    <div className="login-container">

      <div
        className="login-left"
        style={{ backgroundImage: `url(${bgLogin})` }}
      >
        <div className="login-left-content">
          <h1>Eleve seu nível</h1>
          <p>
            Junte-se ao principal sistema para reservas de quadras e administração de torneios.
          </p>
        </div>
      </div>

      <div className="login-right">
        <h2>Bem vindo novamente</h2>
        <p className="subtitle">
          Gerencie suas quadras e torneios com facilidade.
        </p>

        <div className="form-group">
          <label>Email</label>
          <input
            type="email"
            placeholder='seuemail@outlook.com'
            value={email}
            onChange={e => setEmail(e.target.value)}
          />
        </div>

        <div className="form-group">
          <label>Senha</label>
          <input
            type="password"
            placeholder='*******************'
            value={senha}
            onChange={e => setSenha(e.target.value)}
          />
        </div>

        <button className="btn-login" onClick={Login}>
          Entrar
        </button>

        <p className="signup">
          Não possui conta? <a href="/register">Registre-se</a>
        </p>
      </div>
    </div>
  )
}
