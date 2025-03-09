using System;

namespace SocialCare.WEB.Models
{
    public class ContasPagarViewModel
    {
        public int Id { get; set; }
        public int IdPessoa { get; set; }
        public DateTime Data { get; set; }
        public string? NomePessoa { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
    }
}