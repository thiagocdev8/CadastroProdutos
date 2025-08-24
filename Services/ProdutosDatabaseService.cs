using System;
using CadastroProdutos.Database;
using CadastroProdutos.Entity;

namespace CadastroProdutos.Services;

public class ProdutosDatabaseService : IProdutosServices
{
    private readonly ApplicationDbContext banco;
    public ProdutosDatabaseService(ApplicationDbContext banco)
    {
        this.banco = banco;
    }
    
    public void Create(Produtos produto)
    {
        banco.Produtos.Add(produto);
        banco.SaveChanges();
    }

    public bool Delete(int id)
    {
        var produto = banco.Produtos.FirstOrDefault(p => p.Id == id);
        if (produto is null)
        {
            return false;
        }
        else
        {
            banco.Produtos.Remove(produto);
            banco.SaveChanges();
            return true;
        }
    }

    public List<Produtos> GetAll()
    {
        return banco.Produtos.ToList();
    }

    public Produtos? GetById(int id)
    {
        var produto = banco.Produtos.FirstOrDefault(p => p.Id == id);
        return produto;
    }

    public Produtos? updateProduto(int id, Produtos produtoAtualizado)
    {
        var produtoExistente = banco.Produtos.FirstOrDefault(p => p.Id == id);
        if (produtoExistente is null)
        {
            return null;
        }

        produtoExistente.Nome = produtoAtualizado.Nome;
        produtoExistente.Preco = produtoAtualizado.Preco;
        produtoExistente.Estoque = produtoAtualizado.Estoque;

        banco.SaveChanges();

        return produtoExistente;
    }
}
