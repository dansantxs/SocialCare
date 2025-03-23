using SocialCare.DATA.Models;
using System.Data;

public class ProdutosDAO : IDisposable
{
    private DBConnection _dbConnection;

    public ProdutosDAO()
    {
        _dbConnection = new DBConnection();
    }

    public List<Produtos> SelecionarTodos()
    {
        string query = "SELECT * FROM Produtos";
        DataTable dataTable = _dbConnection.ExecuteQuery(query);
        return dataTable.AsEnumerable().Select(row => new Produtos
        {
            Id = row.Field<int>("id"),
            Nome = row.Field<string>("nome"),
            Preco = row.Field<decimal>("preco"),
            Estoque = row.Field<int>("estoque")
        }).ToList();
    }

    public Produtos SelecionarPorId(int id)
    {
        string query = $"SELECT * FROM Produtos WHERE id = {id}";
        DataTable dataTable = _dbConnection.ExecuteQuery(query);
        return dataTable.AsEnumerable().Select(row => new Produtos
        {
            Id = row.Field<int>("id"),
            Nome = row.Field<string>("nome"),
            Preco = row.Field<decimal>("preco"),
            Estoque = row.Field<int>("estoque")
        }).FirstOrDefault();
    }

    public void Incluir(Produtos produto)
    {
        string commandText = $"INSERT INTO Produtos (nome, preco, estoque) VALUES ('{produto.Nome}', {produto.Preco}, {produto.Estoque})";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Alterar(Produtos produto)
    {
        string commandText = $"UPDATE Produtos SET nome = '{produto.Nome}', preco = {produto.Preco}, estoque = {produto.Estoque} WHERE id = {produto.Id}";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Excluir(int id)
    {
        string commandText = $"DELETE FROM Produtos WHERE id = {id}";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Dispose()
    {
        _dbConnection.Dispose();
    }
}