﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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
        PessoasJuridicasDAO dao = new PessoasJuridicasDAO();
        dao.Incluir(this, _dbConnection);
    }

    public void Alterar(DBConnection _dbConnection)
    {
        PessoasJuridicasDAO dao = new PessoasJuridicasDAO();
        dao.Alterar(this, _dbConnection);
    }

    public void Excluir(DBConnection _dbConnection)
    {
        PessoasJuridicasDAO dao = new PessoasJuridicasDAO();
        dao.Excluir(this.Id, _dbConnection);
    }
}