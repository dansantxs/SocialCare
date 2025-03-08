using Microsoft.AspNetCore.Mvc;
using SocialCare.DATA.Models;
using SocialCare.DATA.Services;

namespace SocialCare.WEB.Controllers
{
    public class ProdutosController : Controller
    {
        private ProdutosService oProdutosService = new ProdutosService();
        public IActionResult Index()
        {
            List<Produtos> oListProdutos = oProdutosService.oRepositoryProdutos.SelecionarTodos();
            return View(oListProdutos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Produtos model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            oProdutosService.oRepositoryProdutos.Incluir(model);

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            Produtos oProduto = oProdutosService.oRepositoryProdutos.SelecionarPK(id);
            return View(oProduto);
        }

        public IActionResult Edit(int id)
        {
            Produtos oProduto = oProdutosService.oRepositoryProdutos.SelecionarPK(id);
            return View(oProduto);
        }

        [HttpPost]
        public IActionResult Edit(Produtos model)
        {
            Produtos oProduto = oProdutosService.oRepositoryProdutos.Alterar(model);

            int id = oProduto.Id;

            return RedirectToAction("Details", new { id });
        }

        public IActionResult Delete(int id)
        {
            oProdutosService.oRepositoryProdutos.Excluir(id);
            return RedirectToAction("Index");
        }
    }
}