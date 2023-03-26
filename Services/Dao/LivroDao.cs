using BackBiblioteca.Data;
using BackBiblioteca.Interfaces;
using BackBiblioteca.Models;
using BackBiblioteca.Respostas;

namespace BackBiblioteca.Services.Dao;

public class LivroDao : ILivroDao
{
    private readonly BibliotecContext _context;

    public LivroDao(BibliotecContext context)
    {
        _context = context;
    }

    public void Apagar(Livro livroParaRemover)
    {
        _context.Livros.Remove(livroParaRemover);
        _context.SaveChanges();
    }

    public Livro? BuscarPorRegistro(int registro)
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
        _context.Livros.Remove(BuscarPorRegistro(livroParaEditar.Registro)!);
        _context.Livros.Add(livroParaEditar);
        _context.SaveChanges();
    }
    
    public List<string> ListarEstoque()
    {
        return _context!.Livros
            .Where(livro => !_context.Emprestimos
            .Any(emprestimo => emprestimo.Registro == livro.Registro))
            .GroupBy(livro => livro.Titulo, livro => livro.Autor)
            .OrderBy(grupo => grupo.Key)
            .Select(grupo => $"{grupo.Key}: {grupo.Count()}")
            .ToList();
    }

}
