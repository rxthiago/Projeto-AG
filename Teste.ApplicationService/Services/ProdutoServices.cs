using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Infrastructure.Interface;
using Teste.Models.Models;

namespace Teste.ApplicationService.Service
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<bool> AddProdutoAsync(Produto produto)
        {
            if (!produto.ValidarDatas()) return false;

            await _produtoRepository.AddAsync(produto);
            return true;
        }

        // Outros métodos de serviço aqui...
    }
}
