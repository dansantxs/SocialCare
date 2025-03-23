using SocialCare.DATA.Models;
using Microsoft.Data.SqlClient;
using SocialCare.DATA.Interfaces;
using System.Data;

namespace SocialCare.DATA.Repositories
{
    public class RepositoryPessoasJuridicas : RepositoryBase<PessoasJuridicas>, IRepositoryPessoasJuridicas
    {
        public RepositoryPessoasJuridicas(string connectionString) : base(connectionString)
        {
        }

        public override PessoasJuridicas Incluir(PessoasJuridicas objeto)
        {
            throw new NotImplementedException();
        }

        public override PessoasJuridicas Alterar(PessoasJuridicas objeto)
        {
            throw new NotImplementedException();
        }

        public override void Excluir(PessoasJuridicas objeto)
        {
        }

        public override void Excluir(params object[] variavel)
        {
        }

        protected override PessoasJuridicas MapToEntity(IDataRecord record)
        {
            throw new NotImplementedException();
        }
    }
}