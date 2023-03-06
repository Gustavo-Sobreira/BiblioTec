using BiblioTec.Models.Livro;
using Microsoft.EntityFrameworkCore;

namespace BiblioTec.Data;

public class BibliotecContext : DbContext
{
    public BibliotecContext(DbContextOptions<BibliotecContext> opts)
        : base(opts)
    {
    }

    public DbSet<Livro> Livros { get; set; }
}