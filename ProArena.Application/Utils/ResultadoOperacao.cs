using ProArena.Application.Enums;

namespace ProArena.Application.Utils
{
    public class ResultadoOperacao
    {
        public bool Erro { get; set; } = false;
        public string Mensagem { get; set; } = string.Empty;
        public object? Objeto { get; set; }
        public TipoErroOperacao TipoErro { get; set; }

        public ResultadoOperacao(bool erro, string mensagem, TipoErroOperacao tipoErro,  object? obj = null)
        {
            Erro = erro;
            Mensagem = mensagem;
            Objeto = obj;
        }

        public static ResultadoOperacao Concluido(string mensagem, TipoErroOperacao tipoErro, object? obj = null)
        {
            return new ResultadoOperacao(false, mensagem, tipoErro, obj);
        }

        public static ResultadoOperacao Falhou(string mensagem, TipoErroOperacao tipoErro)
        {
            return new ResultadoOperacao(true, mensagem, tipoErro);
        }
    }
}
