using System;
using CadastroProdutos.Entity;


namespace CadastroProdutos.Services;

public class ProdutosService
{
    private static List<Produtos> produtos = new List<Produtos>
    {
        new Produtos { Id = 1, Nome = "Produto A", Preco = 10.0m, Estoque = 100 },
        new Produtos { Id = 2, Nome = "Produto B", Preco = 20.0m, Estoque = 200 }
    };
    

    public List<Produtos> GetAll()
    {
        return produtos;
    }
}
