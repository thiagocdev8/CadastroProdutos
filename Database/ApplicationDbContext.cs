using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using CadastroProdutos.Entity;

namespace CadastroProdutos.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Produtos> Produtos { get; set; }

}
