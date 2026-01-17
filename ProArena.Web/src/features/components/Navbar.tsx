import { Link } from 'react-router-dom';
import '../styles/components-styles/Navbar.css';

export function Navbar() {
    return (
        <>
            <div className="container header-content">
                <h3 className="logo">ProArena</h3>

                <nav>
                    <ul className="nav-list">
                        <li><Link to='/campeonatos'>Campeonatos</Link></li>
                        <li><Link to='/'>Jogos</Link></li>
                        <li><Link to='/sobre'>Sobre</Link></li>
                    </ul>
                </nav>
            </div>
        </>
    )
}