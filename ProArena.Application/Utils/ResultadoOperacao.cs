namespace ProArena.Application.Utils
{
    public class ResultadoOperacao
    {
        public bool Erro { get; set; } = false;
        public string Mensagem { get; set; } = string.Empty;
        public object? Objeto { get; set; }

        public ResultadoOperacao(bool erro, string mensagem, object? obj = null)
        {
            Erro = erro;
            Mensagem = mensagem;
            Objeto = obj;
        }

        public static ResultadoOperacao Concluido(string mensagem, object? obj = null)
        {
            return new ResultadoOperacao(false, mensagem, obj);
        }

        public static ResultadoOperacao Falhou(string mensagem)
        {
            return new ResultadoOperacao(true, mensagem);
        }
    }
}
