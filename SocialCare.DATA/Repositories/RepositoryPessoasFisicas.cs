using SocialCare.DATA.Interfaces;
using SocialCare.DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCare.DATA.Repositories
{
    public class RepositoryPessoasFisicas : RepositoryBase<PessoasFisicas>, IRepositoryPessoasFisicas
    {
        public RepositoryPessoasFisicas(bool SaveChanges = true) : base(SaveChanges)
        {

        }
    }
}