using SocialCare.DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmprestimoLivros.WEB.Models
{
    public class ComprasViewModel
    {
        public DateTime DataCompra { get; set; }
        public List<ItensCompraViewModel> Itens { get; set; } = new List<ItensCompraViewModel>();
    }
}