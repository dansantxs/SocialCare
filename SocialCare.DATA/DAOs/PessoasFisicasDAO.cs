using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SocialCare.DATA.Models;

public class PessoasFisicasDAO : IDisposable
{
    private DBConnection _dbConnection;

    public PessoasFisicasDAO()
    {
        _dbConnection = new DBConnection();
    }

    public List<PessoasFisicas> SelecionarTodos()
    {
        string query = "SELECT * FROM Pessoas_Fisicas";
        DataTable dataTable = _dbConnection.ExecuteQuery(query);
        return dataTable.AsEnumerable().Select(row => new PessoasFisicas
        {
            Id = row.Field<int>("id"),
            Cpf = row.Field<string>("cpf"),
            DataNascimento = row.Field<DateOnly>("data_nascimento")
        }).ToList();
    }

    public PessoasFisicas SelecionarPorId(int id)
    {
        string query = $"SELECT * FROM Pessoas_Fisicas WHERE id = {id}";
        DataTable dataTable = _dbConnection.ExecuteQuery(query);
        return dataTable.AsEnumerable().Select(row => new PessoasFisicas
        {
            Id = row.Field<int>("id"),
            Cpf = row.Field<string>("cpf"),
            DataNascimento = row.Field<DateOnly>("data_nascimento")
        }).FirstOrDefault();
    }

    public void Incluir(PessoasFisicas pessoaFisica)
    {
        string commandText = $"INSERT INTO Pessoas_Fisicas (cpf, data_nascimento) VALUES ('{pessoaFisica.Cpf}', '{pessoaFisica.DataNascimento}')";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Alterar(PessoasFisicas pessoaFisica)
    {
        string commandText = $"UPDATE Pessoas_Fisicas SET cpf = '{pessoaFisica.Cpf}', data_nascimento = '{pessoaFisica.DataNascimento}' WHERE id = {pessoaFisica.Id}";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Excluir(int id)
    {
        string commandText = $"DELETE FROM Pessoas_Fisicas WHERE id = {id}";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Dispose()
    {
        _dbConnection.Dispose();
    }
}