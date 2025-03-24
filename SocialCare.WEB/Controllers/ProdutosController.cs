using Microsoft.AspNetCore.Mvc;
using SocialCare.DATA.Models;

namespace SocialCare.WEB.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ProdutosFacade oProdutosFacade;

        public ProdutosController()
        {
            oProdutosFacade = ProdutosFacade.Instance;
        }

        public IActionResult Index()
        {
            var oListProdutos = oProdutosFacade.ObterTodosProdutos();
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

            oProdutosFacade.CriarProdutos(model);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var oProduto = oProdutosFacade.ObterProdutosPorId(id);
            return View(oProduto);
        }

        public IActionResult Edit(int id)
        {
            var oProduto = oProdutosFacade.ObterProdutosPorId(id);
            return View(oProduto);
        }

        [HttpPost]
        public IActionResult Edit(Produtos model)
        {
            oProdutosFacade.EditarProdutos(model);
            return RedirectToAction("Details", new { id = model.Id });
        }

        public IActionResult Delete(int id)
        {
            oProdutosFacade.ExcluirProdutos(id);
            return RedirectToAction("Index");
        }
    }
}