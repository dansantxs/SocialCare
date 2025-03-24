using Microsoft.AspNetCore.Mvc;
using SocialCare.DATA.Models;

namespace SocialCare.WEB.Views
{
    public class ProdutosController : Controller
    {
        private readonly ProdutosControl oProdutosControl;

        public ProdutosController()
        {
            oProdutosControl = ProdutosControl.Instance;
        }

        public IActionResult Index()
        {
            var oListProdutos = oProdutosControl.ObterTodosProdutos();
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

            oProdutosControl.CriarProdutos(model);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var oProduto = oProdutosControl.ObterProdutosPorId(id);
            return View(oProduto);
        }

        public IActionResult Edit(int id)
        {
            var oProduto = oProdutosControl.ObterProdutosPorId(id);
            return View(oProduto);
        }

        [HttpPost]
        public IActionResult Edit(Produtos model)
        {
            oProdutosControl.EditarProdutos(model);
            return RedirectToAction("Details", new { id = model.Id });
        }

        public IActionResult Delete(int id)
        {
            oProdutosControl.ExcluirProdutos(id);
            return RedirectToAction("Index");
        }
    }
}