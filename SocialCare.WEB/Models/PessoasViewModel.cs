using SocialCare.DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialCare.WEB.Models
{
    public class PessoasViewModel
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Tipo { get; set; }

        public string? Cpf { get; set; }
        public DateOnly? DataNascimento { get; set; }

        public string? Cnpj { get; set; }
        public string? RazaoSocial { get; set; }
    }
}