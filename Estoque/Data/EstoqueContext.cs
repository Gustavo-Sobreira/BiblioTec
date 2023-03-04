using Estoques.Models;
using Microsoft.EntityFrameworkCore;

namespace Estoques.Data;

public class EstoqueContext : DbContext
{
    public EstoqueContext(DbContextOptions<EstoqueContext> opts)
        : base(opts)
    {

    }

    public DbSet<ProdutoModel> Produto { get; set; }
}