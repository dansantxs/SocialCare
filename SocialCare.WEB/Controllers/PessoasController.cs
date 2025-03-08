using Microsoft.AspNetCore.Mvc;
using SocialCare.DATA.Models;
using SocialCare.DATA.Services;

namespace SocialCare.WEB.Controllers
{
    public class PessoasController : Controller
    {
        private PessoasService oPessoasService = new PessoasService();
        private VwPessoasFisicasService oVwPessoasFisicasService = new VwPessoasFisicasService();
        private VwPessoasJuridicasService oVwPessoasJuridicasService = new VwPessoasJuridicasService();
        public IActionResult Index()
        {
            List<Pessoas> oListPessoas = oPessoasService.oRepositoryPessoas.SelecionarTodos();
            return View(oListPessoas);
        }

        public IActionResult Details(int id)
        {
            Pessoas oPessoa = oPessoasService.oRepositoryPessoas.SelecionarPK(id);
            if (oPessoa.Tipo == "F")
            {
                VwPessoasFisicas oPessoaFisica = oVwPessoasFisicasService.oRepositoryVwPessoasFisicas.SelecionarPK(id);
                return View("DetailsFisica", oPessoaFisica);
            }
            else
            {
                VwPessoasJuridicas oPessoaJuridica = oVwPessoasJuridicasService.oRepositoryVwPessoasJuridicas.SelecionarPK(id);
                return View("DetailsJuridica", oPessoaJuridica);
            }
        }
    }
}
