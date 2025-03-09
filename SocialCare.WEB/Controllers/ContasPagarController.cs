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
                    DataVencimento = model.DataVencimento,
                    DataPagamento = model.DataPagamento
                };

                oContasPagarService.oRepositoryContasPagar.Incluir(contaPagar);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Pessoas = oPessoasService.oRepositoryPessoas.SelecionarTodos();
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var contaPagar = oContasPagarService.oRepositoryContasPagar.SelecionarPK(id);

            if (contaPagar == null)
            {
                return NotFound();
            }

            var viewModel = new ContasPagarViewModel
            {
                Id = contaPagar.Id,
                IdPessoa = contaPagar.IdPessoa,
                NomePessoa = oPessoasService.oRepositoryPessoas.SelecionarPK(contaPagar.IdPessoa).Nome,
                Data = contaPagar.Data,
                Valor = contaPagar.Valor,
                DataVencimento = contaPagar.DataVencimento,
                DataPagamento = contaPagar.DataPagamento
            };

            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var contaPagar = oContasPagarService.oRepositoryContasPagar.SelecionarPK(id);
            if (contaPagar == null)
            {
                return NotFound();
            }

            var viewModel = new ContasPagarViewModel
            {
                Id = contaPagar.Id,
                IdPessoa = contaPagar.IdPessoa,
                Data = contaPagar.Data,
                Valor = contaPagar.Valor,
                DataVencimento = contaPagar.DataVencimento,
                DataPagamento = contaPagar.DataPagamento
            };

            ViewBag.Pessoas = oPessoasService.oRepositoryPessoas.SelecionarTodos();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ContasPagarViewModel model)
        {
            if (ModelState.IsValid)
            {
                var contaPagar = new ContasPagar
                {
                    Id = model.Id,
                    IdPessoa = model.IdPessoa,
                    Data = model.Data,
                    Valor = model.Valor,
                    DataVencimento = model.DataVencimento,
                    DataPagamento = model.DataPagamento
                };

                oContasPagarService.oRepositoryContasPagar.Alterar(contaPagar);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Pessoas = oPessoasService.oRepositoryPessoas.SelecionarTodos();
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            oContasPagarService.oRepositoryContasPagar.Excluir(id);
            return RedirectToAction("Index");
        }
    }
}