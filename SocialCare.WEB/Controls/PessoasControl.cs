using SocialCare.DATA.Models;
using SocialCare.WEB.Models;
using System;
using System.Collections.Generic;

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

    public void CriarPessoa(PessoasViewModel model)
    {
        try
        {
            _dbConnection.BeginTransaction();

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

            pessoa.Incluir(_dbConnection);

            if (model.Tipo == "F")
            {
                var pessoaFisica = new PessoasFisicas
                {
                    Id = pessoa.Id,
                    Cpf = model.Cpf,
                    DataNascimento = model.DataNascimento.Value
                };
                pessoaFisica.Incluir(_dbConnection);
            }
            else if (model.Tipo == "J")
            {
                var pessoaJuridica = new PessoasJuridicas
                {
                    Id = pessoa.Id,
                    Cnpj = model.Cnpj,
                    RazaoSocial = model.RazaoSocial
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

    public void EditarPessoa(PessoasViewModel model)
    {
        try
        {
            _dbConnection.BeginTransaction();

            var pessoa = ObterPessoaPorId(model.Id);

            pessoa.Nome = model.Nome;
            pessoa.Cidade = model.Cidade;
            pessoa.Bairro = model.Bairro;
            pessoa.Endereco = model.Endereco;
            pessoa.Numero = model.Numero;
            pessoa.Email = model.Email;
            pessoa.Telefone = model.Telefone;
            pessoa.Tipo = model.Tipo;

            pessoa.Alterar(_dbConnection);

            if (model.Tipo == "F")
            {
                var pessoaFisica = new PessoasFisicas().SelecionarPorId(model.Id, _dbConnection);
                if (pessoaFisica == null)
                {
                    pessoaFisica = new PessoasFisicas();
                    pessoaFisica.Id = model.Id;
                }

                pessoaFisica.Cpf = model.Cpf;
                pessoaFisica.DataNascimento = model.DataNascimento.Value;

                if (pessoaFisica.Id == 0)
                {
                    pessoaFisica.Id = model.Id;
                    pessoaFisica.Incluir(_dbConnection);
                }
                else
                {
                    pessoaFisica.Alterar(_dbConnection);
                }

                var pessoaJuridica = new PessoasJuridicas().SelecionarPorId(model.Id, _dbConnection);
                if (pessoaJuridica != null)
                {
                    pessoaJuridica.Excluir(_dbConnection);
                }
            }
            else if (model.Tipo == "J")
            {
                var pessoaJuridica = new PessoasJuridicas().SelecionarPorId(model.Id, _dbConnection);
                if (pessoaJuridica == null)
                {
                    pessoaJuridica = new PessoasJuridicas();
                    pessoaJuridica.Id = model.Id;
                }

                pessoaJuridica.Cnpj = model.Cnpj;
                pessoaJuridica.RazaoSocial = model.RazaoSocial;

                if (pessoaJuridica.Id == 0)
                {
                    pessoaJuridica.Id = model.Id;
                    pessoaJuridica.Incluir(_dbConnection);
                }
                else
                {
                    pessoaJuridica.Alterar(_dbConnection);
                }

                var pessoaFisica = new PessoasFisicas().SelecionarPorId(model.Id, _dbConnection);
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