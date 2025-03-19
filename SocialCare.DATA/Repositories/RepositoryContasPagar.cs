using SocialCare.DATA.Interfaces;
using SocialCare.DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCare.DATA.Repositories
{
    public class RepositoryContasPagar : RepositoryBase<ContasPagar>, IRepositoryContasPagar
    {
        public RepositoryContasPagar(bool SaveChanges = true) : base(SaveChanges)
        {

        }

        public ContasPagar SelecionarPorCompraId(int id)
        {
            return _Contexto.ContasPagar.FirstOrDefault(cp => cp.IdCompra == id);
        }
    }
}