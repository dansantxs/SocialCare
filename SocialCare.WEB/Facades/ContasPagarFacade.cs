using SocialCare.DATA.Models;
using SocialCare.DATA.Services;
using SocialCare.WEB.Models;

public class ContasPagarFacade
{
    private readonly ContasPagarService oContasPagarService;
    private readonly PessoasService oPessoasService;

    public ContasPagarFacade()
    {
        oContasPagarService = new ContasPagarService();
        oPessoasService = new PessoasService();
    }

    public List<ContasPagarViewModel> ObterTodasContasPagar()
    {
        var contasPagar = oContasPagarService.oRepositoryContasPagar.SelecionarTodos();
        return contasPagar.Select(cp => new ContasPagarViewModel
        {
            Id = cp.Id,
            IdPessoa = cp.IdPessoa,
            NomePessoa = oPessoasService.oRepositoryPessoas.SelecionarPK(cp.IdPessoa).Nome,
            Data = cp.Data,
            Valor = cp.Valor,
            DataVencimento = cp.DataVencimento,
            DataPagamento = cp.DataPagamento
        }).ToList();
    }

    public ContasPagarViewModel ObterContaPagarPorId(int id)
    {
        var contaPagar = oContasPagarService.oRepositoryContasPagar.SelecionarPK(id);

        return new ContasPagarViewModel
        {
            Id = contaPagar.Id,
            IdPessoa = contaPagar.IdPessoa,
            NomePessoa = oPessoasService.oRepositoryPessoas.SelecionarPK(contaPagar.IdPessoa).Nome,
            Data = contaPagar.Data,
            Valor = contaPagar.Valor,
            DataVencimento = contaPagar.DataVencimento,
            DataPagamento = contaPagar.DataPagamento
        };
    }

    public void CriarContaPagar(ContasPagarViewModel model)
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
    }

    public void EditarContaPagar(ContasPagarViewModel model)
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
    }

    public void ExcluirContaPagar(int id)
    {
        oContasPagarService.oRepositoryContasPagar.Excluir(id);
    }

    public List<Pessoas> ObterPessoas()
    {
        return oPessoasService.oRepositoryPessoas.SelecionarTodos();
    }
}