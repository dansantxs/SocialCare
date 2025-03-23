using SocialCare.DATA.Models;
using Microsoft.Data.SqlClient;
using SocialCare.DATA.Interfaces;
using System.Data;

namespace SocialCare.DATA.Repositories
{
    public class RepositoryPessoasFisicas : RepositoryBase<PessoasFisicas>, IRepositoryPessoasFisicas
    {
        public RepositoryPessoasFisicas(string connectionString) : base(connectionString)
        {
        }

        public override PessoasFisicas Incluir(PessoasFisicas objeto)
        {
            throw new NotImplementedException();
        }

        public override PessoasFisicas Alterar(PessoasFisicas objeto)
        {
            throw new NotImplementedException();
        }

        public override void Excluir(PessoasFisicas objeto)
        {
        }

        public override void Excluir(params object[] variavel)
        {
        }

        protected override PessoasFisicas MapToEntity(IDataRecord record)
        {
            throw new NotImplementedException();
        }
    }
}