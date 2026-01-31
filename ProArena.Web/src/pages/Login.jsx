import '../styles/login.css'
import bgLogin from '../assets/dashboard-futvolei.png'

export default function Login() {
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
          <input type="email" placeholder="Enter your email" />
        </div>

        <div className="form-group">
          <label>Senha</label>
          <input type="password" placeholder="Enter your password" />
        </div>

        <div className="login-actions">
          <label>
            <input type="checkbox" /> 
          </label>
          <a href="#">Esqueceu sua senha?</a>
        </div>

        <button className="btn-login">Sign In</button>

        <p className="signup">
          Não possui conta? <a href="/register">Sign Up</a>
        </p>
      </div>
    </div>
  )
}
