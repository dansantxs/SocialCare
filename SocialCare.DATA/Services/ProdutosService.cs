using SocialCare.DATA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCare.DATA.Services
{
    public class ProdutosService
    {
        public RepositoryProdutos oRepositoryProdutos { get; set; }

        public ProdutosService()
        {
            oRepositoryProdutos = new RepositoryProdutos();
        }
    }
}