using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CadastroProdutos.Entity;

public class Produtos
{
    
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int Estoque { get; set; }

    
}

