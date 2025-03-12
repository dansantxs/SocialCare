using SocialCare.DATA.Models;
using SocialCare.DATA.Services;
using SocialCare.WEB.Models;

public class PessoasFacade
{
    private readonly PessoasService oPessoasService;
    private readonly PessoasFisicasService oPessoasFisicasService;
    private readonly PessoasJuridicasService oPessoasJuridicasService;

    public PessoasFacade()
    {
        oPessoasService = new PessoasService();
        oPessoasFisicasService = new PessoasFisicasService();
        oPessoasJuridicasService = new PessoasJuridicasService();
    }

    public List<Pessoas> ObterTodasPessoas()
    {
        var pessoas = oPessoasService.oRepositoryPessoas.SelecionarTodos();

        foreach (var pessoa in pessoas)
        {
            if (pessoa.Tipo == "F")
            {
                pessoa.PessoasFisicas = oPessoasFisicasService.oRepositoryPessoasFisicas.SelecionarPK(pessoa.Id);
            }
            else if (pessoa.Tipo == "J")
            {
                pessoa.PessoasJuridicas = oPessoasJuridicasService.oRepositoryPessoasJuridicas.SelecionarPK(pessoa.Id);
            }
        }

        return pessoas;
    }

    public Pessoas ObterPessoaPorId(int id)
    {
        var pessoa = oPessoasService.oRepositoryPessoas.SelecionarPK(id);
        if (pessoa.Tipo == "F")
        {
            pessoa.PessoasFisicas = oPessoasFisicasService.oRepositoryPessoasFisicas.SelecionarPK(pessoa.Id);
        }
        else if (pessoa.Tipo == "J")
        {
            pessoa.PessoasJuridicas = oPessoasJuridicasService.oRepositoryPessoasJuridicas.SelecionarPK(pessoa.Id);
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
            oPessoasFisicasService.oRepositoryPessoasFisicas.Incluir(pessoaFisica);
        }
        else if (model.Tipo == "J")
        {
            var pessoaJuridica = new PessoasJuridicas
            {
                Cnpj = model.Cnpj,
                RazaoSocial = model.RazaoSocial,
                IdNavigation = pessoa
            };
            oPessoasJuridicasService.oRepositoryPessoasJuridicas.Incluir(pessoaJuridica);
        }
    }

    public void EditarPessoa(PessoasViewModel model)
    {
        var pessoa = oPessoasService.oRepositoryPessoas.SelecionarPK(model.Id);

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
            var pessoaFisica = oPessoasFisicasService.oRepositoryPessoasFisicas.SelecionarPK(model.Id);
            if (pessoaFisica != null)
            {
                pessoaFisica.Cpf = model.Cpf;
                pessoaFisica.DataNascimento = model.DataNascimento.Value;
                oPessoasFisicasService.oRepositoryPessoasFisicas.Alterar(pessoaFisica);
            }
        }
        else if (model.Tipo == "J")
        {
            var pessoaJuridica = oPessoasJuridicasService.oRepositoryPessoasJuridicas.SelecionarPK(model.Id);
            if (pessoaJuridica != null)
            {
                pessoaJuridica.Cnpj = model.Cnpj;
                pessoaJuridica.RazaoSocial = model.RazaoSocial;
                oPessoasJuridicasService.oRepositoryPessoasJuridicas.Alterar(pessoaJuridica);
            }
        }

        oPessoasService.oRepositoryPessoas.Alterar(pessoa);
    }

    public void ExcluirPessoa(int id)
    {
        var pessoa = oPessoasService.oRepositoryPessoas.SelecionarPK(id);
        if (pessoa != null)
        {
            if (pessoa.Tipo == "F")
            {
                oPessoasFisicasService.oRepositoryPessoasFisicas.Excluir(pessoa.Id);
            }
            else if (pessoa.Tipo == "J")
            {
                oPessoasJuridicasService.oRepositoryPessoasJuridicas.Excluir(pessoa.Id);
            }

            oPessoasService.oRepositoryPessoas.Excluir(id);
        }
    }
}