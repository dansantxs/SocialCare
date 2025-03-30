using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;

namespace SocialCare.DATA.Models;

public class Produtos
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("nome")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nome { get; set; }

    [Column("preco", TypeName = "decimal(10, 2)")]
    public decimal Preco { get; set; }

    [Column("estoque")]
    public int Estoque { get; set; }

    [InverseProperty("IdProdutoNavigation")]
    public virtual ICollection<ItensCompra> ItensCompra { get; set; } = new List<ItensCompra>();

    public List<Produtos> SelecionarTodos(DBConnection _dbConnection)
    {
        ProdutosDAO dao = new ProdutosDAO();
        return dao.SelecionarTodos(_dbConnection);
    }

    public Produtos SelecionarPorId(int id, DBConnection _dbConnection)
    {
        ProdutosDAO dao = new ProdutosDAO();
        return dao.SelecionarPorId(id, _dbConnection);
    }

    public void Incluir(DBConnection _dbConnection)
    {
        ProdutosDAO dao = new ProdutosDAO();
        dao.Incluir(this, _dbConnection);
    }

    public void Alterar(DBConnection _dbConnection)
    {
        ProdutosDAO dao = new ProdutosDAO();
        dao.Alterar(this, _dbConnection);
    }

    public void Excluir(DBConnection _dbConnection)
    {
        ProdutosDAO dao = new ProdutosDAO();
        dao.Excluir(this.Id, _dbConnection);
    }
}