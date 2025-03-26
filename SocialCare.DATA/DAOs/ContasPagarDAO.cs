﻿using SocialCare.DATA.Models;
using System.Data;

public class ContasPagarDAO : IDisposable
{
    private DBConnection _dbConnection;

    public ContasPagarDAO()
    {
        _dbConnection = new DBConnection();
    }

    public List<ContasPagar> SelecionarTodos()
    {
        string query = "SELECT * FROM ContasPagar";
        DataTable dataTable = _dbConnection.ExecuteQuery(query);
        return dataTable.AsEnumerable().Select(row => new ContasPagar
        {
            Id = row.Field<int>("id"),
            IdPessoa = row.Field<int>("idPessoa"),
            IdCompra = row.Field<int?>("idCompra"),
            Data = row.Field<DateTime>("data"),
            Valor = row.Field<decimal>("valor"),
            DataVencimento = row.Field<DateTime>("dataVencimento"),
            DataPagamento = row.Field<DateTime?>("dataPagamento")
        }).ToList();
    }

    public ContasPagar SelecionarPorId(int id)
    {
        string query = $"SELECT * FROM ContasPagar WHERE id = {id}";
        DataTable dataTable = _dbConnection.ExecuteQuery(query);
        return dataTable.AsEnumerable().Select(row => new ContasPagar
        {
            Id = row.Field<int>("id"),
            IdPessoa = row.Field<int>("idPessoa"),
            IdCompra = row.Field<int?>("idCompra"),
            Data = row.Field<DateTime>("data"),
            Valor = row.Field<decimal>("valor"),
            DataVencimento = row.Field<DateTime>("dataVencimento"),
            DataPagamento = row.Field<DateTime?>("dataPagamento")
        }).FirstOrDefault();
    }

    public ContasPagar SelecionarPorIdCompra(int id)
    {
        string query = $"SELECT * FROM ContasPagar WHERE idCompra = {id}";
        DataTable dataTable = _dbConnection.ExecuteQuery(query);
        return dataTable.AsEnumerable().Select(row => new ContasPagar
        {
            Id = row.Field<int>("id"),
            IdPessoa = row.Field<int>("idPessoa"),
            IdCompra = row.Field<int?>("idCompra"),
            Data = row.Field<DateTime>("data"),
            Valor = row.Field<decimal>("valor"),
            DataVencimento = row.Field<DateTime>("dataVencimento"),
            DataPagamento = row.Field<DateTime?>("dataPagamento")
        }).FirstOrDefault();
    }

    public void Incluir(ContasPagar conta)
    {
        try
        {
            _dbConnection.BeginTransaction();
            string commandText = $"INSERT INTO ContasPagar (idPessoa, idCompra, data, valor, dataVencimento, dataPagamento) VALUES ({conta.IdPessoa}, {(conta.IdCompra.HasValue ? conta.IdCompra.ToString() : "NULL")}, '{conta.Data}', {conta.Valor}, '{conta.DataVencimento}', {(conta.DataPagamento.HasValue ? $"'{conta.DataPagamento}'" : "NULL")})";
            _dbConnection.ExecuteCommand(commandText);
            _dbConnection.Commit();
        }
        catch
        {
            _dbConnection.Rollback();
            throw;
        }
    }

    public void Alterar(ContasPagar conta)
    {
        try
        {
            _dbConnection.BeginTransaction();
            string commandText = $"UPDATE ContasPagar SET idPessoa = {conta.IdPessoa}, idCompra = {(conta.IdCompra.HasValue ? conta.IdCompra.ToString() : "NULL")}, data = '{conta.Data}', valor = {conta.Valor}, dataVencimento = '{conta.DataVencimento}', dataPagamento = {(conta.DataPagamento.HasValue ? $"'{conta.DataPagamento}'" : "NULL")} WHERE id = {conta.Id}";
            _dbConnection.ExecuteCommand(commandText);
            _dbConnection.Commit();
        }
        catch
        {
            _dbConnection.Rollback();
            throw;
        }
    }

    public void Excluir(int id)
    {
        try
        {
            _dbConnection.BeginTransaction();
            string commandText = $"DELETE FROM ContasPagar WHERE id = {id}";
            _dbConnection.ExecuteCommand(commandText);
            _dbConnection.Commit();
        }
        catch
        {
            _dbConnection.Rollback();
            throw;
        }
    }

    public void Dispose()
    {
        _dbConnection.Dispose();
    }
}