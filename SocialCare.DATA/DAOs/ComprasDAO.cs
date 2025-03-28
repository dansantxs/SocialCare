using SocialCare.DATA.Models;
using System.ComponentModel;
using System.Data;

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

    public List<ItensCompra> SelecionarItensPorCompraId(int idCompra)
    {
        string query = $"SELECT * FROM ItensCompra WHERE idCompra = {idCompra}";
        DataTable dataTable = _dbConnection.ExecuteQuery(query);

        return dataTable.AsEnumerable().Select(row => new ItensCompra
        {
            IdProduto = row.Field<int>("idProduto"),
            Quantidade = row.Field<int>("quantidade"),
            PrecoUnitario = row.Field<decimal>("precoUnitario"),
            Subtotal = row.Field<decimal>("subtotal")
        }).ToList();
    }

    public void Incluir(Compras compra)
    {
        try
        {
            _dbConnection.BeginTransaction();

            string commandText = $"INSERT INTO Compras (idPessoa, dataCompra, total) VALUES ({compra.IdPessoa}, '{compra.DataCompra}', {compra.Total});";
            _dbConnection.ExecuteCommand(commandText);

            commandText = $"SELECT CAST(scope_identity() AS int);";
            int idCompra = _dbConnection.ExecuteCommand(commandText);

            foreach (var item in compra.ItensCompra)
            {
                commandText = $"INSERT INTO ItensCompra (idCompra, idProduto, quantidade, precoUnitario) VALUES ({idCompra}, {item.IdProduto}, {item.Quantidade}, {item.PrecoUnitario})";
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

    public void Alterar(Compras compra)
    {
        try
        {
            _dbConnection.BeginTransaction();

            string commandText = $"UPDATE Compras SET idPessoa = {compra.IdPessoa}, dataCompra = '{compra.DataCompra}', total = {compra.Total} WHERE id = {compra.Id}";
            _dbConnection.ExecuteCommand(commandText);

            var itensExistentes = SelecionarItensPorCompraId(compra.Id);

            var itensExistentesDict = itensExistentes
                .ToDictionary(item => item.IdProduto, item => item.Id);

            foreach (var item in compra.ItensCompra)
            {
                if (itensExistentesDict.TryGetValue(item.IdProduto, out var idItemExistente))
                {
                    commandText = $"UPDATE ItensCompra SET quantidade = {item.Quantidade}, precoUnitario = {item.PrecoUnitario} WHERE id = {idItemExistente}";
                    _dbConnection.ExecuteCommand(commandText);
                }
                else
                {
                    commandText = $"INSERT INTO ItensCompra (idCompra, idProduto, quantidade, precoUnitario) VALUES ({compra.Id}, {item.IdProduto}, {item.Quantidade}, {item.PrecoUnitario})";
                    _dbConnection.ExecuteCommand(commandText);
                }
            }

            var idsItensCompra = compra.ItensCompra.Select(i => i.IdProduto).ToList();
            var itensParaRemover = itensExistentes
                .Where(item => !idsItensCompra.Contains(item.IdProduto))
                .Select(item => item.Id)
                .ToList();

            foreach (var idItem in itensParaRemover)
            {
                commandText = $"DELETE FROM ItensCompra WHERE id = {idItem}";
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

            string commandText = $"DELETE FROM ItensCompra WHERE idCompra = {id}";
            _dbConnection.ExecuteCommand(commandText);

            commandText = $"DELETE FROM Compras WHERE id = {id}";
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