import { Footer } from '../components/Footer';
import { Navbar } from '../components/Navbar';
import '../styles/pages-styles/LoginPage.css';

export function LoginPage() {
    return (
        <>
            <Navbar/>
            <main>
                <section className="login-page">
                    <div className="box-login">
                        <div className="group-item">
                            <p>Bem-vindo!</p>
                            <form method='post'>
                                <div>
                                    <label>E-mail</label>
                                    <input type='email' placeholder='seuemail@gmail.com'></input>
                                </div>
                                <div>
                                    <label>Senha</label>
                                    <input placeholder='*********' type='password'></input>
                                </div>
                                <button type='submit'>Entrar</button>
                            </form>
                            <div className='esqueceu-senha'>
                                <a href=''>Esqueceu sua senha?</a>
                            </div>
                        </div>
                    </div>
                </section>
            </main>
            <Footer/>
        </>
    )
}