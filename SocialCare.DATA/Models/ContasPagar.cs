﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SocialCare.DATA.Models;

public partial class ContasPagar
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idPessoa")]
    public int IdPessoa { get; set; }

    [Column("idCompra")]
    public int? IdCompra { get; set; }

    [Column("data", TypeName = "datetime")]
    public DateTime Data { get; set; }

    [Column("valor", TypeName = "decimal(10, 2)")]
    public decimal Valor { get; set; }

    [Column("dataVencimento", TypeName = "datetime")]
    public DateTime DataVencimento { get; set; }

    [Column("dataPagamento", TypeName = "datetime")]
    public DateTime? DataPagamento { get; set; }

    [ForeignKey("IdPessoa")]
    [InverseProperty("ContasPagar")]
    public virtual Pessoas IdPessoaNavigation { get; set; }
}