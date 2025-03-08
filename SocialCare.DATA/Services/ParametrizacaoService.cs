using SocialCare.DATA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCare.DATA.Services
{
    public class ParametrizacaoService
    {
        public RepositoryParametrizacao oRepositoryParametrizacao { get; set; }

        public ParametrizacaoService()
        {
            oRepositoryParametrizacao = new RepositoryParametrizacao();
        }
    }
}