using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Models.Models;

namespace Teste.Infrastructure.Interface
{
    public interface IProdutoService
    {
        Task<Produto> GetProdutoByIdAsync(int id);
        Task<Produto> AddProdutoAsync(Produto produto);
        Task UpdateProdutoAsync(Produto produto);
        Task DeleteProdutoAsync(int id);
    }
}
