using Microsoft.AspNetCore.Mvc;
using SocialCare.WEB.Models;

namespace SocialCare.WEB.Controllers
{
    public class ComprasController : Controller
    {
        private readonly ComprasFacade oComprasFacade;

        public ComprasController()
        {
            oComprasFacade = new ComprasFacade();
        }

        public IActionResult Index()
        {
            var viewModel = oComprasFacade.ObterTodasCompras();
            return View(viewModel);
        }

        public IActionResult Create()
        {
            ViewBag.Pessoas = oComprasFacade.ObterPessoas();
            ViewBag.Produtos = oComprasFacade.ObterProdutos();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ComprasViewModel model)
        {
            if (ModelState.IsValid)
            {
                oComprasFacade.CriarCompra(model);
                return RedirectToAction("Index");
            }

            ViewBag.Pessoas = oComprasFacade.ObterPessoas();
            ViewBag.Produtos = oComprasFacade.ObterProdutos();
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var detalhesViewModel = oComprasFacade.ObterCompraPorId(id);
            return View(detalhesViewModel);
        }

        public IActionResult Edit(int id)
        {
            var editViewModel = oComprasFacade.ObterCompraPorId(id);
            ViewBag.Pessoas = oComprasFacade.ObterPessoas();
            ViewBag.Produtos = oComprasFacade.ObterProdutos();
            return View(editViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ComprasViewModel model)
        {
            if (ModelState.IsValid)
            {
                oComprasFacade.EditarCompra(model);
                return RedirectToAction("Index");
            }

            ViewBag.Pessoas = oComprasFacade.ObterPessoas();
            ViewBag.Produtos = oComprasFacade.ObterProdutos();
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            oComprasFacade.ExcluirCompra(id);
            return RedirectToAction("Index");
        }
    }
}