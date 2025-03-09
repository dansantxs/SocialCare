using Microsoft.AspNetCore.Mvc;
using SocialCare.DATA.Models;
using SocialCare.DATA.Services;
using SocialCare.WEB.Models;

namespace SocialCare.WEB.Controllers
{
    public class PessoasController : Controller
    {
        private PessoasService oPessoasService = new PessoasService();
        private PessoasFisicasService oPessoasFisicasService = new PessoasFisicasService();
        private PessoasJuridicasService oPessoasJuridicasService = new PessoasJuridicasService();
        public IActionResult Index()
        {
            var oListPessoas = oPessoasService.oRepositoryPessoas.SelecionarTodos();

            foreach (var pessoa in oListPessoas)
            {
                if (pessoa.Tipo == "F")
                {
                    pessoa.PessoasFisicas = oPessoasFisicasService.oRepositoryPessoasFisicas.SelecionarPK(pessoa.Id);
                }
                else if (pessoa.Tipo == "J")
                {
                    pessoa.PessoasJuridicas = oPessoasJuridicasService.oRepositoryPessoasJuridicas.SelecionarPK(pessoa.Id);
                }
            }

            return View(oListPessoas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PessoasViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pessoa = new Pessoas
                {
                    Nome = model.Nome,
                    Cidade = model.Cidade,
                    Bairro = model.Bairro,
                    Endereco = model.Endereco,
                    Numero = model.Numero,
                    Email = model.Email,
                    Telefone = model.Telefone,
                    Tipo = model.Tipo
                };

                if (model.Tipo == "F")
                {
                    var pessoaFisica = new PessoasFisicas
                    {
                        Cpf = model.Cpf,
                        DataNascimento = model.DataNascimento.Value,
                        IdNavigation = pessoa
                    };
                    oPessoasFisicasService.oRepositoryPessoasFisicas.Incluir(pessoaFisica);
                }
                else if (model.Tipo == "J")
                {
                    var pessoaJuridica = new PessoasJuridicas
                    {
                        Cnpj = model.Cnpj,
                        RazaoSocial = model.RazaoSocial,
                        IdNavigation = pessoa
                    };
                    oPessoasJuridicasService.oRepositoryPessoasJuridicas.Incluir(pessoaJuridica);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var pessoa = oPessoasService.oRepositoryPessoas.SelecionarPK(id);
            if (pessoa.Tipo == "F")
            {
                pessoa.PessoasFisicas = oPessoasFisicasService.oRepositoryPessoasFisicas.SelecionarPK(pessoa.Id);
            }
            else if (pessoa.Tipo == "J")
            {
                pessoa.PessoasJuridicas = oPessoasJuridicasService.oRepositoryPessoasJuridicas.SelecionarPK(pessoa.Id);
            }

            var model = new PessoasViewModel
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                Cidade = pessoa.Cidade,
                Bairro = pessoa.Bairro,
                Endereco = pessoa.Endereco,
                Numero = pessoa.Numero,
                Email = pessoa.Email,
                Telefone = pessoa.Telefone,
                Tipo = pessoa.Tipo,
                Cpf = pessoa.PessoasFisicas?.Cpf,
                DataNascimento = pessoa.PessoasFisicas?.DataNascimento,
                Cnpj = pessoa.PessoasJuridicas?.Cnpj,
                RazaoSocial = pessoa.PessoasJuridicas?.RazaoSocial
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var pessoa = oPessoasService.oRepositoryPessoas.SelecionarPK(id);
            if (pessoa != null)
            {
                if (pessoa.Tipo == "F")
                {
                    oPessoasFisicasService.oRepositoryPessoasFisicas.Excluir(pessoa.Id);
                }
                else if (pessoa.Tipo == "J")
                {
                    oPessoasJuridicasService.oRepositoryPessoasJuridicas.Excluir(pessoa.Id);
                }

                oPessoasService.oRepositoryPessoas.Excluir(id);
            }

            return RedirectToAction("Index");
        }
    }
}