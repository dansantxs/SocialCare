using SocialCare.DATA.Models;
using SocialCare.WEB.Models;

public class ContasPagarControl : IDisposable
{
    private static readonly Lazy<ContasPagarControl> instance = new Lazy<ContasPagarControl>(() => new ContasPagarControl());

    private DBConnection _dbConnection;

    private ContasPagarControl()
    {
        _dbConnection = new DBConnection();
    }

    public static ContasPagarControl Instance => instance.Value;

    public List<ContasPagarViewModel> ObterTodasContasPagar()
    {
        ContasPagar contasPagar = new ContasPagar();
        List<ContasPagar> listaContasPagar = contasPagar.SelecionarTodos(_dbConnection);

        return listaContasPagar.Select(cp => new ContasPagarViewModel
        {
            Id = cp.Id,
            IdPessoa = cp.IdPessoa,
            IdCompra = cp.IdCompra,
            NomePessoa = PessoasControl.Instance.ObterPessoaPorId(cp.IdPessoa)?.Nome,
            Data = cp.Data,
            Valor = cp.Valor,
            DataVencimento = cp.DataVencimento,
            DataPagamento = cp.DataPagamento
        }).ToList();
    }

    public ContasPagarViewModel ObterContaPagarPorId(int id)
    {
        ContasPagar contaPagar = new ContasPagar().SelecionarPorId(id, _dbConnection);
        if (contaPagar == null) return null;

        return new ContasPagarViewModel
        {
            Id = contaPagar.Id,
            IdPessoa = contaPagar.IdPessoa,
            IdCompra = contaPagar.IdCompra,
            NomePessoa = PessoasControl.Instance.ObterPessoaPorId(contaPagar.IdPessoa)?.Nome,
            Data = contaPagar.Data,
            Valor = contaPagar.Valor,
            DataVencimento = contaPagar.DataVencimento,
            DataPagamento = contaPagar.DataPagamento
        };
    }

    public ContasPagarViewModel ObterContaPagarPorCompraId(int id)
    {
        ContasPagar contaPagar = new ContasPagar().SelecionarPorIdCompra(id, _dbConnection);
        if (contaPagar == null) return null;

        return new ContasPagarViewModel
        {
            Id = contaPagar.Id,
            IdPessoa = contaPagar.IdPessoa,
            IdCompra = contaPagar.IdCompra,
            NomePessoa = PessoasControl.Instance.ObterPessoaPorId(contaPagar.IdPessoa)?.Nome,
            Data = contaPagar.Data,
            Valor = contaPagar.Valor,
            DataVencimento = contaPagar.DataVencimento,
            DataPagamento = contaPagar.DataPagamento
        };
    }

    public void CriarContaPagar(ContasPagarViewModel model)
    {
        try
        {
            _dbConnection.BeginTransaction();

            var contaPagar = new ContasPagar
            {
                IdPessoa = model.IdPessoa,
                IdCompra = model.IdCompra,
                Data = model.Data,
                Valor = model.Valor,
                DataVencimento = model.DataVencimento,
                DataPagamento = model.DataPagamento
            };

            contaPagar.Incluir(_dbConnection);

            _dbConnection.Commit();
        }
        catch
        {
            _dbConnection.Rollback();
            throw;
        }
    }

    public void EditarContaPagar(ContasPagarViewModel model)
    {
        try
        {
            _dbConnection.BeginTransaction();

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

            contaPagar.Alterar(_dbConnection);

            _dbConnection.Commit();
        }
        catch
        {
            _dbConnection.Rollback();
            throw;
        }
    }

    public void ExcluirContaPagar(int id)
    {
        try
        {
            _dbConnection.BeginTransaction();

            var contaPagar = new ContasPagar { Id = id };
            contaPagar.Excluir(_dbConnection);

            _dbConnection.Commit();
        }
        catch
        {
            _dbConnection.Rollback();
            throw;
        }
    }

    public List<Pessoas> ObterPessoas()
    {
        Pessoas pessoas = new Pessoas();
        return pessoas.SelecionarTodos(_dbConnection);
    }

    public void Dispose()
    {
        _dbConnection.Dispose();
    }
}