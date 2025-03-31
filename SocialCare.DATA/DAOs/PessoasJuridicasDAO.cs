using System.Data;
using SocialCare.DATA.Models;

public class PessoasJuridicasDAO
{
    public PessoasJuridicas SelecionarPorId(int id, DBConnection _dbConnection)
    {
        string query = $"SELECT * FROM Pessoas_Juridicas WHERE id = {id}";
        DataTable dataTable = _dbConnection.ExecuteQuery(query);
        return dataTable.AsEnumerable().Select(row => new PessoasJuridicas
        {
            Id = row.Field<int>("id"),
            Cnpj = row.Field<string>("cnpj"),
            RazaoSocial = row.Field<string>("razao_social")
        }).FirstOrDefault();
    }

    public void Incluir(PessoasJuridicas pessoaJuridica, DBConnection _dbConnection)
    {
        string commandText = $"INSERT INTO Pessoas_Juridicas (id, cnpj, razao_social) " +
                           $"VALUES ({pessoaJuridica.Id}, '{pessoaJuridica.Cnpj}', '{pessoaJuridica.RazaoSocial}')";

        _dbConnection.ExecuteCommand(commandText);
    }

    public void Alterar(PessoasJuridicas pessoaJuridica, DBConnection _dbConnection)
    {
        string commandText = $"UPDATE Pessoas_Juridicas SET cnpj = '{pessoaJuridica.Cnpj}', " +
                           $"razao_social = '{pessoaJuridica.RazaoSocial}' " +
                           $"WHERE id = {pessoaJuridica.Id}";

        _dbConnection.ExecuteCommand(commandText);
    }

    public void Excluir(int id, DBConnection _dbConnection)
    {
        string commandText = $"DELETE FROM Pessoas_Juridicas WHERE id = {id}";
        _dbConnection.ExecuteCommand(commandText);
    }
}