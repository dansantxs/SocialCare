using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SocialCare.DATA.Models;

public class PessoasJuridicasDAO : IDisposable
{
    private DBConnection _dbConnection;

    public PessoasJuridicasDAO()
    {
        _dbConnection = new DBConnection();
    }

    public List<PessoasJuridicas> SelecionarTodos()
    {
        string query = "SELECT * FROM Pessoas_Juridicas";
        DataTable dataTable = _dbConnection.ExecuteQuery(query);
        return dataTable.AsEnumerable().Select(row => new PessoasJuridicas
        {
            Id = row.Field<int>("id"),
            Cnpj = row.Field<string>("cnpj"),
            RazaoSocial = row.Field<string>("razao_social")
        }).ToList();
    }

    public PessoasJuridicas SelecionarPorId(int id)
    {
        string query = $"SELECT * FROM Pessoas_Juridicas WHERE id = {id}";
        DataTable dataTable = _dbConnection.ExecuteQuery(query);
        return dataTable.AsEnumerable().Select(row => new PessoasJuridicas
        {
            Id = row.Field<int>("id"),
            Cnpj = row.Field<string>("cnpj"),
            RazaoSocial = row.Field<string>("razao_social")
        }).FirstOrDefault();
    }

    public void Incluir(PessoasJuridicas pessoaJuridica)
    {
        string commandText = $"INSERT INTO Pessoas_Juridicas (cnpj, razao_social) VALUES ('{pessoaJuridica.Cnpj}', '{pessoaJuridica.RazaoSocial}')";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Alterar(PessoasJuridicas pessoaJuridica)
    {
        string commandText = $"UPDATE Pessoas_Juridicas SET cnpj = '{pessoaJuridica.Cnpj}', razao_social = '{pessoaJuridica.RazaoSocial}' WHERE id = {pessoaJuridica.Id}";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Excluir(int id)
    {
        string commandText = $"DELETE FROM Pessoas_Juridicas WHERE id = {id}";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Dispose()
    {
        _dbConnection.Dispose();
    }
}