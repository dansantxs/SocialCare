using SocialCare.DATA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCare.DATA.Services
{
    public class PessoasFisicasService
    {
        public RepositoryPessoasFisicas oRepositoryPessoasFisicas { get; set; }

        public PessoasFisicasService()
        {
            oRepositoryPessoasFisicas = new RepositoryPessoasFisicas();
        }
    }
}
