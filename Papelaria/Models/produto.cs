using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Papelaria.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }

        public Produto() {     
            this.Nome = string.Empty;
            this.Quantidade = 0;
            this.Preco = 0;

        }
    }
}