using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Models.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string Descricao { get; set; }
        public bool Situacao { get; set; } = true; // Ativo por padrão
        public DateTime? DataFabricacao { get; set; }
        public DateTime? DataValidade { get; set; }
        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }

        public bool ValidarDatas()
        {
            if (!DataFabricacao.HasValue || !DataValidade.HasValue) return true;
            return DataFabricacao <= DataValidade;
        }
    }
}
