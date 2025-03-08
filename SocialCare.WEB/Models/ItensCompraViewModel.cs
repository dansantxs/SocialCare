using SocialCare.DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmprestimoLivros.WEB.Models
{
    public class ItensCompraViewModel
    {
        public int IdProduto { get; set; }
        public string? NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}