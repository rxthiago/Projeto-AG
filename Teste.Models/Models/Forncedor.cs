using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Models.Models
{
    public class Fornecedor
    {
        public int FornecedorId { get; set; }
        public string Descricao { get; set; }
        public string CNPJ { get; set; }
        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
