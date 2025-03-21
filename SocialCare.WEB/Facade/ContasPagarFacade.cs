using SocialCare.DATA.Models;
using SocialCare.DATA.Services;
using SocialCare.WEB.Models;

public class ContasPagarFacade
{
    private static readonly Lazy<ContasPagarFacade> instance = new Lazy<ContasPagarFacade>(() => new ContasPagarFacade());

    private readonly ContasPagarService oContasPagarService;
    private readonly PessoasFacade oPessoasFacade;

    private ContasPagarFacade()
    {
        oContasPagarService = new ContasPagarService();
        oPessoasFacade = PessoasFacade.Instance;
    }

    public static ContasPagarFacade Instance => instance.Value;

    public List<ContasPagarViewModel> ObterTodasContasPagar()
    {
        var contasPagar = oContasPagarService.oRepositoryContasPagar.SelecionarTodos();
        return contasPagar.Select(cp => new ContasPagarViewModel
        {
            Id = cp.Id,
            IdPessoa = cp.IdPessoa,
            NomePessoa = oPessoasFacade.ObterPessoaPorId(cp.IdPessoa).Nome,
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
            NomePessoa = oPessoasFacade.ObterPessoaPorId(contaPagar.IdPessoa).Nome,
            Data = contaPagar.Data,
            Valor = contaPagar.Valor,
            DataVencimento = contaPagar.DataVencimento,
            DataPagamento = contaPagar.DataPagamento
        };
    }

    public ContasPagarViewModel ObterContaPagarPorCompraId(int id)
    {
        var contaPagar = oContasPagarService.oRepositoryContasPagar.SelecionarPorCompraId(id);

        return new ContasPagarViewModel
        {
            Id = contaPagar.Id,
            IdPessoa = contaPagar.IdPessoa,
            NomePessoa = oPessoasFacade.ObterPessoaPorId(contaPagar.IdPessoa).Nome,
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
            IdCompra = model.IdCompra,
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
            IdCompra = model.IdCompra,
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
        return oPessoasFacade.ObterTodasPessoas();
    }
}