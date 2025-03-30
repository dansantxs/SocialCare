using SocialCare.DATA.Models;
using System.Data;

public class ItensCompraDAO : IDisposable
{
    private DBConnection _dbConnection;

    public ItensCompraDAO()
    {
        _dbConnection = new DBConnection();
    }

    public List<ItensCompra> SelecionarTodos()
    {
        string query = "SELECT * FROM ItensCompra";
        DataTable dataTable = _dbConnection.ExecuteQuery(query);
        return dataTable.AsEnumerable().Select(row => new ItensCompra
        {
            Id = row.Field<int>("id"),
            IdCompra = row.Field<int>("idCompra"),
            IdProduto = row.Field<int>("idProduto"),
            Quantidade = row.Field<int>("quantidade"),
            PrecoUnitario = row.Field<decimal>("precoUnitario"),
            Subtotal = row.Field<decimal?>("subtotal")
        }).ToList();
    }

    public ItensCompra SelecionarPorId(int id)
    {
        string query = $"SELECT * FROM ItensCompra WHERE id = {id}";
        DataTable dataTable = _dbConnection.ExecuteQuery(query);
        return dataTable.AsEnumerable().Select(row => new ItensCompra
        {
            Id = row.Field<int>("id"),
            IdCompra = row.Field<int>("idCompra"),
            IdProduto = row.Field<int>("idProduto"),
            Quantidade = row.Field<int>("quantidade"),
            PrecoUnitario = row.Field<decimal>("precoUnitario"),
            Subtotal = row.Field<decimal?>("subtotal")
        }).FirstOrDefault();
    }

    public void Incluir(ItensCompra item)
    {
        string commandText = $"INSERT INTO ItensCompra (idCompra, idProduto, quantidade, precoUnitario, subtotal) VALUES ({item.IdCompra}, {item.IdProduto}, {item.Quantidade}, {item.PrecoUnitario}, {item.Subtotal})";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Alterar(ItensCompra item)
    {
        string commandText = $"UPDATE ItensCompra SET idCompra = {item.IdCompra}, idProduto = {item.IdProduto}, quantidade = {item.Quantidade}, precoUnitario = {item.PrecoUnitario}, subtotal = {item.Subtotal} WHERE id = {item.Id}";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Excluir(int id)
    {
        string commandText = $"DELETE FROM ItensCompra WHERE id = {id}";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Dispose()
    {
        _dbConnection.Dispose();
    }
}