using BiblioTec.Models.Livro;
using Microsoft.EntityFrameworkCore;

namespace BiblioTec.Data;

public class TabelaLivroContext : DbContext
{
    public TabelaLivroContext(DbContextOptions<TabelaLivroContext> opts)
        : base(opts)
    {
    }
    public DbSet<Livro> livro { get; set; } = null!;
}