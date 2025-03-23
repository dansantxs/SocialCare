using SocialCare.DATA.Models;
using Microsoft.Data.SqlClient;
using SocialCare.DATA.Interfaces;
using System.Data;

namespace SocialCare.DATA.Repositories
{
    public class RepositoryItensCompra : RepositoryBase<ItensCompra>, IRepositoryItensCompra
    {
        public RepositoryItensCompra(string connectionString) : base(connectionString)
        {
        }

        public override ItensCompra Incluir(ItensCompra objeto)
        {
            throw new NotImplementedException();
        }

        public override ItensCompra Alterar(ItensCompra objeto)
        {
            throw new NotImplementedException();
        }

        public override void Excluir(ItensCompra objeto)
        {
        }

        public override void Excluir(params object[] variavel)
        {
        }

        protected override ItensCompra MapToEntity(IDataRecord record)
        {
            throw new NotImplementedException();
        }
    }
}