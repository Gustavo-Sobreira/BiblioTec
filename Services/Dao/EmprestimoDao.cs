using BackBiblioteca.Data;
using BackBiblioteca.Interfaces;
using BackBiblioteca.Models;

namespace BackBiblioteca.Services.Dao;

public class EmprestimoDao : IEmprestimoDao
{
    private readonly BibliotecContext _context;
       
    public EmprestimoDao(BibliotecContext context)
    {
        _context = context;
    }
    
    public void Cadastrar(Emprestimo novoEmprestimo)
    {
       _context.Emprestimos.Add(novoEmprestimo);
       _context.SaveChanges();
    }

    public Emprestimo? BuscarPorMatricula(int matricula)
    {
        return _context.Emprestimos.FirstOrDefault(emprestimo => emprestimo.Matricula == matricula);
    }
    
    public Emprestimo? BuscarPorRegistro(int registro)
    {
        return _context.Emprestimos.FirstOrDefault(emprestimo => emprestimo.Registro == registro);
    }

    public int BuscarUltimoId()
    {
        int ultimoId = _context.Emprestimos
            .Select(emprestimo => emprestimo.IdEmprestimo).Max();
        return ultimoId;
    }
    
    public void Editar(int matricula, int registro)
    {
        throw new NotImplementedException();
    }

    public void Apagar(int registro)
    {
        _context.Emprestimos.Remove(BuscarPorRegistro(registro)!);
        _context.SaveChanges();
    }
}