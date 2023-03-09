using BackBiblioteca.Data;
using BackBiblioteca.Models;

namespace BackBiblioteca.Services;

public class AlunoServices
{
    private readonly BibliotecContext _context;

    public AlunoServices(BibliotecContext context)
    {
        _context = context;
    }
    
    public bool VerificarMatriculaAluno(int matricula)
    {
        Aluno? alunoExiste = _context.aluno
            .FirstOrDefault(aluno => aluno.matricula == matricula);
        return (alunoExiste != null) ? true: false;
    }
    public bool VerificarPendenciaAluno(int matricula)
    {
        Emprestado? pendencia = _context.emprestado
            .FirstOrDefault(emprestado => emprestado.matricula_aluno == matricula);
        return pendencia != null? true : false;
    }
}