using SocialCare.DATA.Models;
using SocialCare.DATA.Repositories;
using SocialCare.WEB.Models;

public class PessoasFacade
{
    private static readonly Lazy<PessoasFacade> instance = new Lazy<PessoasFacade>(() => new PessoasFacade());
    private RepositoryPessoas oRepositoryPessoas { get; set; }
    private RepositoryPessoasFisicas oRepositoryPessoasFisicas { get; set; }
    private RepositoryPessoasJuridicas oRepositoryPessoasJuridicas { get; set; }

    private PessoasFacade()
    {
        oRepositoryPessoas = new RepositoryPessoas();
        oRepositoryPessoasFisicas = new RepositoryPessoasFisicas();
        oRepositoryPessoasJuridicas = new RepositoryPessoasJuridicas();
    }

    public static PessoasFacade Instance => instance.Value;

    public List<Pessoas> ObterTodasPessoas()
    {
        var pessoas = oRepositoryPessoas.SelecionarTodos();

        foreach (var pessoa in pessoas)
        {
            if (pessoa.Tipo == "F")
            {
                pessoa.PessoasFisicas = oRepositoryPessoasFisicas.SelecionarPorId(pessoa.Id);
            }
            else if (pessoa.Tipo == "J")
            {
                pessoa.PessoasJuridicas = oRepositoryPessoasJuridicas.SelecionarPorId(pessoa.Id);
            }
        }

        return pessoas;
    }

    public Pessoas ObterPessoaPorId(int id)
    {
        var pessoa = oRepositoryPessoas.SelecionarPorId(id);
        if (pessoa.Tipo == "F")
        {
            pessoa.PessoasFisicas = oRepositoryPessoasFisicas.SelecionarPorId(pessoa.Id);
        }
        else if (pessoa.Tipo == "J")
        {
            pessoa.PessoasJuridicas = oRepositoryPessoasJuridicas.SelecionarPorId(pessoa.Id);
        }

        return pessoa;
    }

    public void CriarPessoa(PessoasViewModel model)
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
            oRepositoryPessoasFisicas.Incluir(pessoaFisica);
        }
        else if (model.Tipo == "J")
        {
            var pessoaJuridica = new PessoasJuridicas
            {
                Cnpj = model.Cnpj,
                RazaoSocial = model.RazaoSocial,
                IdNavigation = pessoa
            };
            oRepositoryPessoasJuridicas.Incluir(pessoaJuridica);
        }
    }

    public void EditarPessoa(PessoasViewModel model)
    {
        var pessoa = oRepositoryPessoas.SelecionarPorId(model.Id);

        pessoa.Nome = model.Nome;
        pessoa.Cidade = model.Cidade;
        pessoa.Bairro = model.Bairro;
        pessoa.Endereco = model.Endereco;
        pessoa.Numero = model.Numero;
        pessoa.Email = model.Email;
        pessoa.Telefone = model.Telefone;
        pessoa.Tipo = model.Tipo;

        if (model.Tipo == "F")
        {
            var pessoaFisica = oRepositoryPessoasFisicas.SelecionarPorId(model.Id);
            if (pessoaFisica != null)
            {
                pessoaFisica.Cpf = model.Cpf;
                pessoaFisica.DataNascimento = model.DataNascimento.Value;
                oRepositoryPessoasFisicas.Alterar(pessoaFisica);
            }
        }
        else if (model.Tipo == "J")
        {
            var pessoaJuridica = oRepositoryPessoasJuridicas.SelecionarPorId(model.Id);
            if (pessoaJuridica != null)
            {
                pessoaJuridica.Cnpj = model.Cnpj;
                pessoaJuridica.RazaoSocial = model.RazaoSocial;
                oRepositoryPessoasJuridicas.Alterar(pessoaJuridica);
            }
        }

        oRepositoryPessoas.Alterar(pessoa);
    }

    public void ExcluirPessoa(int id)
    {
        var pessoa = oRepositoryPessoas.SelecionarPorId(id);
        if (pessoa != null)
        {
            if (pessoa.Tipo == "F")
            {
                oRepositoryPessoasFisicas.Excluir(pessoa.Id);
            }
            else if (pessoa.Tipo == "J")
            {
                oRepositoryPessoasJuridicas.Excluir(pessoa.Id);
            }

            oRepositoryPessoas.Excluir(id);
        }
    }
}