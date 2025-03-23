using Microsoft.Data.SqlClient;
using SocialCare.DATA.Interfaces;
using System.Data;

namespace SocialCare.DATA.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryModel<T>, IDisposable where T : class
    {
        protected readonly string _connectionString;

        protected RepositoryBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> SelecionarTodos()
        {
            var result = new List<T>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM " + typeof(T).Name, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var entity = MapToEntity(reader);
                            result.Add(entity);
                        }
                    }
                }
            }
            return result;
        }

        public T SelecionarPorId(params object[] variavel)
        {
            T entity = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM " + typeof(T).Name + " WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", variavel[0]);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            entity = MapToEntity(reader);
                        }
                    }
                }
            }
            return entity;
        }

        public abstract T Incluir(T objeto);
        public abstract T Alterar(T objeto);
        public abstract void Excluir(T objeto);
        public abstract void Excluir(params object[] variavel);

        public void Dispose()
        {
            // Dispose resources if needed
        }

        protected abstract T MapToEntity(IDataRecord record);
    }
}