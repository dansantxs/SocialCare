using SocialCare.DATA.Models;
using System.Data;

public class ProdutosDAO
{
    public List<Produtos> SelecionarTodos(DBConnection _dbConnection)
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

    public Produtos SelecionarPorId(int id, DBConnection _dbConnection)
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

    public void Incluir(Produtos produto, DBConnection _dbConnection)
    {
        string commandText = $"INSERT INTO Produtos (nome, preco, estoque) VALUES ('{produto.Nome}', {produto.Preco}, {produto.Estoque})";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Alterar(Produtos produto, DBConnection _dbConnection)
    {
        string commandText = $"UPDATE Produtos SET nome = '{produto.Nome}', preco = {produto.Preco}, estoque = {produto.Estoque} WHERE id = {produto.Id}";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Excluir(int id, DBConnection _dbConnection)
    {
        string commandText = $"DELETE FROM Produtos WHERE id = {id}";
        _dbConnection.ExecuteCommand(commandText);
    }
}