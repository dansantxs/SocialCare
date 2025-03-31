using Microsoft.AspNetCore.Mvc;
using SocialCare.DATA.Models;

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
            var listaContaPagar = oContasPagarControl.ObterTodasContasPagar();
            return View(listaContaPagar);
        }

        public IActionResult Create()
        {
            ViewBag.Pessoas = oContasPagarControl.ObterPessoas();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ContasPagar model)
        {
            oContasPagarControl.CriarContaPagar(model);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var contaPagar = oContasPagarControl.ObterContaPagarPorId(id);
            return View(contaPagar);
        }

        public IActionResult Edit(int id)
        {
            var contaPagar = oContasPagarControl.ObterContaPagarPorId(id);
            ViewBag.Pessoas = oContasPagarControl.ObterPessoas();
            return View(contaPagar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ContasPagar model)
        {
            oContasPagarControl.EditarContaPagar(model);
            return RedirectToAction("Details", new { id = model.Id });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            oContasPagarControl.ExcluirContaPagar(id);
            return RedirectToAction("Index");
        }
    }
}