using SocialCare.DATA.Repositories;
using SocialCare.DATA.Repositories.SocialCare.DATA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCare.DATA.Services
{
    public class VwPessoasFisicasService
    {
        public RepositoryVwPessoasFisicas oRepositoryVwPessoasFisicas { get; set; }

        public VwPessoasFisicasService()
        {
            oRepositoryVwPessoasFisicas = new RepositoryVwPessoasFisicas();
        }
    }
}