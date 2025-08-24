using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CadastroProdutos.Entity;

public class Produtos
{
    
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome do produto não pode exceder 100 caracteres.")]
    public required string Nome { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
    public decimal Preco { get; set; }

    [Range(0, 1000, ErrorMessage = "O estoque precisa estar entre 0 e 1000.")]
    public int Estoque { get; set; }

    
}

