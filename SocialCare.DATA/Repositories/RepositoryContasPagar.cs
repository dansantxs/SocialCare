using SocialCare.DATA.Interfaces;
using SocialCare.DATA.Models;

namespace SocialCare.DATA.Repositories
{
    public class RepositoryContasPagar : RepositoryBase<ContasPagar>, IRepositoryContasPagar
    {
        public RepositoryContasPagar(bool SaveChanges = true) : base(SaveChanges)
        {

        }

        public ContasPagar SelecionarPorIdCompra(int id)
        {
            return _Contexto.ContasPagar.FirstOrDefault(cp => cp.IdCompra == id);
        }
    }
}