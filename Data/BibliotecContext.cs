using BackBiblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace BackBiblioteca.Data;

public class BibliotecContext : DbContext
{
    public BibliotecContext(DbContextOptions<BibliotecContext> options):base(options){}
    
    public DbSet<Livro> livro { get; set; } = null!;
    
    public DbSet<Aluno> aluno { get; set; } = null!;

    public DbSet<Emprestimo> emprestado { get; set; } = null!;
}

