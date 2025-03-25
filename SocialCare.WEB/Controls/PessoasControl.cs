using SocialCare.DATA.Models;
using SocialCare.WEB.Models;

public class PessoasControl
{
    private static readonly Lazy<PessoasControl> instance = new Lazy<PessoasControl>(() => new PessoasControl());
    private PessoasDAO oPessoasDAO { get; set; }

    private PessoasControl()
    {
        oPessoasDAO = new PessoasDAO();
    }

    public static PessoasControl Instance => instance.Value;

    public List<Pessoas> ObterTodasPessoas()
    {
        return oPessoasDAO.SelecionarTodos();
    }

    public Pessoas ObterPessoaPorId(int id)
    {
        return oPessoasDAO.SelecionarPorId(id);
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
            oPessoasDAO.Incluir(pessoa, pessoaFisica);
        }
        else if (model.Tipo == "J")
        {
            var pessoaJuridica = new PessoasJuridicas
            {
                Cnpj = model.Cnpj,
                RazaoSocial = model.RazaoSocial,
                IdNavigation = pessoa
            };
            oPessoasDAO.Incluir(pessoa, pessoaJuridica);
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
            var pessoaFisica = new PessoasFisicas
            {
                IdNavigation = pessoa,
                Cpf = model.Cpf,
                DataNascimento = model.DataNascimento.Value
            };
            oPessoasDAO.Alterar(pessoa, pessoaFisica);
        }
        else if (model.Tipo == "J")
        {
            var pessoaJuridica = new PessoasJuridicas
            {
                IdNavigation = pessoa,
                Cnpj = model.Cnpj,
                RazaoSocial = model.RazaoSocial
            };
            oPessoasDAO.Alterar(pessoa, pessoaJuridica);
        }
    }

    public void ExcluirPessoa(int id)
    {
        oPessoasDAO.Excluir(id);
    }
}