using SocialCare.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialCare.DATA.Models;
using SocialCare.DATA.Services;
using SocialCare.WEB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ComprasController : Controller
{
    private ComprasService oComprasService = new ComprasService();
    private ItensCompraService oItensComprasService = new ItensCompraService();
    private PessoasService oPessoasService = new PessoasService();
    private ProdutosService oprodutosService = new ProdutosService();

    public IActionResult Index()
    {
        var compra = oComprasService.oRepositoryCompras.SelecionarTodos();

        var viewModel = compra.Select(cp => new ComprasViewModel
        {
            Id = cp.Id,
            IdPessoa = cp.IdPessoa,
            NomePessoa = oPessoasService.oRepositoryPessoas.SelecionarPK(cp.IdPessoa).Nome,
            DataCompra = cp.DataCompra,
            Total = cp.Total
        }).ToList();

        return View(viewModel);
    }

    public IActionResult Create()
    {
        var pessoas = oPessoasService.oRepositoryPessoas.SelecionarTodos();
        var produtos = oprodutosService.oRepositoryProdutos.SelecionarTodos();

        ViewBag.Pessoas = pessoas;
        ViewBag.Produtos = produtos;

        return View();
    }

    [HttpPost]
    public IActionResult Create(ComprasViewModel model)
    {
        if (ModelState.IsValid)
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

                oItensComprasService.oRepositoryItensCompra.Incluir(itensCompra);
            }

            return RedirectToAction("Index");
        }

        ViewBag.Pessoas = oPessoasService.oRepositoryPessoas.SelecionarTodos();
        ViewBag.Produtos = oprodutosService.oRepositoryProdutos.SelecionarTodos();

        return View(model);
    }

    public IActionResult Details(int id)
    {
        var compra = oComprasService.oRepositoryCompras.SelecionarPK(id);
        var pessoa = oPessoasService.oRepositoryPessoas.SelecionarPK(compra.IdPessoa);

        var itensCompra = oItensComprasService.oRepositoryItensCompra.SelecionarTodos()
            .Where(i => i.IdCompra == compra.Id)
            .ToList();

        var detalhesViewModel = new ComprasViewModel
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
                NomeProduto = oprodutosService.oRepositoryProdutos.SelecionarPK(i.IdProduto)?.Nome
            }).ToList()
        };

        return View(detalhesViewModel);
    }
}