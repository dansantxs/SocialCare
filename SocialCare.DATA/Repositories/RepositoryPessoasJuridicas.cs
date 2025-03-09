using SocialCare.DATA.Interfaces;
using SocialCare.DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCare.DATA.Repositories
{
    public class RepositoryPessoasJuridicas : RepositoryBase<PessoasJuridicas>, IRepositoryPessoasJuridicas
    {
        public RepositoryPessoasJuridicas(bool SaveChanges = true) : base(SaveChanges)
        {

        }
    }
}