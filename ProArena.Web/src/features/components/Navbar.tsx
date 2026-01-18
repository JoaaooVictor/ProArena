import { Link } from 'react-router-dom';
import '../styles/components-styles/Navbar.css';

export function Navbar() {
    return (
        <>
        <header className="header">
            <div className="container header-content">
                <h3 className="logo">ProArena</h3>

                <nav>
                    <ul className="nav-list">
                        {/* <li><Link to='/campeonatos'>Campeonatos</Link></li>
                        <li><Link to='/'>Jogos</Link></li>
                        <li><Link to='/sobre'>Sobre</Link></li> */}
                        <li className='botao-login'><Link to="/login">Entrar</Link></li>
                        <li className='botao-campeonato'><Link to="/login">Novo Campeonato</Link></li>
                    </ul>
                </nav>
            </div>
        </header>
        </>
    )
}