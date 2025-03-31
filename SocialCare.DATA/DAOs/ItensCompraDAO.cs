using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SocialCare.DATA.Models;

public class ItensCompraDAO
{
    public List<ItensCompra> SelecionarTodos(DBConnection _dbConnection)
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

    public List<ItensCompra> SelecionarPorIdCompra(int idCompra, DBConnection _dbConnection)
    {
        string query = $"SELECT * FROM ItensCompra WHERE idCompra = {idCompra}";
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

    public ItensCompra SelecionarPorId(int id, DBConnection _dbConnection)
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

    public void Incluir(ItensCompra item, DBConnection _dbConnection)
    {
        string commandText = $"INSERT INTO ItensCompra (idCompra, idProduto, quantidade, precoUnitario, subtotal) VALUES ({item.IdCompra}, {item.IdProduto}, {item.Quantidade}, {item.PrecoUnitario}, {item.Subtotal}); SELECT SCOPE_IDENTITY();";
        item.Id = Convert.ToInt32(_dbConnection.ExecuteScalar(commandText));
    }

    public void Alterar(ItensCompra item, DBConnection _dbConnection)
    {
        string commandText = $"UPDATE ItensCompra SET idCompra = {item.IdCompra}, idProduto = {item.IdProduto}, quantidade = {item.Quantidade}, precoUnitario = {item.PrecoUnitario}, subtotal = {item.Subtotal} WHERE id = {item.Id}";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Excluir(int id, DBConnection _dbConnection)
    {
        string commandText = $"DELETE FROM ItensCompra WHERE id = {id}";
        _dbConnection.ExecuteCommand(commandText);
    }
}