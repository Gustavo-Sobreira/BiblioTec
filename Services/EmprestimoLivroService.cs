using BackBiblioteca.Data;
using BackBiblioteca.Models;

namespace BackBiblioteca.Services;

public class EmprestimoLivroService
{
    private BibliotecContext _context;

    public EmprestimoLivroService(BibliotecContext context)
    {
        _context = context;
    }

    public bool ValidarIntegrantes(int codigo, int matricula)
    {
        Emprestimo pendencia = PesquisarEmprestimo(matricula);
        if (pendencia != null)
        {
            Console.WriteLine("Há um emprétimo com está matrícula");
            return false;
        }
        
        var ultimoEmprestimo = _context.emprestado
            .Where(i => i.id_emprestimo > -1)
            .OrderBy(emprestimo => emprestimo.id_emprestimo).Last();

        var matriculaAluno = _context.aluno.Where(aluno => aluno.matricula == matricula).FirstOrDefault();
        var codigoLivro = _context.livro.Where(livro => livro.codigo == codigo).FirstOrDefault();
        
        if (matriculaAluno == null || codigoLivro == null)
        {
            Console.WriteLine("Matrícula ou Código do livro inválidos");
            return false;
        }

        int id = ultimoEmprestimo.id_emprestimo + 1;

        AdicionarEmprestimo(id,codigo,matricula);
        return true;
    }

    private void AdicionarEmprestimo(int id, int codigo, int matricula)
    {
        Emprestimo novoEmprestimo = new Emprestimo()
        {
            id_emprestimo = id,
            codigo_livro = codigo,
            matricula_aluno = matricula
        };
        _context.emprestado.Add(novoEmprestimo);
        _context.SaveChanges();
    }

    private Emprestimo? PesquisarEmprestimo(int matricula)
    {
        Emprestimo objeto = _context.emprestado.Where(emprestimo => emprestimo.matricula_aluno == matricula).FirstOrDefault();
        return objeto;
    }

    public bool DevolverLivro(int matricula, int codigo)
    {
        
        Emprestimo pendencia = PesquisarEmprestimo(matricula);
        if ((pendencia != null) && (pendencia.codigo_livro == codigo))
        {
            _context.emprestado.Remove(pendencia);
            _context.SaveChanges();
            return true;
        }

        Console.WriteLine("Este aluno não pegou este livro");
        return false;
    }
}