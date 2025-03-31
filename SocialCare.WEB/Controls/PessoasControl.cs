using SocialCare.DATA.Models;

public class PessoasControl : IDisposable
{
    private static readonly Lazy<PessoasControl> instance = new Lazy<PessoasControl>(() => new PessoasControl());

    private DBConnection _dbConnection;

    private PessoasControl()
    {
        _dbConnection = new DBConnection();
    }

    public static PessoasControl Instance => instance.Value;

    public List<Pessoas> ObterTodasPessoas()
    {
        Pessoas pessoas = new Pessoas();
        List<Pessoas> listaPessoas = pessoas.SelecionarTodos(_dbConnection);

        foreach (var pessoa in listaPessoas)
        {
            if (pessoa.Tipo == "F")
            {
                var pessoaFisica = new PessoasFisicas().SelecionarPorId(pessoa.Id, _dbConnection);
                if (pessoaFisica != null)
                {
                    pessoa.PessoasFisicas = pessoaFisica;
                }
            }
            else if (pessoa.Tipo == "J")
            {
                var pessoaJuridica = new PessoasJuridicas().SelecionarPorId(pessoa.Id, _dbConnection);
                if (pessoaJuridica != null)
                {
                    pessoa.PessoasJuridicas = pessoaJuridica;
                }
            }
        }

        return listaPessoas;
    }

    public Pessoas ObterPessoaPorId(int id)
    {
        Pessoas pessoa = new Pessoas().SelecionarPorId(id, _dbConnection);
        if (pessoa.Tipo == "F")
        {
            var pessoaFisica = new PessoasFisicas().SelecionarPorId(pessoa.Id, _dbConnection);
            if (pessoaFisica != null)
            {
                pessoa.PessoasFisicas = pessoaFisica;
            }
        }
        else if (pessoa.Tipo == "J")
        {
            var pessoaJuridica = new PessoasJuridicas().SelecionarPorId(pessoa.Id, _dbConnection);
            if (pessoaJuridica != null)
            {
                pessoa.PessoasJuridicas = pessoaJuridica;
            }
        }
        return pessoa;
    }

    public void CriarPessoa(Pessoas pessoa)
    {
        try
        {
            _dbConnection.BeginTransaction();

            pessoa.Incluir(_dbConnection);

            if (pessoa.Tipo == "F")
            {
                var pessoaFisica = new PessoasFisicas
                {
                    Id = pessoa.Id,
                    Cpf = pessoa.PessoasFisicas.Cpf,
                    DataNascimento = pessoa.PessoasFisicas.DataNascimento
                };
                pessoaFisica.Incluir(_dbConnection);
            }
            else if (pessoa.Tipo == "J")
            {
                var pessoaJuridica = new PessoasJuridicas
                {
                    Id = pessoa.Id,
                    Cnpj = pessoa.PessoasJuridicas.Cnpj,
                    RazaoSocial = pessoa.PessoasJuridicas.RazaoSocial
                };
                pessoaJuridica.Incluir(_dbConnection);
            }

            _dbConnection.Commit();
        }
        catch
        {
            _dbConnection.Rollback();
            throw;
        }
    }

    public void EditarPessoa(Pessoas pessoa)
    {
        try
        {
            _dbConnection.BeginTransaction();

            pessoa.Alterar(_dbConnection);

            if (pessoa.Tipo == "F")
            {
                var pessoaFisica = new PessoasFisicas().SelecionarPorId(pessoa.Id, _dbConnection);
                if (pessoaFisica == null)
                {
                    pessoaFisica = new PessoasFisicas();
                    pessoaFisica.Id = pessoa.Id;
                }

                pessoaFisica.Cpf = pessoa.PessoasFisicas.Cpf;
                pessoaFisica.DataNascimento = pessoa.PessoasFisicas.DataNascimento;

                if (pessoaFisica.Id == 0)
                {
                    pessoaFisica.Id = pessoa.Id;
                    pessoaFisica.Incluir(_dbConnection);
                }
                else
                {
                    pessoaFisica.Alterar(_dbConnection);
                }

                var pessoaJuridica = new PessoasJuridicas().SelecionarPorId(pessoa.Id, _dbConnection);
                if (pessoaJuridica != null)
                {
                    pessoaJuridica.Excluir(_dbConnection);
                }
            }
            else if (pessoa.Tipo == "J")
            {
                var pessoaJuridica = new PessoasJuridicas().SelecionarPorId(pessoa.Id, _dbConnection);
                if (pessoaJuridica == null)
                {
                    pessoaJuridica = new PessoasJuridicas();
                    pessoaJuridica.Id = pessoa.Id;
                }

                pessoaJuridica.Cnpj = pessoa.PessoasJuridicas.Cnpj;
                pessoaJuridica.RazaoSocial = pessoa.PessoasJuridicas.RazaoSocial;

                if (pessoaJuridica.Id == 0)
                {
                    pessoaJuridica.Id = pessoa.Id;
                    pessoaJuridica.Incluir(_dbConnection);
                }
                else
                {
                    pessoaJuridica.Alterar(_dbConnection);
                }

                var pessoaFisica = new PessoasFisicas().SelecionarPorId(pessoa.Id, _dbConnection);
                if (pessoaFisica != null)
                {
                    pessoaFisica.Excluir(_dbConnection);
                }
            }

            _dbConnection.Commit();
        }
        catch
        {
            _dbConnection.Rollback();
            throw;
        }
    }

    public void ExcluirPessoa(int id)
    {
        try
        {
            _dbConnection.BeginTransaction();

            var pessoaFisica = new PessoasFisicas().SelecionarPorId(id, _dbConnection);
            if (pessoaFisica != null)
            {
                pessoaFisica.Excluir(_dbConnection);
            }

            var pessoaJuridica = new PessoasJuridicas().SelecionarPorId(id, _dbConnection);
            if (pessoaJuridica != null)
            {
                pessoaJuridica.Excluir(_dbConnection);
            }

            var pessoa = ObterPessoaPorId(id);
            if (pessoa != null)
            {
                pessoa.Excluir(_dbConnection);
            }

            _dbConnection.Commit();
        }
        catch
        {
            _dbConnection.Rollback();
            throw;
        }
    }

    public void Dispose()
    {
        _dbConnection.Dispose();
    }
}