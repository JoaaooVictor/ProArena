import '../styles/pages-styles/HomePage.css'
import { Navbar } from '../components/Navbar';
import { Footer } from '../components/Footer';

export function HomePage() {
    return (
        <>
            <Navbar />
            <main>
                <section className="hero">
                    <div className="container hero-content">
                        <div className="hero-text">
                            <h1>ProArena conectando quem joga!</h1>
                            <p>
                                Organize campeonatos, encontre parceiros e viva o melhor do
                                futevôlei em um só lugar.
                            </p>
                        </div>
                        <div className="group-botoes">
                            <button className="botao-home">Encontrar Quadras</button>
                            <button className="botao-home">Encontrar Jogadores</button>
                        </div>
                        <div className="container features-grid">
                            <div className="feature-card">
                                <h3>🏆 Campeonatos</h3>
                                <p>Crie e participe de campeonatos com facilidade.</p>
                            </div>

                            <div className="feature-card">
                                <h3>📊 Rankings</h3>
                                <p>Acompanhe desempenho e evolução.</p>
                            </div>

                            <div className="feature-card">
                                <h3>📍Localização</h3>
                                <p>Encontre a quadra mais proxima de você.</p>
                            </div>
                        </div>
                    </div>
                </section>
            </main>
            <Footer />
        </>
    )
}
