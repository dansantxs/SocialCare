using Microsoft.AspNetCore.Mvc;
using SocialCare.DATA.Models;
using System.ComponentModel.DataAnnotations;

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
        public IActionResult Create(ContasPagar contaPagar)
        {
            try
            {
                oContasPagarControl.CriarContaPagar(contaPagar);
                return RedirectToAction("Index");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.Pessoas = oContasPagarControl.ObterPessoas();
                return View(contaPagar);
            }
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
        public IActionResult Edit(ContasPagar contaPagar)
        {
            try
            {
                oContasPagarControl.EditarContaPagar(contaPagar);
                return RedirectToAction("Details", new { id = contaPagar.Id });
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(contaPagar);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            oContasPagarControl.ExcluirContaPagar(id);
            return RedirectToAction("Index");
        }
    }
}