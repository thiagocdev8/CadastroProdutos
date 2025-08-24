using System;
using CadastroProdutos.Entity;

namespace CadastroProdutos.Services;

public interface IProdutosServices
{
    List<Produtos> GetAll();
    Produtos? GetById(int id);
    void Create(Produtos produto);
    Produtos? updateProduto(int id, Produtos produtoAtualizado);
    bool Delete(int id);
}
