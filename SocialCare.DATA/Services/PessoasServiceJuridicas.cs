using SocialCare.DATA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCare.DATA.Services
{
    public class PessoasJuridicasService
    {
        public RepositoryPessoasJuridicas oRepositoryPessoasJuridicas { get; set; }

        public PessoasJuridicasService()
        {
            oRepositoryPessoasJuridicas = new RepositoryPessoasJuridicas();
        }
    }
}
