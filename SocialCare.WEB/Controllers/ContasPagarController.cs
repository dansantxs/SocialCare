using Microsoft.AspNetCore.Mvc;
using SocialCare.WEB.Models;

namespace SocialCare.WEB.Controllers
{
    public class ContasPagarController : Controller
    {
        private readonly ContasPagarFacade oContasPagarFacade;

        public ContasPagarController()
        {
            oContasPagarFacade = ContasPagarFacade.Instance;
        }

        public IActionResult Index()
        {
            var viewModel = oContasPagarFacade.ObterTodasContasPagar();
            return View(viewModel);
        }

        public IActionResult Create()
        {
            ViewBag.Pessoas = oContasPagarFacade.ObterPessoas();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ContasPagarViewModel model)
        {
            if (ModelState.IsValid)
            {
                oContasPagarFacade.CriarContaPagar(model);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Pessoas = oContasPagarFacade.ObterPessoas();
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var viewModel = oContasPagarFacade.ObterContaPagarPorId(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var viewModel = oContasPagarFacade.ObterContaPagarPorId(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            ViewBag.Pessoas = oContasPagarFacade.ObterPessoas();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ContasPagarViewModel model)
        {
            if (ModelState.IsValid)
            {
                oContasPagarFacade.EditarContaPagar(model);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Pessoas = oContasPagarFacade.ObterPessoas();
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            oContasPagarFacade.ExcluirContaPagar(id);
            return RedirectToAction("Index");
        }
    }
}