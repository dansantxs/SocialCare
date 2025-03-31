using System.Data;
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
        string query = $"SELECT * FROM Pessoas WHERE id = {id}";
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
        }).FirstOrDefault();
    }

    public void Incluir(Pessoas pessoa, DBConnection _dbConnection)
    {
        string commandText = $"INSERT INTO Pessoas (nome, cidade, bairro, endereco, numero, email, telefone, tipo) " +
                           $"VALUES ('{pessoa.Nome}', '{pessoa.Cidade}', '{pessoa.Bairro}', '{pessoa.Endereco}', " +
                           $"'{pessoa.Numero}', '{pessoa.Email}', '{pessoa.Telefone}', '{pessoa.Tipo}'); SELECT SCOPE_IDENTITY();";

        pessoa.Id = Convert.ToInt32(_dbConnection.ExecuteScalar(commandText));
    }

    public void Alterar(Pessoas pessoa, DBConnection _dbConnection)
    {
        string commandText = $"UPDATE Pessoas SET nome = '{pessoa.Nome}', cidade = '{pessoa.Cidade}', " +
                           $"bairro = '{pessoa.Bairro}', endereco = '{pessoa.Endereco}', numero = '{pessoa.Numero}', " +
                           $"email = '{pessoa.Email}', telefone = '{pessoa.Telefone}', tipo = '{pessoa.Tipo}' " +
                           $"WHERE id = {pessoa.Id}";

        _dbConnection.ExecuteCommand(commandText);
    }

    public void Excluir(int id, DBConnection _dbConnection)
    {
        string commandText = $"DELETE FROM Pessoas WHERE id = {id}";
        _dbConnection.ExecuteCommand(commandText);
    }
}