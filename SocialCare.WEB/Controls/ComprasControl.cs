﻿using SocialCare.DATA.Models;
using SocialCare.DATA.Observer;

public class ComprasControl : IDisposable
{
    private static readonly Lazy<ComprasControl> instance = new Lazy<ComprasControl>(() => new ComprasControl());

    private DBConnection _dbConnection;
    private ComprasSujeito _comprasSujeito;

    private ComprasControl()
    {
        _dbConnection = new DBConnection();
        _comprasSujeito = new ComprasSujeito();

        _comprasSujeito.AdicionarObservador(new EstoqueObservador());
    }

    public static ComprasControl Instance => instance.Value;

    public List<Compras> ObterTodasCompras()
    {
        Compras compras = new Compras();
        List<Compras> listaCompras = compras.SelecionarTodos(_dbConnection);

        foreach (var compra in listaCompras)
        {
            Pessoas pessoa = new Pessoas().SelecionarPorId(compra.IdPessoa, _dbConnection);
            compra.Pessoa = pessoa;

            ItensCompra itensCompra = new ItensCompra();
            List<ItensCompra> listaItensCompra = itensCompra.SelecionarPorIdCompra(compra.Id, _dbConnection);
            compra.ItensCompra = listaItensCompra;
        }

        return listaCompras;
    }

    public Compras ObterCompraPorId(int id)
    {
        Compras compra = new Compras().SelecionarPorId(id, _dbConnection);
        Pessoas pessoa = new Pessoas().SelecionarPorId(compra.IdPessoa, _dbConnection);
        compra.Pessoa = pessoa;

        ItensCompra itensCompra = new ItensCompra();
        List<ItensCompra> listaItensCompra = itensCompra.SelecionarPorIdCompra(compra.Id, _dbConnection);
        compra.ItensCompra = listaItensCompra;

        return compra;
    }

    public void CriarCompra(Compras compra)
    {
        try
        {
            _dbConnection.BeginTransaction();

            var itensAgrupados = compra.ItensCompra
                .GroupBy(i => i.IdProduto)
                .Select(g => new ItensCompra
                {
                    IdProduto = g.Key,
                    Quantidade = g.Sum(i => i.Quantidade),
                    PrecoUnitario = g.First().PrecoUnitario
                }).ToList();

            compra.Total = itensAgrupados.Sum(i => i.PrecoUnitario * i.Quantidade);
            compra.Incluir(_dbConnection);

            foreach (var item in itensAgrupados)
            {
                var itemCompra = new ItensCompra
                {
                    IdCompra = compra.Id,
                    IdProduto = item.IdProduto,
                    Quantidade = item.Quantidade,
                    PrecoUnitario = item.PrecoUnitario,
                    Subtotal = item.PrecoUnitario * item.Quantidade
                };

                itemCompra.Incluir(_dbConnection);

                _comprasSujeito.NotificarObservadores(itemCompra, _dbConnection);
            }

            var contaPagar = new ContasPagar
            {
                IdPessoa = compra.IdPessoa,
                IdCompra = compra.Id,
                Data = DateTime.Now,
                Valor = compra.Total,
                DataVencimento = DateTime.Now.AddDays(30)
            };

            contaPagar.Incluir(_dbConnection);

            _dbConnection.Commit();
        }
        catch
        {
            _dbConnection.Rollback();
            throw;
        }
    }

    public void EditarCompra(Compras compra)
    {
        try
        {
            _dbConnection.BeginTransaction();

            ItensCompra itensCompraObj = new ItensCompra();
            List<ItensCompra> itensAntigos = itensCompraObj.SelecionarPorIdCompra(compra.Id, _dbConnection);

            foreach (var itemAntigo in itensAntigos)
            {
                var produto = new Produtos().SelecionarPorId(itemAntigo.IdProduto, _dbConnection);
                produto.Estoque -= itemAntigo.Quantidade;
                produto.Alterar(_dbConnection);
            }

            compra.IdPessoa = compra.IdPessoa;
            compra.DataCompra = compra.DataCompra;

            var itensAgrupados = compra.ItensCompra
                .GroupBy(i => i.IdProduto)
                .Select(g => new ItensCompra
                {
                    IdProduto = g.Key,
                    Quantidade = g.Sum(i => i.Quantidade),
                    PrecoUnitario = g.First().PrecoUnitario
                }).ToList();

            compra.Total = itensAgrupados.Sum(i => i.PrecoUnitario * i.Quantidade);
            compra.Alterar(_dbConnection);

            foreach (var item in itensAntigos)
            {
                item.Excluir(_dbConnection);
            }

            foreach (var item in itensAgrupados)
            {
                var itemCompra = new ItensCompra
                {
                    IdCompra = compra.Id,
                    IdProduto = item.IdProduto,
                    Quantidade = item.Quantidade,
                    PrecoUnitario = item.PrecoUnitario,
                    Subtotal = item.PrecoUnitario * item.Quantidade
                };

                itemCompra.Incluir(_dbConnection);

                _comprasSujeito.NotificarObservadores(itemCompra, _dbConnection);
            }

            ContasPagar contaPagar = new ContasPagar().SelecionarPorIdCompra(compra.Id, _dbConnection);
            if (contaPagar != null)
            {
                contaPagar.Valor = compra.Total;
                contaPagar.Alterar(_dbConnection);
            }

            _dbConnection.Commit();
        }
        catch
        {
            _dbConnection.Rollback();
            throw;
        }
    }

    public void ExcluirCompra(int id)
    {
        try
        {
            _dbConnection.BeginTransaction();

            Compras compra = new Compras().SelecionarPorId(id, _dbConnection);

            ItensCompra itensCompraObj = new ItensCompra();
            List<ItensCompra> itensCompra = itensCompraObj.SelecionarPorIdCompra(compra.Id, _dbConnection);

            foreach (var item in itensCompra)
            {
                var produto = new Produtos().SelecionarPorId(item.IdProduto, _dbConnection);
                produto.Estoque -= item.Quantidade;
                produto.Alterar(_dbConnection);

                item.Excluir(_dbConnection);
            }

            ContasPagar contaPagar = new ContasPagar().SelecionarPorIdCompra(compra.Id, _dbConnection);
            if (contaPagar != null)
            {
                contaPagar.Excluir(_dbConnection);
            }

            compra.Excluir(_dbConnection);

            _dbConnection.Commit();
        }
        catch
        {
            _dbConnection.Rollback();
            throw;
        }
    }

    public List<Pessoas> ObterPessoas()
    {
        Pessoas pessoas = new Pessoas();
        return pessoas.SelecionarTodos(_dbConnection);
    }

    public List<Produtos> ObterProdutos()
    {
        Produtos produtos = new Produtos();
        return produtos.SelecionarTodos(_dbConnection);
    }

    public void Dispose()
    {
        _dbConnection.Dispose();
    }
}