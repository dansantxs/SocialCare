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
            var listaProdutos = oProdutosControl.ObterTodosProdutos();
            return View(listaProdutos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Produtos produto)
        {
            oProdutosControl.CriarProdutos(produto);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var produto = oProdutosControl.ObterProdutosPorId(id);
            return View(produto);
        }

        public IActionResult Edit(int id)
        {
            var produto = oProdutosControl.ObterProdutosPorId(id);
            return View(produto);
        }

        [HttpPost]
        public IActionResult Edit(Produtos produto)
        {
            oProdutosControl.EditarProdutos(produto);
            return RedirectToAction("Details", new { id = produto.Id });
        }
        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            oProdutosControl.ExcluirProdutos(id);
            return RedirectToAction("Index");
        }
    }
}