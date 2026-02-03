import '../styles/registrar.css'
import back from '../assets/home-futvolei.png'
import React, { useState } from 'react'
import { toast } from 'react-toastify';
import { IRegistraUsuario } from '../interfaces/IUsuario';
import { RegistraUsuario } from '../services/UsuarioService';
import { AplicaMascaraCpf } from '../utils/Formatacao';
import Loading from '../components/Loading';
import { ValidaCampos } from '../utils/Validador';

const Registrar = () => {

    const [nome, setNome] = useState('');
    const [email, setEmail] = useState('');
    const [cpf, setCpf] = useState('');
    const [senha, setSenha] = useState('');
    const [confirmaSenha, setConfirmaSenha] = useState('');
    const [loading, setLoading] = useState(false);

    async function formSubmit(e: React.SubmitEvent) {
        e.preventDefault();

        if (senha != confirmaSenha) {
            return toast.error("As senhas não conferem");
        }

        const registraUsuario: IRegistraUsuario = {
            nome,
            cpf,
            email,
            senha
        }

        const erro = ValidaCampos(nome, cpf, email, senha, confirmaSenha)
        if (erro) {
            toast.error(erro)
            return
        }

        try {
            setLoading(true);
            var response = await RegistraUsuario(registraUsuario);

            if (response.data.erro) {
                toast.error(response.data.mensagem);
            }

            if (!response.data.erro) {
                toast.success(response.data.mensagem);
            }
        } catch (erro: any) {
            toast.error('Erro inesperado ao cadastrar usuário');
        }
        finally {
            setLoading(false);
        }
    }

    return (
        <>
            {loading && <Loading />}
            <div className="registrar-full" style={{ backgroundImage: `url(${back})` }}>
                <div className='registrar-overlay'>
                    <div className='registrar-box'>
                        <form onSubmit={formSubmit}>
                            <h2>Registre-se</h2>
                            <p className="registrar-subtitle">
                                Crie sua conta para gerenciar arenas e campeonatos
                            </p>

                            <div className='row'>
                                <div className='col-9'>
                                    <label>Nome</label>
                                    <input
                                        className='form-control'
                                        type="text"
                                        placeholder='João Mario'
                                        onChange={e => setNome(e.target.value)}
                                    />
                                </div>
                                <div className='col-3'>
                                    <label>CPF</label>
                                    <input
                                        className='form-control'
                                        type="text"
                                        placeholder='xxx.xxx.xxx-xx'
                                        value={cpf}
                                        onChange={e => setCpf(AplicaMascaraCpf(e.target.value))}
                                    />
                                </div>
                            </div>
                            <div className='row'>
                            </div>
                            <div className='row'>
                                <div className='col-12'>
                                    <label>E-mail</label>
                                    <input
                                        className='form-control'
                                        type="text"
                                        placeholder='seumail@outlook.com'
                                        onChange={e => setEmail(e.target.value)}
                                    />
                                </div>
                            </div>
                            <div className='row'>
                                <div className='col-6'>
                                    <label>Senha</label>
                                    <input
                                        className='form-control'
                                        type="text"
                                        placeholder='***************'
                                        onChange={e => setSenha(e.target.value)}
                                    />
                                </div>
                                <div className='col-md-6'>
                                    <label>Confirme sua senha</label>
                                    <input
                                        className='form-control'
                                        type="text"
                                        placeholder='***************'
                                        onChange={e => setConfirmaSenha(e.target.value)}
                                    />
                                </div>
                            </div>
                            <div className='row'>
                                <div className='col-md-12 pt-4 text-center'>
                                    <button type='submit' className='btn btn-registrar form-control'>Registrar</button>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </>
    )
}

export default Registrar