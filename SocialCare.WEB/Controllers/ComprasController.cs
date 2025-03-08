using EmprestimoLivros.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialCare.DATA.Models;
using SocialCare.DATA.Services;
using SocialCare.WEB.Models;

public class ComprasController : Controller
{
    private ComprasService oComprasService = new ComprasService();
    private ItensCompraService oItensComprasService = new ItensCompraService();
    private ProdutosService oprodutosService = new ProdutosService();

    public IActionResult Index()
    {
        List<Compras> oListCompras = oComprasService.oRepositoryCompras.SelecionarTodos();
        return View(oListCompras);
    }

    public IActionResult Create()
    {
        var produtos = oprodutosService.oRepositoryProdutos.SelecionarTodos()
            .OrderBy(p => p.Nome) 
            .ToList();

        ViewBag.Produtos = produtos;

        var model = new ComprasViewModel
        {
            DataCompra = DateTime.Today,
            Itens = new List<ItensCompraViewModel> { new ItensCompraViewModel() }
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ComprasViewModel model, string itensJson)
    {
        if (ModelState.IsValid)
        {
            var itens = JsonConvert.DeserializeObject<List<ItensCompraViewModel>>(itensJson);

            var itensAgrupados = itens.GroupBy(i => i.IdProduto)
                .Select(g => new ItensCompraViewModel
                {
                    IdProduto = g.Key,
                    Quantidade = g.Sum(i => i.Quantidade),
                    PrecoUnitario = g.First().PrecoUnitario
                })
                .ToList();

            var compra = new Compras
            {
                DataCompra = model.DataCompra,
                Total = itensAgrupados.Sum(i => i.PrecoUnitario * i.Quantidade)
            };

            oComprasService.oRepositoryCompras.Incluir(compra);

            foreach (var item in itensAgrupados)
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

            oComprasService.oRepositoryCompras.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction("Index");
    }

    public IActionResult Details(int id)
    {
        var compra = oComprasService.oRepositoryCompras.SelecionarPK(id);
        var itensCompra = oItensComprasService.oRepositoryItensCompra.SelecionarPorCompraId(id);

        var model = new ComprasViewModel
        {
            DataCompra = compra.DataCompra,
            Itens = itensCompra.Select(ic => new ItensCompraViewModel
            {
                IdProduto = ic.IdProduto,
                Quantidade = ic.Quantidade,
                PrecoUnitario = ic.PrecoUnitario
            }).ToList()
        };

        foreach (var item in model.Itens)
        {
            var produto = oprodutosService.oRepositoryProdutos.SelecionarPK(item.IdProduto);
            if (produto != null)
            {
                item.NomeProduto = produto.Nome;
            }
        }

        return View(model);
    }
}