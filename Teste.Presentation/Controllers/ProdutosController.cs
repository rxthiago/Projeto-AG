using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teste.ApplicationService.Service;
using Teste.Infrastructure.DataAccess;
using Teste.Models.Models;

namespace Teste.Presentation.Controllers;
/*[ApiController]
[Route("[controller]")]
public class ProdutosController : ControllerBase 
{
    private readonly MeuDbContext _context; 
    public ProdutosController(MeuDbContext context)
    {
        _context = context;
    }


    [HttpGet("{id}")]  // Recuperar um registro por código
    public async Task<ActionResult<Produto>> GetProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);

        if (produto == null)
        {
            return NotFound();
        }

        return produto;
    }


    [HttpGet]  // Listar Registros Filtrando a partir de Parâmetros e Paginando a Resposta
    public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos([FromQuery] string descricao = "", [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = _context.Produtos.AsQueryable();

        if (!string.IsNullOrEmpty(descricao))
        {
            query = query.Where(p => p.Descricao.Contains(descricao));
        }

        var produtos = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return produtos;
    }

    [HttpPost]
    public async Task<ActionResult<Produto>> PostProduto(Produto produto) // Inserir
    {
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProduto", new { id = produto.ProdutoId }, produto);
    }


    [HttpPost]
    public async Task<ActionResult<Produto>> ValidacaoProduto(Produto produto) //Criar Validação para o Campo Data de Fabricação
    {
        if (produto.DataFabricacao >= produto.DataValidade)
        {
            return BadRequest("Data de fabricação deve ser menor que a data de validade.");
        }

        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProduto", new { id = produto.ProdutoId }, produto);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduto(int id, Produto produto) //Editar
    {
        if (id != produto.ProdutoId)
        {
            return BadRequest();
        }

        _context.Entry(produto).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Produtos.Any(e => e.ProdutoId == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduto(int id, Produto produto) //Validação na edição
    {
        if (id != produto.ProdutoId)
        {
            return BadRequest();
        }

        if (produto.DataFabricacao >= produto.DataValidade)
        {
            return BadRequest("Data de fabricação deve ser menor que a data de validade.");
        }

        _context.Entry(produto).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Produtos.Any(e => e.ProdutoId == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduto(int id) //Deletar Produto
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
        {
            return NotFound();
        }

        // Marca o produto como inativo em vez de deletá-lo
        produto.Situacao = false;
        await _context.SaveChangesAsync();

        return NoContent();
    }
}*/






[ApiController]
[Route("[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly ProdutoService _produtoService;

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
    // outros endpoints...
}
