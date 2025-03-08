using SocialCare.DATA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCare.DATA.Services
{
    public class ItensCompraService
    {
        public RepositoryItensCompra oRepositoryItensCompra { get; set; }

        public ItensCompraService()
        {
            oRepositoryItensCompra = new RepositoryItensCompra();
        }
    }
}
