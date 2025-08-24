using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CadastroProdutos.Entity;
using CadastroProdutos.Services;

namespace CadastroProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
       

        [HttpGet]
        public ActionResult<List<Produtos>> GetAll()
        {
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public ActionResult<Produtos> GetById(int id)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);
            if (produto is null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        [HttpPost]
        public ActionResult createProduto(Produtos produto)
        {
            var novoProduto = new Produtos
            {
                Id = produtos.Max(p => p.Id) + 1,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Estoque = produto.Estoque
            };

            produtos.Add(novoProduto);
            return Created($"/produtos/{novoProduto.Id}", novoProduto);
        }

        [HttpPut("{id}")]
        public ActionResult updateProduto(int id, Produtos produtoAtualizado)
        {
            var produtoExistente = produtos.FirstOrDefault(p => p.Id == id);
            if (produtoExistente is null)
            {
                return NotFound($"Produto com ID {id} não encontrado.");
            }

            produtoExistente.Nome = produtoAtualizado.Nome;
            produtoExistente.Preco = produtoAtualizado.Preco;
            produtoExistente.Estoque = produtoAtualizado.Estoque;

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult deleteProduto(int id)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);
            if (produto is null)
            {
                return NotFound($"Produto com ID {id} não encontrado.");
            }

            produtos.Remove(produto);
            return Ok();
        }
    } 
}
