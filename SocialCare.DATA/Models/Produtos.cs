using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SocialCare.DATA.Models;

public partial class Produtos
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
}