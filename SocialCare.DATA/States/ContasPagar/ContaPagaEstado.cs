namespace SocialCare.DATA.State
{
    public class ContaPagaEstado : IContaPagarEstado
    {
        public string ObterCorLinha()
        {
            return "table-success";
        }

        public string ObterDescricaoEstado()
        {
            return "Paga";
        }
    }
}