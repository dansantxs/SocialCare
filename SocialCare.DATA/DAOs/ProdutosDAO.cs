using System.Data;
using Npgsql;
using SocialCare.DATA.Models;

public class ProdutosDAO
{
    public List<Produtos> SelecionarTodos(DBConnection _dbConnection)
    {
        string query = "SELECT * FROM \"Produtos\"";
        DataTable dataTable = _dbConnection.ExecuteQuery(query);
        return dataTable.AsEnumerable().Select(row => new Produtos
        {
            Id = row.Field<int>("id"),
            Nome = row.Field<string>("nome"),
            Preco = row.Field<decimal>("preco"),
            Estoque = row.Field<int>("estoque")
        }).ToList();
    }

    public Produtos SelecionarPorId(int id, DBConnection _dbConnection)
    {
        string query = "SELECT * FROM \"Produtos\" WHERE \"id\" = @id";
        using (NpgsqlCommand command = new NpgsqlCommand(query, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@id", id);
            DataTable dataTable = new DataTable();
            using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }
            return dataTable.AsEnumerable().Select(row => new Produtos
            {
                Id = row.Field<int>("id"),
                Nome = row.Field<string>("nome"),
                Preco = row.Field<decimal>("preco"),
                Estoque = row.Field<int>("estoque")
            }).FirstOrDefault();
        }
    }

    public void Incluir(Produtos produto, DBConnection _dbConnection)
    {
        string commandText = "INSERT INTO \"Produtos\" (\"nome\", \"preco\", \"estoque\") VALUES (@nome, @preco, @estoque) RETURNING \"id\";";
        using (NpgsqlCommand command = new NpgsqlCommand(commandText, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@nome", produto.Nome);
            command.Parameters.AddWithValue("@preco", produto.Preco);
            command.Parameters.AddWithValue("@estoque", produto.Estoque);
            produto.Id = Convert.ToInt32(command.ExecuteScalar());
        }
    }

    public void Alterar(Produtos produto, DBConnection _dbConnection)
    {
        string commandText = "UPDATE \"Produtos\" SET \"nome\" = @nome, \"preco\" = @preco, \"estoque\" = @estoque WHERE \"id\" = @id";
        using (NpgsqlCommand command = new NpgsqlCommand(commandText, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@nome", produto.Nome);
            command.Parameters.AddWithValue("@preco", produto.Preco);
            command.Parameters.AddWithValue("@estoque", produto.Estoque);
            command.Parameters.AddWithValue("@id", produto.Id);
            command.ExecuteNonQuery();
        }
    }

    public void Excluir(int id, DBConnection _dbConnection)
    {
        string commandText = "DELETE FROM \"Produtos\" WHERE \"id\" = @id";
        using (NpgsqlCommand command = new NpgsqlCommand(commandText, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}