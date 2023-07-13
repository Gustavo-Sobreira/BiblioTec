using BackBiblioteca.Data;
using BackBiblioteca.Interface;
using BackBiblioteca.Models;

namespace BackBiblioteca.Services.Dao;

public class LivroDao : ILivroDao
{
    private readonly BibliotecContext _context;

    public LivroDao(BibliotecContext context)
    {
        _context = context;
    }

    public void Apagar(Livro livro)
    {
        _context.Livros.Remove(livro);
        _context.SaveChanges();
    }

    public Livro? BuscarPorRegistro(string registro)
    {
        return _context.Livros.Find(registro);
    }

    public void Cadastrar(Livro livroParaAdicionar)
    {
        _context.Livros.Add(livroParaAdicionar);
        _context.SaveChanges();     
    }

    public void Editar(Livro livroParaEditar)
    {
        _context.Livros.Remove(BuscarPorRegistro(livroParaEditar.Registro!)!);
        _context.Livros.Add(livroParaEditar);
        _context.SaveChanges();
    }
    
    public List<Livro> ListarTodosLivrosExistentes(int skip, int take)
    {
        return _context.Livros
        .OrderBy(livro => livro.Titulo)
        .Skip(skip)
        .Take(take)
        .ToList();
    }
    

    public List<Livro> ListarTodosLivrosDisponiveis()
    {
        return _context.Livros
            .Where(l => !_context.Emprestimos.Any(e => e.Registro == l.Registro))
            .OrderBy(livro => livro.Titulo)
            .ToList();
    }

    public int ContarTotalLivrosIguaisDisponiveis(string autor, string titulo)
    {
        return _context.Livros
            .Where(l => !_context.Emprestimos.Any(e => e.Registro == l.Registro)
            && _context.Livros.Any(e => e.Autor == autor)
            && _context.Livros.Any(e => e.Titulo == titulo))
            .OrderBy(livro => livro.Titulo)
            .ToList().Count();
    }

    internal List<Livro> LocalizarLivroDisponiveisPeloTitulo(string titulo, int skip, int take)
    {
        return _context.Livros
        .Where(l => !_context.Emprestimos.Any(e => e.Registro == l.Registro) 
        && l.Titulo.Contains(titulo))
        .OrderBy(l => l.Titulo)
        .Skip(skip)
        .Take(take)
        .ToList();
    }
}
