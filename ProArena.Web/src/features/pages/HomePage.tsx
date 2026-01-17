import '../styles/HomePage.css'
import futvoleiImg from '../assets/home-page-quadra.jpeg';

export function HomePage() {
    return (
        <>
            <header className="header">
                <div className="container header-content">
                    <h3 className="logo">ProArena</h3>

                    <nav>
                        <ul className="nav-list">
                            <li>Campeonatos</li>
                            <li>Parcerias</li>
                            <li>Sobre</li>
                        </ul>
                    </nav>
                </div>
            </header>

            <section className="hero">
                <div className="container hero-content">
                    <div className="hero-text">
                        <h1>Conectando atletas ao futevôlei</h1>
                        <p>
                            Organize campeonatos, encontre parceiros e viva o melhor do
                            futevôlei em um só lugar.
                        </p>
                    </div>

                    <div className="hero-image">
                        <img
                            alt="Jogo de futevôlei na praia"
                            src={futvoleiImg}
                        />
                    </div>
                </div>
            </section>

            <section className="features">
                <div className="container features-grid">
                    <div className="feature-card">
                        <h3>🏆 Campeonatos</h3>
                        <p>Crie e participe de campeonatos com facilidade.</p>
                    </div>

                    <div className="feature-card">
                        <h3>📊 Rankings</h3>
                        <p>Acompanhe desempenho e evolução.</p>
                    </div>
                </div>
            </section>
        </>
    )
}
