using SocialCare.DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialCare.WEB.Models
{
    public class ComprasViewModel
    {
        public int Id { get; set; }
        public int IdPessoa { get; set; }
        public string? NomePessoa { get; set; }
        public DateTime DataCompra { get; set; }
        public decimal? Total { get; set; }
        public List<ItensCompraViewModel> Itens { get; set; } = new List<ItensCompraViewModel>();
    }
}