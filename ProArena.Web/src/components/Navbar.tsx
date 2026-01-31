import '../styles/navbar.css';
import { Link } from 'react-router-dom';

export default function Navbar() {
    return (
        <nav className="navbar">
            <div className="container">

                <span className="navbar-brand">ProArena</span>

                <ul className="navbar-nav flex-row gap-3">
                    <li className="nav-item"><Link to="/arenas">Arenas</Link></li>
                    <li className="nav-item"><Link to="/campeonatos">Campeonatos</Link></li>
                    <li className="nav-item"><Link to="/sobre">Sobre</Link></li>
                </ul>

                <div>
                    <button className="nav-item btn btn-outline-secondary me-2"><Link to="/login">Login</Link></button>
                    <button className="nav-item btn btn-outline-primary"><Link to="/registrar">Registre-se</Link></button>
                </div>
            </div>
        </nav>
    )
}
