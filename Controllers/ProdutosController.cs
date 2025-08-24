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

        //dependency injection
        private IProdutosServices produtosService;

        public ProdutosController(IProdutosServices produtosService)
        {
            this.produtosService = produtosService;
        }


        [HttpGet]
        public ActionResult<List<Produtos>> GetAll()
        {
            return Ok(produtosService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Produtos> GetById(int id)
        {
            var produto = produtosService.GetById(id);
            if (produto is null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        [HttpPost]
        public ActionResult createProduto(Produtos produto)
        {
            produtosService.Create(produto);
            return Created();
            
        }

        [HttpPut("{id}")]
        public ActionResult updateProduto(int id, Produtos produtoAtualizado)
        {
            var produtoExistente = produtosService.updateProduto(id, produtoAtualizado);
            if (produtoExistente is null)
            {
                return NotFound($"Produto com ID {id} não encontrado.");
            }

            return Ok(produtoAtualizado);
        }

        [HttpDelete("{id}")]
        public ActionResult deleteProduto(int id)
        {
            var deletou = produtosService.Delete(id);

            if (!deletou)
            {
                return NotFound($"Produto com ID {id} não encontrado.");
            }

            return Ok();
        }
    } 
}
