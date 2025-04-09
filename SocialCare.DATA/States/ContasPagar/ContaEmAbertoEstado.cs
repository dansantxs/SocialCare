namespace SocialCare.DATA.State
{
    public class ContaEmAbertoEstado : IContaPagarEstado
    {
        public string ObterCorLinha()
        {
            return "table-primary";
        }

        public string ObterDescricaoEstado()
        {
            return "Em Aberto";
        }
    }
}