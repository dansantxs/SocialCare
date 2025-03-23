using SocialCare.DATA.Models;
using Microsoft.Data.SqlClient;
using SocialCare.DATA.Interfaces;
using System.Data;

namespace SocialCare.DATA.Repositories
{
    public class RepositoryCompras : RepositoryBase<Compras>, IRepositoryCompras
    {
        public RepositoryCompras(string connectionString) : base(connectionString)
        {
        }

        public override Compras Incluir(Compras objeto)
        {
            throw new NotImplementedException();
        }

        public override Compras Alterar(Compras objeto)
        {
            throw new NotImplementedException();
        }

        public override void Excluir(Compras objeto)
        {
        }

        public override void Excluir(params object[] variavel)
        {
        }

        protected override Compras MapToEntity(IDataRecord record)
        {
            throw new NotImplementedException();
        }
    }
}