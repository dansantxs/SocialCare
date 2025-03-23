using SocialCare.DATA.Models;
using Microsoft.Data.SqlClient;
using SocialCare.DATA.Interfaces;
using System.Data;

namespace SocialCare.DATA.Repositories
{
    public class RepositoryContasPagar : RepositoryBase<ContasPagar>, IRepositoryContasPagar
    {
        public RepositoryContasPagar(string connectionString) : base(connectionString)
        {
        }

        public ContasPagar SelecionarPorIdCompra(int id)
        {
            throw new NotImplementedException();
        }

        public override ContasPagar Incluir(ContasPagar objeto)
        {
            throw new NotImplementedException();
        }

        public override ContasPagar Alterar(ContasPagar objeto)
        {
            throw new NotImplementedException();
        }

        public override void Excluir(ContasPagar objeto)
        {
        }

        public override void Excluir(params object[] variavel)
        {
        }

        protected override ContasPagar MapToEntity(IDataRecord record)
        {
            throw new NotImplementedException();
        }
    }
}