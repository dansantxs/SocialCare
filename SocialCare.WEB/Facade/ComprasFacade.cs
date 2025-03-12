using SocialCare.DATA.Models;
using SocialCare.DATA.Services;
using SocialCare.WEB.Models;

public class ComprasFacade
{
    private static readonly Lazy<ComprasFacade> instance = new Lazy<ComprasFacade>(() => new ComprasFacade());

    private readonly ComprasService oComprasService;
    private readonly ItensCompraService oItensCompraService;
    private readonly PessoasService oPessoasService;
    private readonly ProdutosService oProdutosService;

    private ComprasFacade()
    {
        oComprasService = new ComprasService();
        oItensCompraService = new ItensCompraService();
        oPessoasService = new PessoasService();
        oProdutosService = new ProdutosService();
    }

    public static ComprasFacade Instance => instance.Value;

    public List<ComprasViewModel> ObterTodasCompras()
    {
        var compras = oComprasService.oRepositoryCompras.SelecionarTodos();
        return compras.Select(cp => new ComprasViewModel
        {
            Id = cp.Id,
            IdPessoa = cp.IdPessoa,
            NomePessoa = oPessoasService.oRepositoryPessoas.SelecionarPK(cp.IdPessoa).Nome,
            DataCompra = cp.DataCompra,
            Total = cp.Total
        }).ToList();
    }

    public ComprasViewModel ObterCompraPorId(int id)
    {
        var compra = oComprasService.oRepositoryCompras.SelecionarPK(id);
        var pessoa = oPessoasService.oRepositoryPessoas.SelecionarPK(compra.IdPessoa);
        var itensCompra = oItensCompraService.oRepositoryItensCompra.SelecionarTodos()
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
                NomeProduto = oProdutosService.oRepositoryProdutos.SelecionarPK(i.IdProduto)?.Nome
            }).ToList()
        };
    }

    public void CriarCompra(ComprasViewModel model)
    {
        var compra = new Compras
        {
            IdPessoa = model.IdPessoa.Value,
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
        oComprasService.oRepositoryCompras.Incluir(compra);

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

            oItensCompraService.oRepositoryItensCompra.Incluir(itensCompra);
        }
    }

    public void EditarCompra(ComprasViewModel model)
    {
        var compra = oComprasService.oRepositoryCompras.SelecionarPK(model.Id);
        compra.IdPessoa = model.IdPessoa.Value;
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
        oComprasService.oRepositoryCompras.Alterar(compra);

        var itensAntigos = oItensCompraService.oRepositoryItensCompra.SelecionarTodos()
            .Where(i => i.IdCompra == compra.Id)
            .ToList();

        foreach (var item in itensAntigos)
        {
            oItensCompraService.oRepositoryItensCompra.Excluir(item);
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

            oItensCompraService.oRepositoryItensCompra.Incluir(itensCompra);
        }
    }

    public void ExcluirCompra(int id)
    {
        var compra = oComprasService.oRepositoryCompras.SelecionarPK(id);
        var itensCompra = oItensCompraService.oRepositoryItensCompra.SelecionarTodos()
            .Where(i => i.IdCompra == compra.Id)
            .ToList();

        foreach (var item in itensCompra)
        {
            oItensCompraService.oRepositoryItensCompra.Excluir(item);
        }

        oComprasService.oRepositoryCompras.Excluir(compra);
    }

    public List<Pessoas> ObterPessoas()
    {
        return oPessoasService.oRepositoryPessoas.SelecionarTodos();
    }

    public List<Produtos> ObterProdutos()
    {
        return oProdutosService.oRepositoryProdutos.SelecionarTodos();
    }
}