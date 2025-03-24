using Microsoft.AspNetCore.Mvc;
using SocialCare.WEB.Models;

namespace SocialCare.WEB.Views
{
    public class ComprasController : Controller
    {
        private readonly ComprasControl oComprasControl;

        public ComprasController()
        {
            oComprasControl = ComprasControl.Instance;
        }

        public IActionResult Index()
        {
            var viewModel = oComprasControl.ObterTodasCompras();
            return View(viewModel);
        }

        public IActionResult Create()
        {
            ViewBag.Pessoas = oComprasControl.ObterPessoas();
            ViewBag.Produtos = oComprasControl.ObterProdutos();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ComprasViewModel model)
        {
            if (ModelState.IsValid)
            {
                oComprasControl.CriarCompra(model);
                return RedirectToAction("Index");
            }

            ViewBag.Pessoas = oComprasControl.ObterPessoas();
            ViewBag.Produtos = oComprasControl.ObterProdutos();
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var detalhesViewModel = oComprasControl.ObterCompraPorId(id);
            return View(detalhesViewModel);
        }

        public IActionResult Edit(int id)
        {
            var editViewModel = oComprasControl.ObterCompraPorId(id);
            ViewBag.Pessoas = oComprasControl.ObterPessoas();
            ViewBag.Produtos = oComprasControl.ObterProdutos();
            return View(editViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ComprasViewModel model)
        {
            if (ModelState.IsValid)
            {
                oComprasControl.EditarCompra(model);
                return RedirectToAction("Index");
            }

            ViewBag.Pessoas = oComprasControl.ObterPessoas();
            ViewBag.Produtos = oComprasControl.ObterProdutos();
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            oComprasControl.ExcluirCompra(id);
            return RedirectToAction("Index");
        }
    }
}