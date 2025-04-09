using System.Data;
using Npgsql;
using SocialCare.DATA.Models;

public class ItensCompraDAO
{
    public List<ItensCompra> SelecionarTodos(DBConnection _dbConnection)
    {
        string query = "SELECT * FROM \"ItensCompra\"";
        DataTable dataTable = _dbConnection.ExecuteQuery(query);
        return dataTable.AsEnumerable().Select(row => new ItensCompra
        {
            Id = row.Field<int>("id"),
            IdCompra = row.Field<int>("idCompra"),
            IdProduto = row.Field<int>("idProduto"),
            Quantidade = row.Field<int>("quantidade"),
            PrecoUnitario = row.Field<decimal>("precoUnitario"),
            Subtotal = row.Field<decimal>("subtotal")
        }).ToList();
    }

    public List<ItensCompra> SelecionarPorIdCompra(int idCompra, DBConnection _dbConnection)
    {
        string query = "SELECT * FROM \"ItensCompra\" WHERE \"idCompra\" = @idCompra";
        using (NpgsqlCommand command = new NpgsqlCommand(query, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@idCompra", idCompra);
            DataTable dataTable = new DataTable();
            using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            return dataTable.AsEnumerable().Select(row => new ItensCompra
            {
                Id = row.Field<int>("id"),
                IdCompra = row.Field<int>("idCompra"),
                IdProduto = row.Field<int>("idProduto"),
                Quantidade = row.Field<int>("quantidade"),
                PrecoUnitario = row.Field<decimal>("precoUnitario"),
                Subtotal = row.Field<decimal>("subtotal"),
                Produto = new Produtos().SelecionarPorId(row.Field<int>("idProduto"), _dbConnection)
            }).ToList();
        }
    }

    public ItensCompra SelecionarPorId(int id, DBConnection _dbConnection)
    {
        string query = "SELECT * FROM \"ItensCompra\" WHERE \"id\" = @id";
        using (NpgsqlCommand command = new NpgsqlCommand(query, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@id", id);
            DataTable dataTable = new DataTable();
            using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            return dataTable.AsEnumerable().Select(row => new ItensCompra
            {
                Id = row.Field<int>("id"),
                IdCompra = row.Field<int>("idCompra"),
                IdProduto = row.Field<int>("idProduto"),
                Quantidade = row.Field<int>("quantidade"),
                PrecoUnitario = row.Field<decimal>("precoUnitario"),
                Subtotal = row.Field<decimal>("subtotal")
            }).FirstOrDefault();
        }
    }

    public void Incluir(ItensCompra item, DBConnection _dbConnection)
    {
        string commandText = "INSERT INTO \"ItensCompra\" (\"idCompra\", \"idProduto\", \"quantidade\", \"precoUnitario\", \"subtotal\") " +
                           "VALUES (@idCompra, @idProduto, @quantidade, @precoUnitario, @subtotal) RETURNING \"id\";";

        using (NpgsqlCommand command = new NpgsqlCommand(commandText, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@idCompra", item.IdCompra);
            command.Parameters.AddWithValue("@idProduto", item.IdProduto);
            command.Parameters.AddWithValue("@quantidade", item.Quantidade);
            command.Parameters.AddWithValue("@precoUnitario", item.PrecoUnitario);
            command.Parameters.AddWithValue("@subtotal", item.Subtotal ?? 0);

            item.Id = Convert.ToInt32(command.ExecuteScalar());
        }
    }

    public void Alterar(ItensCompra item, DBConnection _dbConnection)
    {
        string commandText = "UPDATE \"ItensCompra\" SET \"idCompra\" = @idCompra, \"idProduto\" = @idProduto, " +
                           "\"quantidade\" = @quantidade, \"precoUnitario\" = @precoUnitario, \"subtotal\" = @subtotal " +
                           "WHERE \"id\" = @id";

        using (NpgsqlCommand command = new NpgsqlCommand(commandText, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@idCompra", item.IdCompra);
            command.Parameters.AddWithValue("@idProduto", item.IdProduto);
            command.Parameters.AddWithValue("@quantidade", item.Quantidade);
            command.Parameters.AddWithValue("@precoUnitario", item.PrecoUnitario);
            command.Parameters.AddWithValue("@subtotal", item.Subtotal ?? 0);
            command.Parameters.AddWithValue("@id", item.Id);

            command.ExecuteNonQuery();
        }
    }

    public void Excluir(int id, DBConnection _dbConnection)
    {
        string commandText = "DELETE FROM \"ItensCompra\" WHERE \"id\" = @id";
        using (NpgsqlCommand command = new NpgsqlCommand(commandText, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}