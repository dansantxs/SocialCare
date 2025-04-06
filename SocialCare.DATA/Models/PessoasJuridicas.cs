using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace SocialCare.DATA.Models;

[Table("Pessoas_Juridicas")]
[Index("Cnpj", Name = "UQ__Pessoas___35BD3E48DAFA6CC0", IsUnique = true)]
public class PessoasJuridicas
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("cnpj")]
    [StringLength(14)]
    [Unicode(false)]
    public string Cnpj { get; set; }

    [Required]
    [Column("razao_social")]
    [StringLength(255)]
    [Unicode(false)]
    public string RazaoSocial { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("PessoasJuridicas")]
    public virtual Pessoas IdNavigation { get; set; }

    public PessoasJuridicas SelecionarPorId(int id, DBConnection _dbConnection)
    {
        PessoasJuridicasDAO dao = new PessoasJuridicasDAO();
        return dao.SelecionarPorId(id, _dbConnection);
    }

    public void Incluir(DBConnection _dbConnection)
    {
        if (!ValidarCnpj(Cnpj))
        {
            throw new ValidationException("CNPJ inválido.");
        }

        PessoasJuridicasDAO dao = new PessoasJuridicasDAO();
        dao.Incluir(this, _dbConnection);
    }

    public void Alterar(DBConnection _dbConnection)
    {
        if (!ValidarCnpj(Cnpj))
        {
            throw new ValidationException("CNPJ inválido.");
        }

        PessoasJuridicasDAO dao = new PessoasJuridicasDAO();
        dao.Alterar(this, _dbConnection);
    }

    public void Excluir(DBConnection _dbConnection)
    {
        PessoasJuridicasDAO dao = new PessoasJuridicasDAO();
        dao.Excluir(this.Id, _dbConnection);
    }

    private bool ValidarCnpj(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj) || cnpj.Length != 14 || !Regex.IsMatch(cnpj, @"^\d{14}$"))
        {
            return false;
        }

        int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCnpj = cnpj.Substring(0, 12);
        int soma = 0;

        for (int i = 0; i < 12; i++)
        {
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
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
        tempCnpj = tempCnpj + digito;
        soma = 0;

        for (int i = 0; i < 13; i++)
        {
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
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

        return cnpj.EndsWith(digito);
    }
}