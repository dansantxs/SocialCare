using SocialCare.DATA.Models;
using System;
using System.Collections.Generic;

public class ProdutosControl : IDisposable
{
    private static readonly Lazy<ProdutosControl> instance = new Lazy<ProdutosControl>(() => new ProdutosControl());

    private DBConnection _dbConnection;

    private ProdutosControl()
    {
        _dbConnection = new DBConnection();
    }

    public static ProdutosControl Instance => instance.Value;

    public List<Produtos> ObterTodosProdutos()
    {
        Produtos produtos = new Produtos();
        return produtos.SelecionarTodos(_dbConnection);
    }

    public Produtos ObterProdutosPorId(int id)
    {
        Produtos produtos = new Produtos();
        return produtos.SelecionarPorId(id, _dbConnection);
    }

    public void CriarProdutos(Produtos produto)
    {
        try
        {
            _dbConnection.BeginTransaction();
            produto.Incluir(_dbConnection);
            _dbConnection.Commit();
        }
        catch
        {
            _dbConnection.Rollback();
            throw;
        }
    }

    public void EditarProdutos(Produtos produto)
    {
        try
        {
            _dbConnection.BeginTransaction();
            produto.Alterar(_dbConnection);
            _dbConnection.Commit();
        }
        catch
        {
            _dbConnection.Rollback();
            throw;
        }
    }

    public void ExcluirProdutos(int id)
    {
        try
        {
            _dbConnection.BeginTransaction();
            Produtos produto = ObterProdutosPorId(id);
            if (produto != null)
            {
                produto.Excluir(_dbConnection);
            }
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