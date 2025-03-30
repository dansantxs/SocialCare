using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
        PessoasFisicasDAO dao = new PessoasFisicasDAO();
        dao.Incluir(this, _dbConnection);
    }

    public void Alterar(DBConnection _dbConnection)
    {
        PessoasFisicasDAO dao = new PessoasFisicasDAO();
        dao.Alterar(this, _dbConnection);
    }

    public void Excluir(DBConnection _dbConnection)
    {
        PessoasFisicasDAO dao = new PessoasFisicasDAO();
        dao.Excluir(this.Id, _dbConnection);
    }
}