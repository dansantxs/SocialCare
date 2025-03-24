using SocialCare.DATA.Models;
using SocialCare.WEB.Models;

public class PessoasControl
{
    private static readonly Lazy<PessoasControl> instance = new Lazy<PessoasControl>(() => new PessoasControl());
    private PessoasDAO oPessoasDAO { get; set; }
    private PessoasFisicasDAO oPessoasFisicasDAO { get; set; }
    private PessoasJuridicasDAO oPessoasJuridicasDAO { get; set; }

    private PessoasControl()
    {
        oPessoasDAO = new PessoasDAO();
        oPessoasFisicasDAO = new PessoasFisicasDAO();
        oPessoasJuridicasDAO = new PessoasJuridicasDAO();
    }

    public static PessoasControl Instance => instance.Value;

    public List<Pessoas> ObterTodasPessoas()
    {
        var pessoas = oPessoasDAO.SelecionarTodos();

        foreach (var pessoa in pessoas)
        {
            if (pessoa.Tipo == "F")
            {
                pessoa.PessoasFisicas = oPessoasFisicasDAO.SelecionarPorId(pessoa.Id);
            }
            else if (pessoa.Tipo == "J")
            {
                pessoa.PessoasJuridicas = oPessoasJuridicasDAO.SelecionarPorId(pessoa.Id);
            }
        }

        return pessoas;
    }

    public Pessoas ObterPessoaPorId(int id)
    {
        var pessoa = oPessoasDAO.SelecionarPorId(id);
        if (pessoa.Tipo == "F")
        {
            pessoa.PessoasFisicas = oPessoasFisicasDAO.SelecionarPorId(pessoa.Id);
        }
        else if (pessoa.Tipo == "J")
        {
            pessoa.PessoasJuridicas = oPessoasJuridicasDAO.SelecionarPorId(pessoa.Id);
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
            oPessoasFisicasDAO.Incluir(pessoaFisica);
        }
        else if (model.Tipo == "J")
        {
            var pessoaJuridica = new PessoasJuridicas
            {
                Cnpj = model.Cnpj,
                RazaoSocial = model.RazaoSocial,
                IdNavigation = pessoa
            };
            oPessoasJuridicasDAO.Incluir(pessoaJuridica);
        }
    }

    public void EditarPessoa(PessoasViewModel model)
    {
        var pessoa = oPessoasDAO.SelecionarPorId(model.Id);

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
            var pessoaFisica = oPessoasFisicasDAO.SelecionarPorId(model.Id);
            if (pessoaFisica != null)
            {
                pessoaFisica.Cpf = model.Cpf;
                pessoaFisica.DataNascimento = model.DataNascimento.Value;
                oPessoasFisicasDAO.Alterar(pessoaFisica);
            }
        }
        else if (model.Tipo == "J")
        {
            var pessoaJuridica = oPessoasJuridicasDAO.SelecionarPorId(model.Id);
            if (pessoaJuridica != null)
            {
                pessoaJuridica.Cnpj = model.Cnpj;
                pessoaJuridica.RazaoSocial = model.RazaoSocial;
                oPessoasJuridicasDAO.Alterar(pessoaJuridica);
            }
        }

        oPessoasDAO.Alterar(pessoa);
    }

    public void ExcluirPessoa(int id)
    {
        var pessoa = oPessoasDAO.SelecionarPorId(id);
        if (pessoa != null)
        {
            if (pessoa.Tipo == "F")
            {
                oPessoasFisicasDAO.Excluir(pessoa.Id);
            }
            else if (pessoa.Tipo == "J")
            {
                oPessoasJuridicasDAO.Excluir(pessoa.Id);
            }

            oPessoasDAO.Excluir(id);
        }
    }
}