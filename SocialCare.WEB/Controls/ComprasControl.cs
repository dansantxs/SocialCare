using System;
using System.Collections.Generic;
using System.Linq;
using SocialCare.DATA.Models;
using SocialCare.WEB.Models;

public class ComprasControl : IDisposable
{
    private static readonly Lazy<ComprasControl> instance = new Lazy<ComprasControl>(() => new ComprasControl());

    private DBConnection _dbConnection;

    private ComprasControl()
    {
        _dbConnection = new DBConnection();
    }

    public static ComprasControl Instance => instance.Value;

    public List<ComprasViewModel> ObterTodasCompras()
    {
        Compras compras = new Compras();
        List<Compras> listaCompras = compras.SelecionarTodos(_dbConnection);

        return listaCompras.Select(cp => new ComprasViewModel
        {
            Id = cp.Id,
            IdPessoa = cp.IdPessoa,
            DataCompra = cp.DataCompra,
            Total = cp.Total,
            NomePessoa = new Pessoas().SelecionarPorId(cp.IdPessoa, _dbConnection)?.Nome
        }).ToList();
    }

    public ComprasViewModel ObterCompraPorId(int id)
    {
        Compras compra = new Compras().SelecionarPorId(id, _dbConnection);
        Pessoas pessoa = new Pessoas().SelecionarPorId(compra.IdPessoa, _dbConnection);

        ItensCompra itensCompraObj = new ItensCompra();
        List<ItensCompra> itensCompra = itensCompraObj.SelecionarPorIdCompra(compra.Id, _dbConnection);

        return new ComprasViewModel
        {
            Id = compra.Id,
            IdPessoa = compra.IdPessoa,
            NomePessoa = pessoa?.Nome,
            DataCompra = compra.DataCompra,
            Total = compra.Total,
            Itens = itensCompra.Select(i => new ItensCompraViewModel
            {
                IdProduto = i.IdProduto,
                Quantidade = i.Quantidade,
                PrecoUnitario = i.PrecoUnitario,
                NomeProduto = new Produtos().SelecionarPorId(i.IdProduto, _dbConnection)?.Nome
            }).ToList()
        };
    }

    public void CriarCompra(ComprasViewModel model)
    {
        try
        {
            _dbConnection.BeginTransaction();

            var compra = new Compras
            {
                IdPessoa = model.IdPessoa,
                DataCompra = model.DataCompra,
                Total = 0
            };

            var groupedItems = model.Itens
                .GroupBy(i => i.IdProduto)
                .Select(g => new ItensCompraViewModel
                {
                    IdProduto = g.Key,
                    Quantidade = g.Sum(i => i.Quantidade),
                    PrecoUnitario = g.First().PrecoUnitario
                }).ToList();

            compra.Total = groupedItems.Sum(i => i.PrecoUnitario * i.Quantidade);
            compra.Incluir(_dbConnection);

            foreach (var item in groupedItems)
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

    public void EditarCompra(ComprasViewModel model)
    {
        try
        {
            _dbConnection.BeginTransaction();

            Compras compra = new Compras().SelecionarPorId(model.Id, _dbConnection);
            compra.IdPessoa = model.IdPessoa;
            compra.DataCompra = model.DataCompra;

            var groupedItems = model.Itens
                .GroupBy(i => i.IdProduto)
                .Select(g => new ItensCompraViewModel
                {
                    IdProduto = g.Key,
                    Quantidade = g.Sum(i => i.Quantidade),
                    PrecoUnitario = g.First().PrecoUnitario
                }).ToList();

            compra.Total = groupedItems.Sum(i => i.PrecoUnitario * i.Quantidade);
            compra.Alterar(_dbConnection);

            ItensCompra itensCompraObj = new ItensCompra();
            List<ItensCompra> itensAntigos = itensCompraObj.SelecionarPorIdCompra(compra.Id, _dbConnection);

            foreach (var item in itensAntigos)
            {
                item.Excluir(_dbConnection);
            }

            foreach (var item in groupedItems)
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