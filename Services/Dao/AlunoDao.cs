using BackBiblioteca.Data;
using BackBiblioteca.Models;
using BackBiblioteca.Interfaces;

namespace BackBiblioteca.Services.Dao;

public class AlunoDao : IAlunoDao
{
    private readonly BibliotecContext _context;
       
    public AlunoDao(BibliotecContext context)
    {
        _context = context;
    }

    public void Apagar(Aluno alunoParaRemover)
    {
        _context.Alunos.Remove(alunoParaRemover!);
        _context.SaveChanges();
    }
    
    public void Cadastrar(Aluno alunoParaAdicionar)
    {
        _context.Alunos.Add(alunoParaAdicionar);
        _context.SaveChanges();
    }
    public Aluno? BuscarPorMatricula(int matricula)
    {
        return _context.Alunos.Find(matricula);
    }
    
    public void Editar(Aluno alunoParaEditar)
    {
        _context.Alunos.Remove(BuscarPorMatricula(alunoParaEditar.Matricula)!);
        _context.Alunos.Add(alunoParaEditar);
        _context.SaveChanges();
    }
    
}