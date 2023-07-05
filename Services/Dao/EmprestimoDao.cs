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

    public Emprestimo? BuscarPorMatricula(string matricula)
    {
        return _context.Emprestimos.FirstOrDefault(emprestimo => emprestimo.Matricula == matricula);
    }
    
    public Emprestimo? BuscarPorRegistro(string registro)
    {
        return _context.Emprestimos.FirstOrDefault(emprestimo => emprestimo.Registro == registro);
    }

    public string BuscarUltimoId()
    {
        int ultimoId = _context.Emprestimos
            .Select(emprestimo => emprestimo.IdEmprestimo).Max();
        return ultimoId.ToString();
    }
    
    public void Editar(string matricula, string registro)
    {
        throw new NotImplementedException();
    }

    public void Apagar(string registro)
    {
        _context.Emprestimos.Remove(BuscarPorRegistro(registro)!);
        _context.SaveChanges();
    }

    public List<Emprestimo> ListarPendentes()
    {
        return _context.Emprestimos
            .Where(emprestimo => emprestimo.IdEmprestimo >= 0 && (DateTime.UtcNow - emprestimo.DataEmprestimo).TotalDays > 7)
            .OrderBy(emprestimo => emprestimo.DataEmprestimo)
            .ToList();
    }
}