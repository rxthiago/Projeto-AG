using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Infrastructure.DataAccess;
using Teste.Infrastructure.Interface;
using Teste.Models.Models;

namespace Teste.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly MeuDbContext _context;

        public ProdutoRepository(MeuDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _context.Produtos.Include(p => p.Fornecedor).AsNoTracking().ToListAsync();
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            return await _context.Produtos.Include(p => p.Fornecedor).AsNoTracking().FirstOrDefaultAsync(p => p.ProdutoId == id);
        }

        public async Task AddAsync(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                // Aqui fazemos a exclusão lógica
                produto.Situacao = false;
                _context.Produtos.Update(produto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
