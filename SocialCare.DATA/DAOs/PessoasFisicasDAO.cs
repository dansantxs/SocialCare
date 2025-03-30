using SocialCare.DATA.Models;
using System.Data;

public class PessoasFisicasDAO
{
    public PessoasFisicas SelecionarPorId(int id, DBConnection _dbConnection)
    {
        string query = $"SELECT * FROM Pessoas_Fisicas WHERE id = {id}";
        DataTable dataTable = _dbConnection.ExecuteQuery(query);
        return dataTable.AsEnumerable().Select(row => new PessoasFisicas
        {
            Id = row.Field<int>("id"),
            Cpf = row.Field<string>("cpf"),
            DataNascimento = DateOnly.FromDateTime(row.Field<DateTime>("data_nascimento"))
        }).FirstOrDefault();
    }

    public void Incluir(PessoasFisicas pessoaFisica, DBConnection _dbConnection)
    {
        string commandText = $"INSERT INTO Pessoas_Fisicas (id, cpf, data_nascimento) " +
                           $"VALUES ({pessoaFisica.Id}, '{pessoaFisica.Cpf}', '{pessoaFisica.DataNascimento}')";

        _dbConnection.ExecuteCommand(commandText);
    }

    public void Alterar(PessoasFisicas pessoaFisica, DBConnection _dbConnection)
    {
        string commandText = $"UPDATE Pessoas_Fisicas SET cpf = '{pessoaFisica.Cpf}', " +
                           $"data_nascimento = '{pessoaFisica.DataNascimento}' " +
                           $"WHERE id = {pessoaFisica.Id}";

        _dbConnection.ExecuteCommand(commandText);
    }

    public void Excluir(int id, DBConnection _dbConnection)
    {
        string commandText = $"DELETE FROM Pessoas_Fisicas WHERE id = {id}";
        _dbConnection.ExecuteCommand(commandText);
    }
}