using SocialCare.DATA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCare.DATA.Services
{
    public class ComprasService
    {
        public RepositoryCompras oRepositoryCompras { get; set; }

        public ComprasService()
        {
            oRepositoryCompras = new RepositoryCompras();
        }
    }
}
