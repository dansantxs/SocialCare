using System.Data;
using Microsoft.Data.SqlClient;
using SocialCare.DATA.Models;

public class PessoasDAO
{
    public List<Pessoas> SelecionarTodos(DBConnection _dbConnection)
    {
        string query = "SELECT * FROM Pessoas";
        DataTable dataTable = _dbConnection.ExecuteQuery(query);
        return dataTable.AsEnumerable().Select(row => new Pessoas
        {
            Id = row.Field<int>("id"),
            Nome = row.Field<string>("nome"),
            Cidade = row.Field<string>("cidade"),
            Bairro = row.Field<string>("bairro"),
            Endereco = row.Field<string>("endereco"),
            Numero = row.Field<string>("numero"),
            Email = row.Field<string>("email"),
            Telefone = row.Field<string>("telefone"),
            Tipo = row.Field<string>("tipo")
        }).ToList();
    }

    public Pessoas SelecionarPorId(int id, DBConnection _dbConnection)
    {
        string query = "SELECT * FROM Pessoas WHERE id = @id";
        using (SqlCommand command = new SqlCommand(query, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@id", id);
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }
            return dataTable.AsEnumerable().Select(row => new Pessoas
            {
                Id = row.Field<int>("id"),
                Nome = row.Field<string>("nome"),
                Cidade = row.Field<string>("cidade"),
                Bairro = row.Field<string>("bairro"),
                Endereco = row.Field<string>("endereco"),
                Numero = row.Field<string>("numero"),
                Email = row.Field<string>("email"),
                Telefone = row.Field<string>("telefone"),
                Tipo = row.Field<string>("tipo")
            }).FirstOrDefault();
        }
    }

    public void Incluir(Pessoas pessoa, DBConnection _dbConnection)
    {
        string commandText = "INSERT INTO Pessoas (nome, cidade, bairro, endereco, numero, email, telefone, tipo) " +
                           "VALUES (@nome, @cidade, @bairro, @endereco, @numero, @email, @telefone, @tipo); SELECT SCOPE_IDENTITY();";

        using (SqlCommand command = new SqlCommand(commandText, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@nome", pessoa.Nome);
            command.Parameters.AddWithValue("@cidade", pessoa.Cidade);
            command.Parameters.AddWithValue("@bairro", pessoa.Bairro);
            command.Parameters.AddWithValue("@endereco", pessoa.Endereco);
            command.Parameters.AddWithValue("@numero", pessoa.Numero);
            command.Parameters.AddWithValue("@email", pessoa.Email);
            command.Parameters.AddWithValue("@telefone", pessoa.Telefone);
            command.Parameters.AddWithValue("@tipo", pessoa.Tipo);

            pessoa.Id = Convert.ToInt32(command.ExecuteScalar());
        }
    }

    public void Alterar(Pessoas pessoa, DBConnection _dbConnection)
    {
        string commandText = "UPDATE Pessoas SET nome = @nome, cidade = @cidade, bairro = @bairro, " +
                           "endereco = @endereco, numero = @numero, email = @email, telefone = @telefone, tipo = @tipo " +
                           "WHERE id = @id";

        using (SqlCommand command = new SqlCommand(commandText, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@nome", pessoa.Nome);
            command.Parameters.AddWithValue("@cidade", pessoa.Cidade);
            command.Parameters.AddWithValue("@bairro", pessoa.Bairro);
            command.Parameters.AddWithValue("@endereco", pessoa.Endereco);
            command.Parameters.AddWithValue("@numero", pessoa.Numero);
            command.Parameters.AddWithValue("@email", pessoa.Email);
            command.Parameters.AddWithValue("@telefone", pessoa.Telefone);
            command.Parameters.AddWithValue("@tipo", pessoa.Tipo);
            command.Parameters.AddWithValue("@id", pessoa.Id);

            command.ExecuteNonQuery();
        }
    }

    public void Excluir(int id, DBConnection _dbConnection)
    {
        string commandText = "DELETE FROM Pessoas WHERE id = @id";
        using (SqlCommand command = new SqlCommand(commandText, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}