using SocialCare.DATA.Repositories.SocialCare.DATA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCare.DATA.Services
{
    public class VwPessoasJuridicasService
    {
        public RepositoryVwPessoasJuridicas oRepositoryVwPessoasJuridicas { get; set; }

        public VwPessoasJuridicasService()
        {
            oRepositoryVwPessoasJuridicas = new RepositoryVwPessoasJuridicas();
        }
    }
}