using SocialCare.DATA.Models;
using System.Data;

public class PessoasDAO : IDisposable
{
    private DBConnection _dbConnection;

    public PessoasDAO()
    {
        _dbConnection = new DBConnection();
    }

    public List<Pessoas> SelecionarTodos()
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

    public Pessoas SelecionarPorId(int id)
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

    public void Incluir(Pessoas pessoa)
    {
        string commandText = $"INSERT INTO Pessoas (nome, cidade, bairro, endereco, numero, email, telefone, tipo) VALUES ('{pessoa.Nome}', '{pessoa.Cidade}', '{pessoa.Bairro}', '{pessoa.Endereco}', '{pessoa.Numero}', '{pessoa.Email}', '{pessoa.Telefone}', '{pessoa.Tipo}')";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Alterar(Pessoas pessoa)
    {
        string commandText = $"UPDATE Pessoas SET nome = '{pessoa.Nome}', cidade = '{pessoa.Cidade}', bairro = '{pessoa.Bairro}', endereco = '{pessoa.Endereco}', numero = '{pessoa.Numero}', email = '{pessoa.Email}', telefone = '{pessoa.Telefone}', tipo = '{pessoa.Tipo}' WHERE id = {pessoa.Id}";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Excluir(int id)
    {
        string commandText = $"DELETE FROM Pessoas WHERE id = {id}";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Dispose()
    {
        _dbConnection.Dispose();
    }
}