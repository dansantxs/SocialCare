using System.Data;
using SocialCare.DATA.Models;

public class ComprasDAO
{
    public List<Compras> SelecionarTodos(DBConnection _dbConnection)
    {
        string query = "SELECT * FROM Compras";
        DataTable dataTable = _dbConnection.ExecuteQuery(query);
        return dataTable.AsEnumerable().Select(row => new Compras
        {
            Id = row.Field<int>("id"),
            IdPessoa = row.Field<int>("idPessoa"),
            DataCompra = row.Field<DateTime>("dataCompra"),
            Total = row.Field<decimal>("total")
        }).ToList();
    }

    public Compras SelecionarPorId(int id, DBConnection _dbConnection)
    {
        string query = $"SELECT * FROM Compras WHERE id = {id}";
        DataTable dataTable = _dbConnection.ExecuteQuery(query);
        return dataTable.AsEnumerable().Select(row => new Compras
        {
            Id = row.Field<int>("id"),
            IdPessoa = row.Field<int>("idPessoa"),
            DataCompra = row.Field<DateTime>("dataCompra"),
            Total = row.Field<decimal>("total")
        }).FirstOrDefault();
    }

    public void Incluir(Compras compra, DBConnection _dbConnection)
    {
        string commandText = $"INSERT INTO Compras (idPessoa, dataCompra, total) VALUES ({compra.IdPessoa}, '{compra.DataCompra}', {compra.Total}); SELECT SCOPE_IDENTITY();";
        compra.Id = Convert.ToInt32(_dbConnection.ExecuteScalar(commandText));
    }

    public void Alterar(Compras compra, DBConnection _dbConnection)
    {
        string commandText = $"UPDATE Compras SET idPessoa = {compra.IdPessoa}, dataCompra = '{compra.DataCompra}', total = {compra.Total} WHERE id = {compra.Id}";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Excluir(int id, DBConnection _dbConnection)
    {
        string commandText = $"DELETE FROM Compras WHERE id = {id}";
        _dbConnection.ExecuteCommand(commandText);
    }
}