using Microsoft.AspNetCore.Mvc;
using SocialCare.DATA.Models;
using SocialCare.DATA.Services;
using SocialCare.WEB.Models;
using System.Collections.Generic;
using System.Linq;

namespace SocialCare.WEB.Controllers
{
    public class ContasPagarController : Controller
    {
        private ContasPagarService oContasPagarService = new ContasPagarService();
        private PessoasService oPessoasService = new PessoasService();

        public IActionResult Index()
        {
            var contasPagar = oContasPagarService.oRepositoryContasPagar.SelecionarTodos();

            var viewModel = contasPagar.Select(cp => new ContasPagarViewModel
            {
                Id = cp.Id,
                IdPessoa = cp.IdPessoa,
                NomePessoa = oPessoasService.oRepositoryPessoas.SelecionarPK(cp.IdPessoa).Nome,
                Data = cp.Data,
                Valor = cp.Valor,
                DataVencimento = cp.DataVencimento,
                DataPagamento = cp.DataPagamento
            }).ToList();

            return View(viewModel);
        }

        public IActionResult Create()
        {
            ViewBag.Pessoas = oPessoasService.oRepositoryPessoas.SelecionarTodos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ContasPagarViewModel model)
        {
            if (ModelState.IsValid)
            {
                var contaPagar = new ContasPagar
                {
                    IdPessoa = model.IdPessoa,
                    Data = model.Data,
                    Valor = model.Valor,
                    DataVencimento = model.DataVencimento 
                };

                oContasPagarService.oRepositoryContasPagar.Incluir(contaPagar);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Pessoas = oPessoasService.oRepositoryPessoas.SelecionarTodos();
            return View(model);
        }
    }
}