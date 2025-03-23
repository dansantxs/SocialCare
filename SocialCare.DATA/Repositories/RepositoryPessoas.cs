using SocialCare.DATA.Models;
using Microsoft.Data.SqlClient;
using SocialCare.DATA.Interfaces;
using System.Data;

namespace SocialCare.DATA.Repositories
{
    public class RepositoryPessoas : RepositoryBase<Pessoas>, IRepositoryPessoas
    {
        public RepositoryPessoas(string connectionString) : base(connectionString)
        {
        }

        public override Pessoas Incluir(Pessoas objeto)
        {
            throw new NotImplementedException();
        }

        public override Pessoas Alterar(Pessoas objeto)
        {
            throw new NotImplementedException();
        }

        public override void Excluir(Pessoas objeto)
        {
        }

        public override void Excluir(params object[] variavel)
        {
        }

        protected override Pessoas MapToEntity(IDataRecord record)
        {
            throw new NotImplementedException();
        }
    }
}