using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teste.ApplicationService.Service;
using Teste.Infrastructure.DataAccess;
using Teste.Models.Models;

namespace Teste.Presentation.Controllers;
[ApiController]
[Route("[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly ProdutoService _produtoService;
    private readonly MeuDbContext _context;

    public ProdutosController(ProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpPost]
    public async Task<IActionResult> PostProduto([FromBody] Produto produto)
    {
        if (!produto.ValidarDatas())
        {
            return BadRequest("Data de fabricação deve ser menor ou igual à data de validade.");
        }

        var sucesso = await _produtoService.AddProdutoAsync(produto);
        if (!sucesso) return BadRequest("Falha ao adicionar produto.");

        return CreatedAtAction(nameof(PostProduto), new { id = produto.ProdutoId }, produto);
    }
    [HttpGet]
    public async Task<IActionResult> BuscarProduto([FromBody] Produto produto)
    {
        if (produto != null)
        {
            return Ok("Produto Disponível em Estoque");
        }
        else
        {
            return BadRequest(400);
        }
    }
    [HttpPost]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<Produto> AddProdutoAsync(Produto produto)
    {
        if (!produto.ValidarDatas())
        {
            throw new ArgumentException("Data de fabricação deve ser menor ou igual à data de validade.");
        }

        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
        return produto; // Retorna o produto adicionado, com ID preenchido após SaveChangesAsync()
    }
    [HttpPut]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task UpdateProdutoAsync(Produto produto)
    {
        var produtoExistente = await _context.Produtos.FindAsync(produto.ProdutoId);
        if (produtoExistente == null)
        {
            throw new ArgumentException("Produto não encontrado.");
        }

        if (!produto.ValidarDatas())
        {
            throw new ArgumentException("Data de fabricação deve ser menor ou igual à data de validade.");
        }

        // Atualiza propriedades conforme necessário
        _context.Entry(produtoExistente).CurrentValues.SetValues(produto);

        await _context.SaveChangesAsync();
    }
    [HttpDelete]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task DeleteProdutoAsync(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
        {
            throw new ArgumentException("Produto não encontrado.");
        }

        produto.Situacao = false;
        await _context.SaveChangesAsync();
    }
}

