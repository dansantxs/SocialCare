namespace SocialCare.DATA.State
{
    public static class ContasPagarStateExtension
    {
        public static IContaPagarEstado ObterEstado(this SocialCare.DATA.Models.ContasPagar conta)
        {
            if (conta.DataPagamento.HasValue)
            {
                return new ContaPagaEstado();
            }

            if (conta.DataVencimento.Date < DateTime.Now.Date)
            {
                return new ContaVencidaEstado();
            }

            return new ContaEmAbertoEstado();
        }
    }
}