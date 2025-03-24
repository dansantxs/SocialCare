using SocialCare.DATA.Models;
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

    public void Incluir(ContasPagar conta)
    {
        string commandText = $"INSERT INTO ContasPagar (idPessoa, idCompra, data, valor, dataVencimento, dataPagamento) VALUES ({conta.IdPessoa}, {conta.IdCompra}, '{conta.Data}', {conta.Valor}, '{conta.DataVencimento}', '{conta.DataPagamento}')";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Alterar(ContasPagar conta)
    {
        string commandText = $"UPDATE ContasPagar SET idPessoa = {conta.IdPessoa}, idCompra = {conta.IdCompra}, data = '{conta.Data}', valor = {conta.Valor}, dataVencimento = '{conta.DataVencimento}', dataPagamento = '{conta.DataPagamento}' WHERE id = {conta.Id}";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Excluir(int id)
    {
        string commandText = $"DELETE FROM ContasPagar WHERE id = {id}";
        _dbConnection.ExecuteCommand(commandText);
    }

    public void Dispose()
    {
        _dbConnection.Dispose();
    }

    public ContasPagar SelecionarPorIdCompra(int id)
    {
        throw new NotImplementedException();
    }
}