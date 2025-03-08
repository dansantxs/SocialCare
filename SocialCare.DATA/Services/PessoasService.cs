using SocialCare.DATA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCare.DATA.Services
{
    public class PessoasService
    {
        public RepositoryPessoas oRepositoryPessoas { get; set; }

        public PessoasService()
        {
            oRepositoryPessoas = new RepositoryPessoas();
        }
    }
}
