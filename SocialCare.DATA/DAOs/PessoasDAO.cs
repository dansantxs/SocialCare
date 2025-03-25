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
        string query = @"
            SELECT p.*, pf.cpf, pf.data_nascimento, pj.cnpj, pj.razao_social
            FROM Pessoas p
            LEFT JOIN Pessoas_Fisicas pf ON p.id = pf.id
            LEFT JOIN Pessoas_Juridicas pj ON p.id = pj.id";

        DataTable dataTable = _dbConnection.ExecuteQuery(query);
        return dataTable.AsEnumerable().Select(row => new Pessoas
        {
            Id = row.Field<int>("id"),
            Nome = row.Field<string>("nome") ?? string.Empty,
            Cidade = row.Field<string>("cidade") ?? string.Empty,
            Bairro = row.Field<string>("bairro") ?? string.Empty,
            Endereco = row.Field<string>("endereco") ?? string.Empty,
            Numero = row.Field<string>("numero") ?? string.Empty,
            Email = row.Field<string>("email") ?? string.Empty,
            Telefone = row.Field<string>("telefone") ?? string.Empty,
            Tipo = row.Field<string>("tipo") ?? string.Empty,
            PessoasFisicas = row.Field<string>("cpf") != null ? new PessoasFisicas
            {
                Cpf = row.Field<string>("cpf") ?? string.Empty,
                DataNascimento = DateOnly.FromDateTime(row.Field<DateTime>("data_nascimento"))
            } : null,
            PessoasJuridicas = row.Field<string>("cnpj") != null ? new PessoasJuridicas
            {
                Cnpj = row.Field<string>("cnpj") ?? string.Empty,
                RazaoSocial = row.Field<string>("razao_social") ?? string.Empty
            } : null
        }).ToList();
    }

    public Pessoas SelecionarPorId(int id)
    {
        string query = $@"
        SELECT p.*, pf.cpf, pf.data_nascimento, pj.cnpj, pj.razao_social
        FROM Pessoas p
        LEFT JOIN Pessoas_Fisicas pf ON p.id = pf.id
        LEFT JOIN Pessoas_Juridicas pj ON p.id = pj.id
        WHERE p.id = {id}";

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
            Tipo = row.Field<string>("tipo"),
            PessoasFisicas = row.Field<string>("cpf") != null ? new PessoasFisicas
            {
                Cpf = row.Field<string>("cpf"),
                DataNascimento = DateOnly.FromDateTime(row.Field<DateTime>("data_nascimento"))
            } : null,
            PessoasJuridicas = row.Field<string>("cnpj") != null ? new PessoasJuridicas
            {
                Cnpj = row.Field<string>("cnpj"),
                RazaoSocial = row.Field<string>("razao_social")
            } : null
        }).FirstOrDefault();
    }

    public void Incluir(Pessoas pessoa, object detalhes)
    {
        try
        {
            _dbConnection.BeginTransaction();

            string commandText = $"INSERT INTO Pessoas (nome, cidade, bairro, endereco, numero, email, telefone, tipo) " +
                             $"VALUES ('{pessoa.Nome}', '{pessoa.Cidade}', '{pessoa.Bairro}', '{pessoa.Endereco}', " +
                             $"'{pessoa.Numero}', '{pessoa.Email}', '{pessoa.Telefone}', '{pessoa.Tipo}');";
            _dbConnection.ExecuteCommand(commandText);

            commandText = $"SELECT CAST(scope_identity() AS int);";
            int idPessoa = _dbConnection.ExecuteCommand(commandText);

            if (pessoa.Tipo == "F" && detalhes is PessoasFisicas pf)
            {
                commandText = $"INSERT INTO Pessoas_Fisicas (id, cpf, data_nascimento) VALUES ({idPessoa}, '{pf.Cpf}', '{pf.DataNascimento}')";
                _dbConnection.ExecuteCommand(commandText);
            }
            else if (pessoa.Tipo == "J" && detalhes is PessoasJuridicas pj)
            {
                commandText = $"INSERT INTO Pessoas_Juridicas (id, cnpj, razao_social) VALUES ({idPessoa}, '{pj.Cnpj}', '{pj.RazaoSocial}')";
                _dbConnection.ExecuteCommand(commandText);
            }

            _dbConnection.Commit();
        }
        catch
        {
            _dbConnection.Rollback();
            throw;
        }
    }

    public void Alterar(Pessoas pessoa, object detalhes)
    {
        try
        {
            _dbConnection.BeginTransaction();

            string commandText = $"UPDATE Pessoas SET nome = '{pessoa.Nome}', cidade = '{pessoa.Cidade}', " +
                                 $"bairro = '{pessoa.Bairro}', endereco = '{pessoa.Endereco}', numero = '{pessoa.Numero}', " +
                                 $"email = '{pessoa.Email}', telefone = '{pessoa.Telefone}', tipo = '{pessoa.Tipo}' " +
                                 $"WHERE id = {pessoa.Id}";
            _dbConnection.ExecuteCommand(commandText);

            if (pessoa.Tipo == "F" && detalhes is PessoasFisicas pf)
            {
                commandText = $"UPDATE Pessoas_Fisicas SET cpf = '{pf.Cpf}', data_nascimento = '{pf.DataNascimento}' " +
                              $"WHERE id = {pessoa.Id}";
                _dbConnection.ExecuteCommand(commandText);
            }
            else if (pessoa.Tipo == "F" && detalhes is PessoasJuridicas pj)
            {
                commandText = $"UPDATE Pessoas_Juridicas SET cnpj = '{pj.Cnpj}', razao_social = '{pj.RazaoSocial}' " +
                              $"WHERE id = {pessoa.Id}";
                _dbConnection.ExecuteCommand(commandText);
            }

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

            string query = $"SELECT tipo FROM Pessoas WHERE id = {id}";
            DataTable dataTable = _dbConnection.ExecuteQuery(query);
            var tipo = dataTable.AsEnumerable().Select(row => row.Field<string>("tipo")).FirstOrDefault();

            if (tipo == "F")
            {
                string commandText = $"DELETE FROM Pessoas_Fisicas WHERE id = {id}";
                _dbConnection.ExecuteCommand(commandText);
            }
            else if (tipo == "J")
            {
                string commandText = $"DELETE FROM Pessoas_Juridicas WHERE id = {id}";
                _dbConnection.ExecuteCommand(commandText);
            }

            string deleteCommand = $"DELETE FROM Pessoas WHERE id = {id}";
            _dbConnection.ExecuteCommand(deleteCommand);

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