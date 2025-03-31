using SocialCare.DATA.Models;

public class ContasPagarControl : IDisposable
{
    private static readonly Lazy<ContasPagarControl> instance = new Lazy<ContasPagarControl>(() => new ContasPagarControl());

    private DBConnection _dbConnection;

    private ContasPagarControl()
    {
        _dbConnection = new DBConnection();
    }

    public static ContasPagarControl Instance => instance.Value;

    public List<ContasPagar> ObterTodasContasPagar()
    {
        ContasPagar contasPagar = new ContasPagar();
        List<ContasPagar> listaContasPagar = contasPagar.SelecionarTodos(_dbConnection);

        foreach (var contaPagar in listaContasPagar)
        {
            var pessoa = new Pessoas().SelecionarPorId(contaPagar.IdPessoa, _dbConnection);
            contaPagar.Pessoa = pessoa;
        }

        return listaContasPagar;
    }

    public ContasPagar ObterContaPagarPorId(int id)
    {
        ContasPagar contaPagar = new ContasPagar().SelecionarPorId(id, _dbConnection);

        var pessoa = new Pessoas().SelecionarPorId(contaPagar.IdPessoa, _dbConnection);
        contaPagar.Pessoa = pessoa;

        return contaPagar;
    }

    public ContasPagar ObterContaPagarPorCompraId(int id)
    {
        ContasPagar contaPagar = new ContasPagar().SelecionarPorIdCompra(id, _dbConnection);
        return contaPagar;
    }

    public void CriarContaPagar(ContasPagar contaPagar)
    {
        try
        {
            _dbConnection.BeginTransaction();
            contaPagar.Incluir(_dbConnection);
            _dbConnection.Commit();
        }
        catch
        {
            _dbConnection.Rollback();
            throw;
        }
    }

    public void EditarContaPagar(ContasPagar contaPagar)
    {
        try
        {
            _dbConnection.BeginTransaction();
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