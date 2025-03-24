using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SocialCare.DATA.Models;

public partial class ItensCompra
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idCompra")]
    public int IdCompra { get; set; }

    [Column("idProduto")]
    public int IdProduto { get; set; }

    [Column("quantidade")]
    public int Quantidade { get; set; }

    [Column("precoUnitario", TypeName = "decimal(10, 2)")]
    public decimal PrecoUnitario { get; set; }

    [Column("subtotal", TypeName = "decimal(21, 2)")]
    public decimal? Subtotal { get; set; }

    [ForeignKey("IdCompra")]
    [InverseProperty("ItensCompra")]
    public virtual Compras IdCompraNavigation { get; set; }

    [ForeignKey("IdProduto")]
    [InverseProperty("ItensCompra")]
    public virtual Produtos IdProdutoNavigation { get; set; }
}