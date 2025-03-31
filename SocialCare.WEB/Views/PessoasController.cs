using Microsoft.AspNetCore.Mvc;
using SocialCare.DATA.Models;

namespace SocialCare.WEB.Views
{
    public class PessoasController : Controller
    {
        private readonly PessoasControl oPessoasControl;

        public PessoasController()
        {
            oPessoasControl = PessoasControl.Instance;
        }

        public IActionResult Index()
        {
            var oListPessoas = oPessoasControl.ObterTodasPessoas();
            return View(oListPessoas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pessoas model)
        {
            oPessoasControl.CriarPessoa(model);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var pessoa = oPessoasControl.ObterPessoaPorId(id);
            return View(pessoa);
        }

        public IActionResult Edit(int id)
        {
            var pessoa = oPessoasControl.ObterPessoaPorId(id);
            return View(pessoa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Pessoas model)
        {
            oPessoasControl.EditarPessoa(model);
            return RedirectToAction("Details", new { id = model.Id });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            oPessoasControl.ExcluirPessoa(id);
            return RedirectToAction("Index");
        }
    }
}