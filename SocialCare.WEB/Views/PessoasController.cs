using Microsoft.AspNetCore.Mvc;
using SocialCare.WEB.Models;

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
        public IActionResult Create(PessoasViewModel model)
        {
            if (ModelState.IsValid)
            {
                oPessoasControl.CriarPessoa(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var pessoa = oPessoasControl.ObterPessoaPorId(id);
            if (pessoa == null)
            {
                return NotFound();
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

        public IActionResult Edit(int id)
        {
            var pessoa = oPessoasControl.ObterPessoaPorId(id);
            if (pessoa == null)
            {
                return NotFound();
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
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PessoasViewModel model)
        {
            if (ModelState.IsValid)
            {
                oPessoasControl.EditarPessoa(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            oPessoasControl.ExcluirPessoa(id);
            return RedirectToAction("Index");
        }
    }
}