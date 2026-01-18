import { Footer } from '../components/Footer'
import { Navbar } from '../components/Navbar'
import '../styles/pages-styles/SobrePage.css'

export function SobrePage() {
    return (
        <>
            <Navbar />
            <main>
                <section className="sobre-container">
                    <header className="sobre-hero">
                        <h1>Sobre a ProArena</h1>
                        <p>
                            Conectamos atletas, organizadores e apaixonados por esporte em uma
                            plataforma simples, moderna e eficiente.
                        </p>
                    </header>

                    <div className="sobre-conteudo">
                        <div className="sobre-texto">
                            <h2>Nossa missão</h2>
                            <p>
                                A ProArena nasceu para facilitar a gestão de campeonatos,
                                promover o esporte e aproximar pessoas que vivem a mesma paixão.
                            </p>

                            <h2>O que fazemos</h2>
                            <ul>
                                <li>🏆 Organização de campeonatos</li>
                                <li>🤝 Conexão entre atletas e parceiros</li>
                                <li>📊 Gestão e visualização de dados esportivos</li>
                                <li>📅 Planejamento de eventos</li>
                            </ul>
                        </div>

                        <div className="sobre-destaques">
                            <div className="card">
                                <h3>⚡ Simples</h3>
                                <p>Interface intuitiva e fácil de usar.</p>
                            </div>

                            <div className="card">
                                <h3>🚀 Moderna</h3>
                                <p>Tecnologia atual para melhor desempenho.</p>
                            </div>

                            <div className="card">
                                <h3>🏅 Esportiva</h3>
                                <p>Feita por quem vive o esporte.</p>
                            </div>
                        </div>
                    </div>
                </section>
            </main>
            <Footer/>
        </>
    )
}
