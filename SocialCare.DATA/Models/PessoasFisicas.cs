using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace SocialCare.DATA.Models;

[Table("Pessoas_Fisicas")]
[Index("Cpf", Name = "UQ__Pessoas___D836E71F7887A65C", IsUnique = true)]
public class PessoasFisicas
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("cpf")]
    [StringLength(11)]
    [Unicode(false)]
    public string Cpf { get; set; }

    [Column("data_nascimento")]
    public DateOnly DataNascimento { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("PessoasFisicas")]
    public virtual Pessoas IdNavigation { get; set; }

    public PessoasFisicas SelecionarPorId(int id, DBConnection _dbConnection)
    {
        PessoasFisicasDAO dao = new PessoasFisicasDAO();
        return dao.SelecionarPorId(id, _dbConnection);
    }

    public void Incluir(DBConnection _dbConnection)
    {
        ValidarDados();

        PessoasFisicasDAO dao = new PessoasFisicasDAO();
        dao.Incluir(this, _dbConnection);
    }

    public void Alterar(DBConnection _dbConnection)
    {
        ValidarDados();

        PessoasFisicasDAO dao = new PessoasFisicasDAO();
        dao.Alterar(this, _dbConnection);
    }

    public void Excluir(DBConnection _dbConnection)
    {
        PessoasFisicasDAO dao = new PessoasFisicasDAO();
        dao.Excluir(this.Id, _dbConnection);
    }

    private bool ValidarCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11 || !Regex.IsMatch(cpf, @"^\d{11}$"))
        {
            return false;
        }

        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf.Substring(0, 9);
        int soma = 0;

        for (int i = 0; i < 9; i++)
        {
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
        }

        int resto = soma % 11;
        if (resto < 2)
        {
            resto = 0;
        }
        else
        {
            resto = 11 - resto;
        }

        string digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;

        for (int i = 0; i < 10; i++)
        {
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
        }

        resto = soma % 11;
        if (resto < 2)
        {
            resto = 0;
        }
        else
        {
            resto = 11 - resto;
        }

        digito = digito + resto.ToString();

        return cpf.EndsWith(digito);
    }

    private void ValidarDados()
    {
        if (string.IsNullOrEmpty(Cpf))
        {
            throw new ValidationException("CPF não informado.");
        }

        if (!ValidarCpf(Cpf))
        {
            throw new ValidationException("CPF inválido.");
        }

        if (DataNascimento == default)
        {
            throw new ValidationException("Data de nascimento não informada.");
        }

        var hoje = DateOnly.FromDateTime(DateTime.Today);
        var idadeMinima = 18;
        if (hoje.Year - DataNascimento.Year < idadeMinima ||
            (hoje.Year - DataNascimento.Year == idadeMinima &&
             (hoje.Month < DataNascimento.Month ||
              (hoje.Month == DataNascimento.Month && hoje.Day < DataNascimento.Day))))
        {
            throw new ValidationException("A pessoa deve ter pelo menos 18 anos.");
        }
    }
}
