using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SocialCare.DATA.Models;

public partial class Compras
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idPessoa")]
    public int IdPessoa { get; set; }

    [Column("dataCompra", TypeName = "datetime")]
    public DateTime DataCompra { get; set; }

    [Column("total", TypeName = "decimal(10, 2)")]
    public decimal Total { get; set; }

    [ForeignKey("IdPessoa")]
    [InverseProperty("Compras")]
    public virtual Pessoas IdPessoaNavigation { get; set; }

    [InverseProperty("IdCompraNavigation")]
    public virtual ICollection<ItensCompra> ItensCompra { get; set; } = new List<ItensCompra>();
}