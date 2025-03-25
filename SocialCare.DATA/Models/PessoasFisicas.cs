using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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
}