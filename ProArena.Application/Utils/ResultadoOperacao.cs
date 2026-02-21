using ProArena.Domain.Enums;

namespace ProArena.Application.Utils
{
    public class ResultadoOperacao
    {
        public bool Erro { get; set; } = false;
        public string Mensagem { get; set; } = string.Empty;
        public object? Objeto { get; set; }
        public TipoErroOperacaoEnum TipoErro { get; set; }

        public ResultadoOperacao(bool erro, string mensagem, TipoErroOperacaoEnum tipoErro,  object? obj = null)
        {
            Erro = erro;
            Mensagem = mensagem;
            TipoErro = tipoErro;
            Objeto = obj;
        }

        public static ResultadoOperacao Concluido(string mensagem, TipoErroOperacaoEnum tipoErro, object? obj = null)
        {
            return new ResultadoOperacao(false, mensagem, tipoErro, obj);
        }

        public static ResultadoOperacao Falhou(string mensagem, TipoErroOperacaoEnum tipoErro)
        {
            return new ResultadoOperacao(true, mensagem, tipoErro);
        }
    }
}
