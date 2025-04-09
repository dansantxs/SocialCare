using System.Data;
using Npgsql;
using SocialCare.DATA.Models;

public class PessoasJuridicasDAO
{
    public PessoasJuridicas SelecionarPorId(int id, DBConnection _dbConnection)
    {
        string query = "SELECT * FROM \"Pessoas_Juridicas\" WHERE \"id\" = @id";
        using (NpgsqlCommand command = new NpgsqlCommand(query, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@id", id);
            DataTable dataTable = new DataTable();
            using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }
            return dataTable.AsEnumerable().Select(row => new PessoasJuridicas
            {
                Id = row.Field<int>("id"),
                Cnpj = row.Field<string>("cnpj"),
                RazaoSocial = row.Field<string>("razao_social")
            }).FirstOrDefault();
        }
    }

    public void Incluir(PessoasJuridicas pessoaJuridica, DBConnection _dbConnection)
    {
        string commandText = "INSERT INTO \"Pessoas_Juridicas\" (\"id\", \"cnpj\", \"razao_social\") VALUES (@id, @cnpj, @razaoSocial)";
        using (NpgsqlCommand command = new NpgsqlCommand(commandText, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@id", pessoaJuridica.Id);
            command.Parameters.AddWithValue("@cnpj", pessoaJuridica.Cnpj);
            command.Parameters.AddWithValue("@razaoSocial", pessoaJuridica.RazaoSocial);
            command.ExecuteNonQuery();
        }
    }

    public void Alterar(PessoasJuridicas pessoaJuridica, DBConnection _dbConnection)
    {
        string commandText = "UPDATE \"Pessoas_Juridicas\" SET \"cnpj\" = @cnpj, \"razao_social\" = @razaoSocial WHERE \"id\" = @id";
        using (NpgsqlCommand command = new NpgsqlCommand(commandText, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@cnpj", pessoaJuridica.Cnpj);
            command.Parameters.AddWithValue("@razaoSocial", pessoaJuridica.RazaoSocial);
            command.Parameters.AddWithValue("@id", pessoaJuridica.Id);
            command.ExecuteNonQuery();
        }
    }

    public void Excluir(int id, DBConnection _dbConnection)
    {
        string commandText = "DELETE FROM \"Pessoas_Juridicas\" WHERE \"id\" = @id";
        using (NpgsqlCommand command = new NpgsqlCommand(commandText, _dbConnection.Connection, _dbConnection.Transaction))
        {
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}