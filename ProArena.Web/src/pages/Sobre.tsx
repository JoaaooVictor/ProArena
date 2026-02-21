import "../styles/Sobre.css";
import { Link } from "react-router-dom";

const Sobre = () => {
  return (
    <main className="sobre-page">
      {/* HERO */}
      <section className="sobre-hero">
        <div className="container sobre-hero-content">
          <div className="sobre-hero-text">
            <h1>Sobre o ProArena</h1>
            <p>
              O ProArena é um sistema para <strong>gerenciar quadras</strong>, organizar
              <strong> campeonatos</strong> e facilitar o dia a dia de quem administra e de quem joga.
            </p>

            <div className="sobre-actions">
              <Link className="btn btn-outline-primary" to="/campeonatos">Ver Campeonatos</Link>
              <Link className="btn btn-outline-secondary" to="/">Voltar ao Início</Link>
            </div>
          </div>

          <div className="sobre-card">
            <h3>O que dá pra fazer</h3>
            <ul>
              <li>Controlar disponibilidade e horários das quadras</li>
              <li>Criar campeonatos e acompanhar partidas</li>
              <li>Organizar equipes e jogadores</li>
              <li>Centralizar resultados e informações</li>
            </ul>
          </div>
        </div>
      </section>

      <section className="sobre-section">
        <div className="container">
          <h2>Missão, visão e valores</h2>

          <div className="sobre-grid">
            <article className="box">
              <h3>Missão</h3>
              <p>
                Simplificar a gestão de quadras e eventos esportivos com uma plataforma
                clara, rápida e confiável.
              </p>
            </article>

            <article className="box">
              <h3>Visão</h3>
              <p>
                Ser referência em organização de quadras e campeonatos, conectando arenas e atletas.
              </p>
            </article>

            <article className="box">
              <h3>Valores</h3>
              <p>
                Transparência, eficiência, respeito e evolução contínua.
              </p>
            </article>
          </div>
        </div>
      </section>

      <section className="sobre-section sobre-alt">
        <div className="container">
          <h2>Como funciona</h2>

          <div className="steps">
            <div className="step">
              <span className="step-number">1</span>
              <div>
                <h3>Cadastre e organize</h3>
                <p>Crie campeonatos e mantenha as informações centralizadas.</p>
              </div>
            </div>

            <div className="step">
              <span className="step-number">2</span>
              <div>
                <h3>Monte equipes e partidas</h3>
                <p>Adicione equipes e jogos no seu tempo (o campeonato pode começar “vazio”).</p>
              </div>
            </div>

            <div className="step">
              <span className="step-number">3</span>
              <div>
                <h3>Acompanhe tudo</h3>
                <p>Resultados, histórico, organização e próximos passos do evento.</p>
              </div>
            </div>
          </div>
        </div>
      </section>

      <section className="sobre-section">
        <div className="container">
          <h2>Dúvidas frequentes</h2>

          <div className="faq">
            <details className="faq-item">
              <summary>Posso criar um campeonato sem equipes?</summary>
              <p>Sim. Você cria o campeonato primeiro e cadastra equipes e partidas depois.</p>
            </details>

            <details className="faq-item">
              <summary>É só para futevôlei?</summary>
              <p>O foco é futevôlei, mas o sistema pode evoluir para outros esportes também.</p>
            </details>

            <details className="faq-item">
              <summary>Vai ter rankings?</summary>
              <p>Sim! É uma funcionalidade planejada (e dá pra fazer por etapas).</p>
            </details>
          </div>
        </div>
      </section>

      <section className="sobre-cta">
        <div className="container sobre-cta-content">
          <div>
            <h2>Pronto para organizar sua arena?</h2>
            <p>Comece agora e deixe o gerenciamento mais simples.</p>
          </div>

          <div className="sobre-actions">
            <Link className="btn btn-outline-primary" to="/login">Entrar</Link>
            <Link className="btn btn-outline-secondary" to="/campeonatos">Campeonatos</Link>
          </div>
        </div>
      </section>
    </main>
  );
};

export default Sobre;