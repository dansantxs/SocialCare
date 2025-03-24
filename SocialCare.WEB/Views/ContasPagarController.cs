using Microsoft.AspNetCore.Mvc;
using SocialCare.WEB.Models;

namespace SocialCare.WEB.Views
{
    public class ContasPagarController : Controller
    {
        private readonly ContasPagarControl oContasPagarControl;

        public ContasPagarController()
        {
            oContasPagarControl = ContasPagarControl.Instance;
        }

        public IActionResult Index()
        {
            var viewModel = oContasPagarControl.ObterTodasContasPagar();
            return View(viewModel);
        }

        public IActionResult Create()
        {
            ViewBag.Pessoas = oContasPagarControl.ObterPessoas();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ContasPagarViewModel model)
        {
            if (ModelState.IsValid)
            {
                oContasPagarControl.CriarContaPagar(model);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Pessoas = oContasPagarControl.ObterPessoas();
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var viewModel = oContasPagarControl.ObterContaPagarPorId(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var viewModel = oContasPagarControl.ObterContaPagarPorId(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            ViewBag.Pessoas = oContasPagarControl.ObterPessoas();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ContasPagarViewModel model)
        {
            if (ModelState.IsValid)
            {
                oContasPagarControl.EditarContaPagar(model);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Pessoas = oContasPagarControl.ObterPessoas();
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            oContasPagarControl.ExcluirContaPagar(id);
            return RedirectToAction("Index");
        }
    }
}