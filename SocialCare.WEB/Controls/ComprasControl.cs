using SocialCare.DATA.Models;
using SocialCare.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class ComprasControl
{
    private static readonly Lazy<ComprasControl> instance = new Lazy<ComprasControl>(() => new ComprasControl());

    private ComprasDAO oComprasDAO { get; set; }
    private PessoasDAO oPessoasDAO { get; set; }
    private ProdutosDAO oProdutosDAO { get; set; }
    private ContasPagarDAO oContasPagarDAO { get; set; }

    private ComprasControl()
    {
        oComprasDAO = new ComprasDAO();
        oPessoasDAO = new PessoasDAO();
        oProdutosDAO = new ProdutosDAO();
        oContasPagarDAO = new ContasPagarDAO();
    }

    public static ComprasControl Instance => instance.Value;

    public List<ComprasViewModel> ObterTodasCompras()
    {
        var compras = oComprasDAO.SelecionarTodos();
        return compras.Select(c => new ComprasViewModel
        {
            Id = c.Id,
            IdPessoa = c.IdPessoa,
            NomePessoa = oPessoasDAO.SelecionarPorId(c.IdPessoa)?.Nome ?? "Desconhecido",
            DataCompra = c.DataCompra,
            Total = c.Total
        }).ToList();
    }

    public ComprasViewModel ObterCompraPorId(int id)
    {
        var compra = oComprasDAO.SelecionarPorId(id);
        if (compra == null) return null;

        var itensCompra = oComprasDAO.SelecionarItensPorCompraId(compra.Id);

        return new ComprasViewModel
        {
            Id = compra.Id,
            IdPessoa = compra.IdPessoa,
            NomePessoa = oPessoasDAO.SelecionarPorId(compra.IdPessoa)?.Nome ?? "Desconhecido",
            DataCompra = compra.DataCompra,
            Total = compra.Total,
            Itens = itensCompra.Select(i => new ItensCompraViewModel
            {
                IdProduto = i.IdProduto,
                Quantidade = i.Quantidade,
                PrecoUnitario = i.PrecoUnitario,
                NomeProduto = oProdutosDAO.SelecionarPorId(i.IdProduto)?.Nome ?? "Desconhecido"
            }).ToList()
        };
    }

    public void CriarCompra(ComprasViewModel model)
    {
        var compra = new Compras
        {
            IdPessoa = model.IdPessoa,
            DataCompra = model.DataCompra,
            Total = model.Itens.Sum(i => i.PrecoUnitario * i.Quantidade),
            ItensCompra = model.Itens.Select(i => new ItensCompra
            {
                IdProduto = i.IdProduto,
                Quantidade = i.Quantidade,
                PrecoUnitario = i.PrecoUnitario,
                Subtotal = i.PrecoUnitario * i.Quantidade
            }).ToList()
        };

        oComprasDAO.Incluir(compra);

        var contaPagar = new ContasPagar
        {
            IdPessoa = compra.IdPessoa,
            IdCompra = compra.Id,
            Data = DateTime.Now,
            Valor = compra.Total,
            DataVencimento = DateTime.Now.AddDays(30)
        };

        oContasPagarDAO.Incluir(contaPagar);
    }

    public void EditarCompra(ComprasViewModel model)
    {
        var compra = oComprasDAO.SelecionarPorId(model.Id);
        if (compra == null) throw new Exception("Compra não encontrada.");

        compra.IdPessoa = model.IdPessoa;
        compra.DataCompra = model.DataCompra;
        compra.Total = model.Itens.Sum(i => i.PrecoUnitario * i.Quantidade);
        compra.ItensCompra = model.Itens.Select(i => new ItensCompra
        {
            IdProduto = i.IdProduto,
            Quantidade = i.Quantidade,
            PrecoUnitario = i.PrecoUnitario,
            Subtotal = i.PrecoUnitario * i.Quantidade
        }).ToList();

        oComprasDAO.Alterar(compra);

        var contaPagar = oContasPagarDAO.SelecionarPorIdCompra(compra.Id);
        if (contaPagar != null)
        {
            contaPagar.Valor = compra.Total;
            oContasPagarDAO.Alterar(contaPagar);
        }
    }

    public void ExcluirCompra(int id)
    {
        var compra = oComprasDAO.SelecionarPorId(id);
        if (compra == null) throw new Exception("Compra não encontrada.");

        var contaPagar = oContasPagarDAO.SelecionarPorIdCompra(compra.Id);
        if (contaPagar != null)
        {
            oContasPagarDAO.Excluir(contaPagar.Id);
        }

        oComprasDAO.Excluir(compra.Id);
    }

    public List<Pessoas> ObterPessoas()
    {
        return oPessoasDAO.SelecionarTodos();
    }

    public List<Produtos> ObterProdutos()
    {
        return oProdutosDAO.SelecionarTodos();
    }
}