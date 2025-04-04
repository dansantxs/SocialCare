﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SocialCare.DATA.Models;

public class Pessoas
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("nome")]
    [StringLength(255)]
    [Unicode(false)]
    public string Nome { get; set; }

    [Required]
    [Column("cidade")]
    [StringLength(100)]
    [Unicode(false)]
    public string Cidade { get; set; }

    [Required]
    [Column("bairro")]
    [StringLength(100)]
    [Unicode(false)]
    public string Bairro { get; set; }

    [Required]
    [Column("endereco")]
    [StringLength(255)]
    [Unicode(false)]
    public string Endereco { get; set; }

    [Required]
    [Column("numero")]
    [StringLength(10)]
    [Unicode(false)]
    public string Numero { get; set; }

    [Column("email")]
    [StringLength(255)]
    [Unicode(false)]
    public string Email { get; set; }

    [Column("telefone")]
    [StringLength(20)]
    [Unicode(false)]
    public string Telefone { get; set; }

    [Required]
    [Column("tipo")]
    [StringLength(1)]
    [Unicode(false)]
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
        PessoasDAO dao = new PessoasDAO();
        dao.Incluir(this, _dbConnection);
    }

    public void Alterar(DBConnection _dbConnection)
    {
        PessoasDAO dao = new PessoasDAO();
        dao.Alterar(this, _dbConnection);
    }

    public void Excluir(DBConnection _dbConnection)
    {
        PessoasDAO dao = new PessoasDAO();
        dao.Excluir(this.Id, _dbConnection);
    }
}