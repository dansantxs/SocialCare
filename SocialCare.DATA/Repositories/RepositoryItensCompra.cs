using SocialCare.DATA.Interfaces;
using SocialCare.DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCare.DATA.Repositories
{
    public class RepositoryItensCompra : RepositoryBase<ItensCompra>, IRepositoryItensCompra
    {
        public RepositoryItensCompra(bool SaveChanges = true) : base(SaveChanges)
        {
        }

        public List<ItensCompra> SelecionarPorCompraId(int id)
        {
            return _Contexto.ItensCompra.Where(ic => ic.IdCompra == id).ToList();
        }
    }
}