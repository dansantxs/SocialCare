using SocialCare.DATA.Models;
using SocialCare.DATA.Repositories;
using SocialCare.WEB.Models;

public class ComprasFacade
{
    private static readonly Lazy<ComprasFacade> instance = new Lazy<ComprasFacade>(() => new ComprasFacade());

    private RepositoryCompras oRepositoryCompras { get; set; }
    private RepositoryItensCompra oRepositoryItensCompra { get; set; }
    private RepositoryPessoas oRepositoryPessoas { get; set; }
    private RepositoryProdutos oRepositoryProdutos { get; set; }
    private RepositoryContasPagar oRepositoryContasPagar { get; set; }

    private ComprasFacade()
    {
        oRepositoryCompras = new RepositoryCompras();
        oRepositoryItensCompra = new RepositoryItensCompra();
        oRepositoryPessoas = new RepositoryPessoas();
        oRepositoryProdutos = new RepositoryProdutos();
        oRepositoryContasPagar = new RepositoryContasPagar();
    }

    public static ComprasFacade Instance => instance.Value;

    public List<ComprasViewModel> ObterTodasCompras()
    {
        var compras = oRepositoryCompras.SelecionarTodos();
        return compras.Select(cp => new ComprasViewModel
        {
            Id = cp.Id,
            IdPessoa = cp.IdPessoa,
            NomePessoa = oRepositoryPessoas.SelecionarPorId(cp.IdPessoa)?.Nome,
            DataCompra = cp.DataCompra,
            Total = cp.Total
        }).ToList();
    }

    public ComprasViewModel ObterCompraPorId(int id)
    {
        var compra = oRepositoryCompras.SelecionarPorId(id);
        var pessoa = oRepositoryPessoas.SelecionarPorId(compra.IdPessoa);
        var itensCompra = oRepositoryItensCompra.SelecionarTodos()
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
                NomeProduto = oRepositoryProdutos.SelecionarPorId(i.IdProduto)?.Nome
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
        oRepositoryCompras.Incluir(compra);

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

            oRepositoryItensCompra.Incluir(itensCompra);
        }

        var contaPagar = new ContasPagar
        {
            IdPessoa = compra.IdPessoa,
            IdCompra = compra.Id,
            Data = DateTime.Now,
            Valor = compra.Total,
            DataVencimento = DateTime.Now.AddDays(30)
        };

        oRepositoryContasPagar.Incluir(contaPagar);
    }

    public void EditarCompra(ComprasViewModel model)
    {
        var compra = oRepositoryCompras.SelecionarPorId(model.Id);
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
        oRepositoryCompras.Alterar(compra);

        var itensAntigos = oRepositoryItensCompra.SelecionarTodos()
            .Where(i => i.IdCompra == compra.Id)
            .ToList();

        foreach (var item in itensAntigos)
        {
            oRepositoryItensCompra.Excluir(item);
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

            oRepositoryItensCompra.Incluir(itensCompra);
        }

        var contaPagar = oRepositoryContasPagar.SelecionarPorIdCompra(compra.Id);
        if (contaPagar != null)
        {
            contaPagar.Valor = compra.Total;
            oRepositoryContasPagar.Alterar(contaPagar);
        }
    }

    public void ExcluirCompra(int id)
    {
        var compra = oRepositoryCompras.SelecionarPorId(id);
        var itensCompra = oRepositoryItensCompra.SelecionarTodos()
            .Where(i => i.IdCompra == compra.Id)
            .ToList();

        foreach (var item in itensCompra)
        {
            oRepositoryItensCompra.Excluir(item);
        }

        var contaPagar = oRepositoryContasPagar.SelecionarPorIdCompra(compra.Id);
        if (contaPagar != null)
        {
            oRepositoryContasPagar.Excluir(contaPagar.Id);
        }

        oRepositoryCompras.Excluir(compra);
    }

    public List<Pessoas> ObterPessoas()
    {
        return oRepositoryPessoas.SelecionarTodos();
    }

    public List<Produtos> ObterProdutos()
    {
        return oRepositoryProdutos.SelecionarTodos();
    }
}