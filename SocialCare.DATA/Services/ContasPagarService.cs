using SocialCare.DATA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCare.DATA.Services
{
    public class ContasPagarService
    {
        public RepositoryContasPagar oRepositoryContasPagar { get; set; }

        public ContasPagarService()
        {
            oRepositoryContasPagar = new RepositoryContasPagar();
        }
    }
}
