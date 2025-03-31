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
            var listaPessoas = oPessoasControl.ObterTodasPessoas();
            return View(listaPessoas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pessoas pessoa)
        {
            oPessoasControl.CriarPessoa(pessoa);
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
        public IActionResult Edit(Pessoas pessoa)
        {
            oPessoasControl.EditarPessoa(pessoa);
            return RedirectToAction("Details", new { id = pessoa.Id });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            oPessoasControl.ExcluirPessoa(id);
            return RedirectToAction("Index");
        }
    }
}