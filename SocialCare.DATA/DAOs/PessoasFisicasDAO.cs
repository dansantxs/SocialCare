using System.Data;
using Microsoft.Data.SqlClient;
using SocialCare.DATA.Models;

public class PessoasFisicasDAO
{
    public PessoasFisicas SelecionarPorId(int id, DBConnection _dbConnection)
    {
        string query = "SELECT * FROM Pessoas_Fisicas WHERE id = @id";
        using (SqlCommand command = new SqlCommand(query, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@id", id);
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }
            return dataTable.AsEnumerable().Select(row => new PessoasFisicas
            {
                Id = row.Field<int>("id"),
                Cpf = row.Field<string>("cpf"),
                DataNascimento = DateOnly.FromDateTime(row.Field<DateTime>("data_nascimento"))
            }).FirstOrDefault();
        }
    }

    public void Incluir(PessoasFisicas pessoaFisica, DBConnection _dbConnection)
    {
        string commandText = "INSERT INTO Pessoas_Fisicas (id, cpf, data_nascimento) VALUES (@id, @cpf, @dataNascimento)";
        using (SqlCommand command = new SqlCommand(commandText, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@id", pessoaFisica.Id);
            command.Parameters.AddWithValue("@cpf", pessoaFisica.Cpf);
            command.Parameters.AddWithValue("@dataNascimento", pessoaFisica.DataNascimento.ToDateTime(TimeOnly.MinValue));
            command.ExecuteNonQuery();
        }
    }

    public void Alterar(PessoasFisicas pessoaFisica, DBConnection _dbConnection)
    {
        string commandText = "UPDATE Pessoas_Fisicas SET cpf = @cpf, data_nascimento = @dataNascimento WHERE id = @id";
        using (SqlCommand command = new SqlCommand(commandText, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@cpf", pessoaFisica.Cpf);
            command.Parameters.AddWithValue("@dataNascimento", pessoaFisica.DataNascimento.ToDateTime(TimeOnly.MinValue));
            command.Parameters.AddWithValue("@id", pessoaFisica.Id);
            command.ExecuteNonQuery();
        }
    }

    public void Excluir(int id, DBConnection _dbConnection)
    {
        string commandText = "DELETE FROM Pessoas_Fisicas WHERE id = @id";
        using (SqlCommand command = new SqlCommand(commandText, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}