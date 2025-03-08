using Microsoft.AspNetCore.Mvc;
using SocialCare.DATA.Models;
using SocialCare.DATA.Services;

namespace SocialCare.WEB.Controllers
{
    public class ParametrizacaoController : Controller
    {
        private ParametrizacaoService oParametrizacaoService = new ParametrizacaoService();
        public IActionResult Index()
        {
            List<Parametrizacao> oListParametrizacao = oParametrizacaoService.oRepositoryParametrizacao.SelecionarTodos();
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

            oParametrizacaoService.oRepositoryParametrizacao.Incluir(model);

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            Parametrizacao oProduto = oParametrizacaoService.oRepositoryParametrizacao.SelecionarPK(id);
            return View(oProduto);
        }

        public IActionResult Edit(int id)
        {
            Parametrizacao oProduto = oParametrizacaoService.oRepositoryParametrizacao.SelecionarPK(id);
            return View(oProduto);
        }

        [HttpPost]
        public IActionResult Edit(Parametrizacao model)
        {
            Parametrizacao oProduto = oParametrizacaoService.oRepositoryParametrizacao.Alterar(model);

            int id = oProduto.Id;

            return RedirectToAction("Details", new { id });
        }

        public IActionResult Delete(int id)
        {
            oParametrizacaoService.oRepositoryParametrizacao.Excluir(id);
            return RedirectToAction("Index");
        }
    }
}