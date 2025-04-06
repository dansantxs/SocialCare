using System.Data;
using Microsoft.Data.SqlClient;
using SocialCare.DATA.Models;

public class ContasPagarDAO
{
    public List<ContasPagar> SelecionarTodos(DBConnection _dbConnection)
    {
        string query = "SELECT * FROM ContasPagar";
        DataTable dataTable = _dbConnection.ExecuteQuery(query);
        return dataTable.AsEnumerable().Select(row => new ContasPagar
        {
            Id = row.Field<int>("id"),
            IdPessoa = row.Field<int>("idPessoa"),
            IdCompra = row.Field<int?>("idCompra"),
            Data = row.Field<DateTime>("data"),
            Valor = row.Field<decimal>("valor"),
            DataVencimento = row.Field<DateTime>("dataVencimento"),
            DataPagamento = row.Field<DateTime?>("dataPagamento")
        }).ToList();
    }

    public ContasPagar SelecionarPorId(int id, DBConnection _dbConnection)
    {
        string query = "SELECT * FROM ContasPagar WHERE id = @id";
        using (SqlCommand command = new SqlCommand(query, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@id", id);
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            return dataTable.AsEnumerable().Select(row => new ContasPagar
            {
                Id = row.Field<int>("id"),
                IdPessoa = row.Field<int>("idPessoa"),
                IdCompra = row.Field<int?>("idCompra"),
                Data = row.Field<DateTime>("data"),
                Valor = row.Field<decimal>("valor"),
                DataVencimento = row.Field<DateTime>("dataVencimento"),
                DataPagamento = row.Field<DateTime?>("dataPagamento")
            }).FirstOrDefault();
        }
    }

    public ContasPagar SelecionarPorIdCompra(int id, DBConnection _dbConnection)
    {
        string query = "SELECT * FROM ContasPagar WHERE idCompra = @idCompra";
        using (SqlCommand command = new SqlCommand(query, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@idCompra", id);
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            return dataTable.AsEnumerable().Select(row => new ContasPagar
            {
                Id = row.Field<int>("id"),
                IdPessoa = row.Field<int>("idPessoa"),
                IdCompra = row.Field<int?>("idCompra"),
                Data = row.Field<DateTime>("data"),
                Valor = row.Field<decimal>("valor"),
                DataVencimento = row.Field<DateTime>("dataVencimento"),
                DataPagamento = row.Field<DateTime?>("dataPagamento")
            }).FirstOrDefault();
        }
    }

    public void Incluir(ContasPagar conta, DBConnection _dbConnection)
    {
        string commandText = "INSERT INTO ContasPagar (idPessoa, idCompra, data, valor, dataVencimento, dataPagamento) " +
                           "VALUES (@idPessoa, @idCompra, @data, @valor, @dataVencimento, @dataPagamento); SELECT SCOPE_IDENTITY();";

        using (SqlCommand command = new SqlCommand(commandText, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@idPessoa", conta.IdPessoa);
            command.Parameters.AddWithValue("@idCompra", conta.IdCompra ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@data", conta.Data);
            command.Parameters.AddWithValue("@valor", conta.Valor);
            command.Parameters.AddWithValue("@dataVencimento", conta.DataVencimento);
            command.Parameters.AddWithValue("@dataPagamento", conta.DataPagamento ?? (object)DBNull.Value);

            conta.Id = Convert.ToInt32(command.ExecuteScalar());
        }
    }

    public void Alterar(ContasPagar conta, DBConnection _dbConnection)
    {
        string commandText = "UPDATE ContasPagar SET idPessoa = @idPessoa, idCompra = @idCompra, data = @data, " +
                           "valor = @valor, dataVencimento = @dataVencimento, dataPagamento = @dataPagamento " +
                           "WHERE id = @id";

        using (SqlCommand command = new SqlCommand(commandText, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@idPessoa", conta.IdPessoa);
            command.Parameters.AddWithValue("@idCompra", conta.IdCompra ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@data", conta.Data);
            command.Parameters.AddWithValue("@valor", conta.Valor);
            command.Parameters.AddWithValue("@dataVencimento", conta.DataVencimento);
            command.Parameters.AddWithValue("@dataPagamento", conta.DataPagamento ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@id", conta.Id);

            command.ExecuteNonQuery();
        }
    }

    public void Excluir(int id, DBConnection _dbConnection)
    {
        string commandText = "DELETE FROM ContasPagar WHERE id = @id";
        using (SqlCommand command = new SqlCommand(commandText, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}