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
            try
            {
                return Ok(produtosService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("{id}")]
        public ActionResult<Produtos> GetById(int id)
        {
            try
            {
                var produto = produtosService.GetById(id);
                if (produto is null)
                {
                    return NotFound();
                }
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public ActionResult createProduto(Produtos produto)
        {
            try
            {
                produtosService.Create(produto);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public ActionResult updateProduto(int id, Produtos produtoAtualizado)
        {
            try
            {
                var produtoExistente = produtosService.updateProduto(id, produtoAtualizado);
                if (produtoExistente is null)
                {
                    return NotFound($"Produto com ID {id} não encontrado.");
                }

                return Ok(produtoAtualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("{id}")]
        public ActionResult deleteProduto(int id)
        {
            try
            {
                var deletou = produtosService.Delete(id);

                if (!deletou)
                {
                    return NotFound($"Produto com ID {id} não encontrado.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    } 
}
