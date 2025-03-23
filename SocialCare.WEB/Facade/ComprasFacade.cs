using SocialCare.DATA.Models;
using SocialCare.WEB.Models;

public class ComprasFacade
{
    private static readonly Lazy<ComprasFacade> instance = new Lazy<ComprasFacade>(() => new ComprasFacade());

    private ComprasDAO oComprasDAO { get; set; }
    private ItensCompraDAO oItensCompraDAO { get; set; }
    private PessoasDAO oPessoasDAO { get; set; }
    private ProdutosDAO oProdutosDAO { get; set; }
    private ContasPagarDAO oContasPagarDAO { get; set; }

    private ComprasFacade()
    {
        oComprasDAO = new ComprasDAO();
        oItensCompraDAO = new ItensCompraDAO();
        oPessoasDAO = new PessoasDAO();
        oProdutosDAO = new ProdutosDAO();
        oContasPagarDAO = new ContasPagarDAO();
    }

    public static ComprasFacade Instance => instance.Value;

    public List<ComprasViewModel> ObterTodasCompras()
    {
        var compras = oComprasDAO.SelecionarTodos();
        return compras.Select(cp => new ComprasViewModel
        {
            Id = cp.Id,
            IdPessoa = cp.IdPessoa,
            NomePessoa = oPessoasDAO.SelecionarPorId(cp.IdPessoa)?.Nome,
            DataCompra = cp.DataCompra,
            Total = cp.Total
        }).ToList();
    }

    public ComprasViewModel ObterCompraPorId(int id)
    {
        var compra = oComprasDAO.SelecionarPorId(id);
        var pessoa = oPessoasDAO.SelecionarPorId(compra.IdPessoa);
        var itensCompra = oItensCompraDAO.SelecionarTodos()
            .Where(i => i.IdCompra == compra.Id)
            .ToList();

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
                NomeProduto = oProdutosDAO.SelecionarPorId(i.IdProduto)?.Nome
            }).ToList()
        };
    }

    public void CriarCompra(ComprasViewModel model)
    {
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
        oComprasDAO.Incluir(compra);

        foreach (var item in groupedItems)
        {
            var itensCompra = new ItensCompra
            {
                IdCompra = compra.Id,
                IdProduto = item.IdProduto,
                Quantidade = item.Quantidade,
                PrecoUnitario = item.PrecoUnitario,
                Subtotal = item.PrecoUnitario * item.Quantidade
            };

            oItensCompraDAO.Incluir(itensCompra);
        }

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
        oComprasDAO.Alterar(compra);

        var itensAntigos = oItensCompraDAO.SelecionarTodos()
            .Where(i => i.IdCompra == compra.Id)
            .ToList();

        foreach (var item in itensAntigos)
        {
            oItensCompraDAO.Excluir(item.Id);
        }

        foreach (var item in groupedItems)
        {
            var itensCompra = new ItensCompra
            {
                IdCompra = compra.Id,
                IdProduto = item.IdProduto,
                Quantidade = item.Quantidade,
                PrecoUnitario = item.PrecoUnitario,
                Subtotal = item.PrecoUnitario * item.Quantidade
            };

            oItensCompraDAO.Incluir(itensCompra);
        }

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
        var itensCompra = oItensCompraDAO.SelecionarTodos()
            .Where(i => i.IdCompra == compra.Id)
            .ToList();

        foreach (var item in itensCompra)
        {
            oItensCompraDAO.Excluir(item.Id);
        }

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