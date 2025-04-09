using System.Data;
using Npgsql;
using SocialCare.DATA.Models;

public class ComprasDAO
{
    public List<Compras> SelecionarTodos(DBConnection _dbConnection)
    {
        string query = "SELECT * FROM \"Compras\"";
        DataTable dataTable = _dbConnection.ExecuteQuery(query);
        return dataTable.AsEnumerable().Select(row => new Compras
        {
            Id = row.Field<int>("id"),
            IdPessoa = row.Field<int>("idPessoa"),
            DataCompra = row.Field<DateTime>("dataCompra"),
            Total = row.Field<decimal>("total")
        }).ToList();
    }

    public Compras SelecionarPorId(int id, DBConnection _dbConnection)
    {
        string query = "SELECT * FROM \"Compras\" WHERE \"id\" = @id";
        using (NpgsqlCommand command = new NpgsqlCommand(query, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@id", id);
            DataTable dataTable = new DataTable();
            using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            return dataTable.AsEnumerable().Select(row => new Compras
            {
                Id = row.Field<int>("id"),
                IdPessoa = row.Field<int>("idPessoa"),
                DataCompra = row.Field<DateTime>("dataCompra"),
                Total = row.Field<decimal>("total")
            }).FirstOrDefault();
        }
    }

    public void Incluir(Compras compra, DBConnection _dbConnection)
    {
        string commandText = "INSERT INTO \"Compras\" (\"idPessoa\", \"dataCompra\", \"total\") " +
                           "VALUES (@idPessoa, @dataCompra, @total) RETURNING \"id\";";

        using (NpgsqlCommand command = new NpgsqlCommand(commandText, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@idPessoa", compra.IdPessoa);
            command.Parameters.AddWithValue("@dataCompra", compra.DataCompra);
            command.Parameters.AddWithValue("@total", compra.Total);

            compra.Id = Convert.ToInt32(command.ExecuteScalar());
        }
    }

    public void Alterar(Compras compra, DBConnection _dbConnection)
    {
        string commandText = "UPDATE \"Compras\" SET \"idPessoa\" = @idPessoa, \"dataCompra\" = @dataCompra, \"total\" = @total WHERE \"id\" = @id";

        using (NpgsqlCommand command = new NpgsqlCommand(commandText, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@idPessoa", compra.IdPessoa);
            command.Parameters.AddWithValue("@dataCompra", compra.DataCompra);
            command.Parameters.AddWithValue("@total", compra.Total);
            command.Parameters.AddWithValue("@id", compra.Id);

            command.ExecuteNonQuery();
        }
    }

    public void Excluir(int id, DBConnection _dbConnection)
    {
        string commandText = "DELETE FROM \"Compras\" WHERE \"id\" = @id";
        using (NpgsqlCommand command = new NpgsqlCommand(commandText, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}