using SocialCare.DATA.Models;
using Microsoft.Data.SqlClient;
using SocialCare.DATA.Interfaces;
using System.Data;

namespace SocialCare.DATA.Repositories
{
    public class RepositoryProdutos : RepositoryBase<Produtos>, IRepositoryProdutos
    {
        public RepositoryProdutos(string connectionString) : base(connectionString)
        {
        }

        public override Produtos Incluir(Produtos objeto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SqlCommand("INSERT INTO Produtos (nome, preco, estoque) VALUES (@Nome, @Preco, @Estoque)", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Nome", objeto.Nome);
                            command.Parameters.AddWithValue("@Preco", objeto.Preco);
                            command.Parameters.AddWithValue("@Estoque", objeto.Estoque);
                            command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return objeto;
        }

        public override Produtos Alterar(Produtos objeto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SqlCommand("UPDATE Produtos SET nome = @Nome, preco = @Preco, estoque = @Estoque WHERE id = @Id", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Nome", objeto.Nome);
                            command.Parameters.AddWithValue("@Preco", objeto.Preco);
                            command.Parameters.AddWithValue("@Estoque", objeto.Estoque);
                            command.Parameters.AddWithValue("@Id", objeto.Id);
                            command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return objeto;
        }

        public override void Excluir(Produtos objeto)
        {
            Excluir(objeto.Id);
        }

        public override void Excluir(params object[] variavel)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SqlCommand("DELETE FROM Produtos WHERE id = @Id", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Id", variavel[0]);
                            command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        protected override Produtos MapToEntity(IDataRecord record)
        {
            return new Produtos
            {
                Id = (int)record["id"],
                Nome = record["nome"].ToString(),
                Preco = (decimal)record["preco"],
                Estoque = (int)record["estoque"]
            };
        }
    }
}