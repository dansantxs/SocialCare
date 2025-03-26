using SocialCare.DATA.Models;
using SocialCare.WEB.Models;

public class ContasPagarControl
{
    private static readonly Lazy<ContasPagarControl> instance = new Lazy<ContasPagarControl>(() => new ContasPagarControl());

    private ContasPagarDAO oContasPagarDAO { get; set; }
    private PessoasDAO oPessoasDAO { get; set; }

    private ContasPagarControl()
    {
        oContasPagarDAO = new ContasPagarDAO();
        oPessoasDAO = new PessoasDAO();
    }

    public static ContasPagarControl Instance => instance.Value;

    public List<ContasPagarViewModel> ObterTodasContasPagar()
    {
        var contasPagar = oContasPagarDAO.SelecionarTodos();
        return contasPagar.Select(cp => new ContasPagarViewModel
        {
            Id = cp.Id,
            IdPessoa = cp.IdPessoa,
            NomePessoa = oPessoasDAO.SelecionarPorId(cp.IdPessoa)?.Nome ?? "Desconhecido",
            Data = cp.Data,
            Valor = cp.Valor,
            DataVencimento = cp.DataVencimento,
            DataPagamento = cp.DataPagamento
        }).ToList();
    }

    public ContasPagarViewModel ObterContaPagarPorId(int id)
    {
        var contaPagar = oContasPagarDAO.SelecionarPorId(id);
        if (contaPagar == null) return null;

        return new ContasPagarViewModel
        {
            Id = contaPagar.Id,
            IdPessoa = contaPagar.IdPessoa,
            NomePessoa = oPessoasDAO.SelecionarPorId(contaPagar.IdPessoa)?.Nome ?? "Desconhecido",
            Data = contaPagar.Data,
            Valor = contaPagar.Valor,
            DataVencimento = contaPagar.DataVencimento,
            DataPagamento = contaPagar.DataPagamento
        };
    }

    public ContasPagarViewModel ObterContaPagarPorCompraId(int id)
    {
        var contaPagar = oContasPagarDAO.SelecionarPorIdCompra(id);
        if (contaPagar == null) return null;

        return new ContasPagarViewModel
        {
            Id = contaPagar.Id,
            IdPessoa = contaPagar.IdPessoa,
            NomePessoa = oPessoasDAO.SelecionarPorId(contaPagar.IdPessoa)?.Nome ?? "Desconhecido",
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

        oContasPagarDAO.Incluir(contaPagar);
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

        oContasPagarDAO.Alterar(contaPagar);
    }

    public void ExcluirContaPagar(int id)
    {
        oContasPagarDAO.Excluir(id);
    }

    public List<Pessoas> ObterPessoas()
    {
        return oPessoasDAO.SelecionarTodos();
    }
}