using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SocialCare.DATA.Models;

public class ComprasDAO : IDisposable
{
    private DBConnection _dbConnection;

    public ComprasDAO()
    {
        _dbConnection = new DBConnection();
    }

    public List<Compras> SelecionarTodos()
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

    public Compras SelecionarPorId(int id)
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

    public void Incluir(Compras compra)
    {
        string commandText = $"INSERT INTO Compras (idPessoa, dataCompra, total) VALUES ({compra.IdPessoa}, '{compra.DataCompra}', {compra.Total})";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Alterar(Compras compra)
    {
        string commandText = $"UPDATE Compras SET idPessoa = {compra.IdPessoa}, dataCompra = '{compra.DataCompra}', total = {compra.Total} WHERE id = {compra.Id}";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Excluir(int id)
    {
        string commandText = $"DELETE FROM Compras WHERE id = {id}";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Dispose()
    {
        _dbConnection.Dispose();
    }
}