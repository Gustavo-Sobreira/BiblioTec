using BackBiblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace BackBiblioteca.Data;

public class BibliotecContext : DbContext
{
    public BibliotecContext(DbContextOptions<BibliotecContext> options):base(options){}
    
    public DbSet<Livro> Livros { get; set; } = null!;

    public DbSet<Aluno> Alunos { get; set; } = null!;

    public DbSet<Emprestimo> Emprestimos { get; set; } = null!;
}

