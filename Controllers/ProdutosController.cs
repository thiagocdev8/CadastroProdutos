using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CadastroProdutos.Entity;

namespace CadastroProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private List<Produtos> produtos = new List<Produtos>
        {
            new Produtos { Id = 1, Nome = "Produto A", Preco = 10.0m, Estoque = 100 },
            new Produtos { Id = 2, Nome = "Produto B", Preco = 20.0m, Estoque = 200 }
        };

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
            return Ok(novoProduto);
        }
    } 
}
