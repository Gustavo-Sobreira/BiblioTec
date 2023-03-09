using BackBiblioteca.Data;
using BackBiblioteca.Models;

namespace BackBiblioteca.Services;

public class ValidacaoCadastroService
{
    private readonly BibliotecContext _context;

    public ValidacaoCadastroService(BibliotecContext context)
    {
        _context = context;
    }

    // public bool CadastrarLivro(int codigo, string nome, string autor, int prazo)
    // {
    //     Livro livroEmVerificacao = new Livro()
    //     {
    //         codigo = codigo,
    //         nome = nome,
    //         autor = autor,
    //         prazo = prazo
    //     };
    //     try
    //     {
    //         _context.livro.Add(livroEmVerificacao);
    //         _context.SaveChanges();
    //         return true;
    //     }
    //     catch
    //     {
    //         return false;
    //     }
    // }

    public bool CadastrarAluno(int matricula, string nome, int sala, string turno)
    {
        Aluno alunoEmVerificacao = new Aluno()
        {
            matricula = matricula,
            nome = nome,
            sala = sala,
            turno = turno
        };
        try
        {
            _context.aluno.Add(alunoEmVerificacao);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}