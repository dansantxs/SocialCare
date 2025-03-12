using Microsoft.AspNetCore.Mvc;
using SocialCare.DATA.Models;

namespace SocialCare.WEB.Controllers
{
    public class ParametrizacaoController : Controller
    {
        private readonly ParametrizacaoFacade oParametrizacaoFacade;

        public ParametrizacaoController()
        {
            oParametrizacaoFacade = ParametrizacaoFacade.Instance;
        }

        public IActionResult Index()
        {
            var oListParametrizacao = oParametrizacaoFacade.ObterTodasParametrizacoes();
            return View(oListParametrizacao);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Parametrizacao model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            oParametrizacaoFacade.CriarParametrizacao(model);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var oProduto = oParametrizacaoFacade.ObterParametrizacaoPorId(id);
            return View(oProduto);
        }

        public IActionResult Edit(int id)
        {
            var oProduto = oParametrizacaoFacade.ObterParametrizacaoPorId(id);
            return View(oProduto);
        }

        [HttpPost]
        public IActionResult Edit(Parametrizacao model)
        {
            oParametrizacaoFacade.EditarParametrizacao(model);
            return RedirectToAction("Details", new { id = model.Id });
        }

        public IActionResult Delete(int id)
        {
            oParametrizacaoFacade.ExcluirParametrizacao(id);
            return RedirectToAction("Index");
        }
    }
}