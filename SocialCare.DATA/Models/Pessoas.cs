using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialCare.DATA.Models;

public class Pessoas
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("nome")]
    [StringLength(255)]
    public string Nome { get; set; }

    [Required]
    [Column("cidade")]
    [StringLength(100)]
    public string Cidade { get; set; }

    [Required]
    [Column("bairro")]
    [StringLength(100)]
    public string Bairro { get; set; }

    [Required]
    [Column("endereco")]
    [StringLength(255)]
    public string Endereco { get; set; }

    [Required]
    [Column("numero")]
    [StringLength(10)]
    public string Numero { get; set; }

    [Column("email")]
    [StringLength(255)]
    public string? Email { get; set; }

    [Column("telefone")]
    [StringLength(20)]
    public string? Telefone { get; set; }

    [Required]
    [Column("tipo")]
    [StringLength(1)]
    public string Tipo { get; set; }

    [InverseProperty("IdPessoaNavigation")]
    public virtual ICollection<Compras> Compras { get; set; } = new List<Compras>();

    [InverseProperty("IdPessoaNavigation")]
    public virtual ICollection<ContasPagar> ContasPagar { get; set; } = new List<ContasPagar>();

    [InverseProperty("IdNavigation")]
    public virtual PessoasFisicas PessoasFisicas { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual PessoasJuridicas PessoasJuridicas { get; set; }

    public List<Pessoas> SelecionarTodos(DBConnection _dbConnection)
    {
        PessoasDAO dao = new PessoasDAO();
        return dao.SelecionarTodos(_dbConnection);
    }

    public Pessoas SelecionarPorId(int id, DBConnection _dbConnection)
    {
        PessoasDAO dao = new PessoasDAO();
        return dao.SelecionarPorId(id, _dbConnection);
    }

    public void Incluir(DBConnection _dbConnection)
    {
        ValidarDados();

        PessoasDAO dao = new PessoasDAO();
        dao.Incluir(this, _dbConnection);
    }

    public void Alterar(DBConnection _dbConnection)
    {
        ValidarDados();

        PessoasDAO dao = new PessoasDAO();
        dao.Alterar(this, _dbConnection);
    }

    public void Excluir(DBConnection _dbConnection)
    {
        PessoasDAO dao = new PessoasDAO();
        dao.Excluir(this.Id, _dbConnection);
    }

    private void ValidarDados()
    {
        if (string.IsNullOrEmpty(Nome))
        {
            throw new ValidationException("Nome não informado.");
        }

        if (string.IsNullOrEmpty(Cidade))
        {
            throw new ValidationException("Cidade não informada.");
        }

        if (string.IsNullOrEmpty(Bairro))
        {
            throw new ValidationException("Bairro não informado.");
        }

        if (string.IsNullOrEmpty(Endereco))
        {
            throw new ValidationException("Endereço não informado.");
        }

        if (string.IsNullOrEmpty(Numero))
        {
            throw new ValidationException("Número não informado.");
        }

        if (string.IsNullOrEmpty(Tipo))
        {
            throw new ValidationException("Tipo não informado.");
        }

        if (Tipo != "F" && Tipo != "J")
        {
            throw new ValidationException("Tipo deve ser 'F' para pessoa física ou 'J' para pessoa jurídica.");
        }

        if (!string.IsNullOrEmpty(Email) && !ValidarEmail(Email))
        {
            throw new ValidationException("Email inválido.");
        }

        if (!string.IsNullOrEmpty(Telefone) && !ValidarTelefone(Telefone))
        {
            throw new ValidationException("Telefone inválido.");
        }
    }

    private bool ValidarEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private bool ValidarTelefone(string telefone)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(telefone, @"^\(?[1-9]{2}\)? ?(?:[2-8]|9[1-9])[0-9]{3}\-?[0-9]{4}$");
    }
}
