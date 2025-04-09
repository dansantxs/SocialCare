namespace SocialCare.DATA.State
{
    public class ContaVencidaEstado : IContaPagarEstado
    {
        public string ObterCorLinha()
        {
            return "table-danger";
        }

        public string ObterDescricaoEstado()
        {
            return "Vencida";
        }
    }
}