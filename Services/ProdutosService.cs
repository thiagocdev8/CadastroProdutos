using System;
using CadastroProdutos.Entity;


namespace CadastroProdutos.Services;

public class ProdutosService : IProdutosServices
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

    public Produtos? GetById(int id)
    {
        return produtos.FirstOrDefault(p => p.Id == id);
    }

    public void Create(Produtos produto)
    {
        var novoProduto = new Produtos
        {
            Id = produtos.Max(p => p.Id) + 1,
            Nome = produto.Nome,
            Preco = produto.Preco,
            Estoque = produto.Estoque
        };

        produtos.Add(novoProduto);
    }

    public Produtos? updateProduto(int id, Produtos produtoAtualizado)
    {
        var produtoExistente = produtos.FirstOrDefault(p => p.Id == id);
        if (produtoExistente is null)
        {
            return null;
        }

        produtoExistente.Nome = produtoAtualizado.Nome;
        produtoExistente.Preco = produtoAtualizado.Preco;
        produtoExistente.Estoque = produtoAtualizado.Estoque;

        return produtoExistente;
    }
    
    public bool Delete(int id)
    {
        var produto = produtos.FirstOrDefault(p => p.Id == id);
        if (produto is null)
        {
            return false;
        }

        produtos.Remove(produto);
        return true;
    }
}
