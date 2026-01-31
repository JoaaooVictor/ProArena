import '../styles/login.css'
import bgLogin from '../assets/home-futvolei.png'
import { LoginUsuario } from '../services/AuthService'
import { useState } from 'react'
import { toast } from 'react-toastify'
import { useNavigate } from 'react-router-dom'
import Loading from '../components/Loading'

export default function Auth() {
  const navigate = useNavigate()
  const [email, setEmail] = useState('')
  const [senha, setSenha] = useState('')
  const [loading, setLoading] = useState(false)

  async function Login() {
    try {
      setLoading(true)

      const response = await LoginUsuario({ email, senha })

      if (!response.data.erro) {
        localStorage.setItem('token', response.data.objeto)
        navigate('/dashboard')
      }

    } catch (erro: any) {
      toast.error(erro.response?.data?.mensagem ?? 'Erro inesperado')
    } finally {
      setLoading(false)
    }
  }

  return (
    <>
      {loading && <Loading />}

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

          <div className="form-group">
            <label>Email</label>
            <input
              type="email"
              value={email}
              onChange={e => setEmail(e.target.value)}
            />
          </div>

          <div className="form-group">
            <label>Senha</label>
            <input
              type="password"
              value={senha}
              onChange={e => setSenha(e.target.value)}
            />
          </div>

          <button
            className="btn-login"
            onClick={Login}
            disabled={loading}
          >
            {loading ? 'Entrando...' : 'Entrar'}
          </button>

          <p className="signup">
            Não possui conta? <a href="/register">Registre-se</a>
          </p>
        </div>
      </div>
    </>
  )
}
