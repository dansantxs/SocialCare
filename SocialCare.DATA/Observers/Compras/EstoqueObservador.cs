using SocialCare.DATA.Models;

namespace SocialCare.DATA.Observer
{
    public class EstoqueObservador : IObservador
    {
        public void Update(ItensCompra item, DBConnection _dbConnection)
        {
            var produto = new Produtos().SelecionarPorId(item.IdProduto, _dbConnection);

            produto.Estoque += item.Quantidade;

            produto.Alterar(_dbConnection);
        }
    }
}