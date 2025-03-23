using SocialCare.DATA.Models;
using SocialCare.DATA.Repositories;
using SocialCare.WEB.Models;

public class ContasPagarControl
{
    private static readonly Lazy<ContasPagarControl> instance = new Lazy<ContasPagarControl>(() => new ContasPagarControl());

    private RepositoryContasPagar oRepositoryContasPagar { get; set; }
    private RepositoryPessoas oRepositoryPessoas { get; set; }

    private ContasPagarControl()
    {
        oRepositoryContasPagar = new RepositoryContasPagar("Data Source=DANIEL;Initial Catalog=SocialCare;Persist Security Info=True;User ID=sa;Password=1928;Encrypt=True;TrustServerCertificate=True");
        oRepositoryPessoas = new RepositoryPessoas("Data Source=DANIEL;Initial Catalog=SocialCare;Persist Security Info=True;User ID=sa;Password=1928;Encrypt=True;TrustServerCertificate=True");
    }

    public static ContasPagarControl Instance => instance.Value;

    public List<ContasPagarViewModel> ObterTodasContasPagar()
    {
        var contasPagar = oRepositoryContasPagar.SelecionarTodos();
        return contasPagar.Select(cp => new ContasPagarViewModel
        {
            Id = cp.Id,
            IdPessoa = cp.IdPessoa,
            NomePessoa = oRepositoryPessoas.SelecionarPorId(cp.IdPessoa).Nome,
            Data = cp.Data,
            Valor = cp.Valor,
            DataVencimento = cp.DataVencimento,
            DataPagamento = cp.DataPagamento
        }).ToList();
    }

    public ContasPagarViewModel ObterContaPagarPorId(int id)
    {
        var contaPagar = oRepositoryContasPagar.SelecionarPorId(id);

        return new ContasPagarViewModel
        {
            Id = contaPagar.Id,
            IdPessoa = contaPagar.IdPessoa,
            NomePessoa = oRepositoryPessoas.SelecionarPorId(contaPagar.IdPessoa).Nome,
            Data = contaPagar.Data,
            Valor = contaPagar.Valor,
            DataVencimento = contaPagar.DataVencimento,
            DataPagamento = contaPagar.DataPagamento
        };
    }

    public ContasPagarViewModel ObterContaPagarPorCompraId(int id)
    {
        var contaPagar = oRepositoryContasPagar.SelecionarPorIdCompra(id);

        return new ContasPagarViewModel
        {
            Id = contaPagar.Id,
            IdPessoa = contaPagar.IdPessoa,
            NomePessoa = oRepositoryPessoas.SelecionarPorId(contaPagar.IdPessoa).Nome,
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

        oRepositoryContasPagar.Incluir(contaPagar);
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

        oRepositoryContasPagar.Alterar(contaPagar);
    }

    public void ExcluirContaPagar(int id)
    {
        oRepositoryContasPagar.Excluir(id);
    }

    public List<Pessoas> ObterPessoas()
    {
        return oRepositoryPessoas.SelecionarTodos();
    }
}