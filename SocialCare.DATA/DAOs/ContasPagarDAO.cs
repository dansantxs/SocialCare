using System.Data;
using SocialCare.DATA.Models;

public class ContasPagarDAO
{
    public List<ContasPagar> SelecionarTodos(DBConnection _dbConnection)
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

    public ContasPagar SelecionarPorId(int id, DBConnection _dbConnection)
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

    public ContasPagar SelecionarPorIdCompra(int id, DBConnection _dbConnection)
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

    public void Incluir(ContasPagar conta, DBConnection _dbConnection)
    {
        string commandText = $"INSERT INTO ContasPagar (idPessoa, idCompra, data, valor, dataVencimento, dataPagamento) VALUES " +
                           $"({conta.IdPessoa}, {(conta.IdCompra.HasValue ? conta.IdCompra.ToString() : "NULL")}, " +
                           $"'{conta.Data}', {conta.Valor}, '{conta.DataVencimento}', " +
                           $"{(conta.DataPagamento.HasValue ? $"'{conta.DataPagamento}'" : "NULL")}); SELECT SCOPE_IDENTITY();";

        conta.Id = Convert.ToInt32(_dbConnection.ExecuteScalar(commandText));
    }

    public void Alterar(ContasPagar conta, DBConnection _dbConnection)
    {
        string commandText = $"UPDATE ContasPagar SET " +
                           $"idPessoa = {conta.IdPessoa}, " +
                           $"idCompra = {(conta.IdCompra.HasValue ? conta.IdCompra.ToString() : "NULL")}, " +
                           $"data = '{conta.Data}', " +
                           $"valor = {conta.Valor}, " +
                           $"dataVencimento = '{conta.DataVencimento}', " +
                           $"dataPagamento = {(conta.DataPagamento.HasValue ? $"'{conta.DataPagamento}'" : "NULL")} " +
                           $"WHERE id = {conta.Id}";

        _dbConnection.ExecuteCommand(commandText);
    }

    public void Excluir(int id, DBConnection _dbConnection)
    {
        string commandText = $"DELETE FROM ContasPagar WHERE id = {id}";
        _dbConnection.ExecuteCommand(commandText);
    }
}