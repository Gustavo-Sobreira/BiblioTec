using BackBiblioteca.Data;
using BackBiblioteca.Models;
using BackBiblioteca.Respostas;

namespace BackBiblioteca.Services;

public class EmprestimoServices
{
    private readonly BibliotecContext _context;

    public EmprestimoServices(BibliotecContext context)
    {
        _context = context;
    }

    public string EmprestarLivro(int registro, int matricula)
    {
        var valido = ValidarEmprestimo(registro, matricula);
        if (valido != "")
        {
            return valido;
        }

        var buscarUltimoId = _context.Emprestimos.Max(emprestimo => emprestimo.IdEmprestimo);
        var novoEmprestimo = new Emprestimo()
        {
            IdEmprestimo = buscarUltimoId + 1,
            Matricula = matricula,
            Registro = registro,
            DataEmprestimo = DateTime.Now
        };
        try
        {
            _context.Emprestimos.Add(novoEmprestimo);
            _context.SaveChanges();
            return OperacaoConcluida.Sucesso004;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private string ValidarEmprestimo(int registro, int matricula)
    {
        var aluno = VerificarDadosAluno(matricula);
        if (aluno != "")
        {
            return aluno;
        }

        var livro = VerificarDadosLivro(registro);
        if (livro != "")
        {
            return livro;
        }

        return "";
    }

    private string VerificarDadosAluno(int matricula)
    {
        var alunoEmVerificacao = new AlunoServices(_context);
        var matriculaEmVerificacao = alunoEmVerificacao.VerificarMatricula(matricula);
        if (!matriculaEmVerificacao)
        {
            return AlunoErro.Erro001;
        }

        var pendenciaAluno = alunoEmVerificacao.VerificarPendenciaAluno(matricula);
        if (pendenciaAluno != null)
        {
            return EmprestimoErro.Erro071;
        }

        return "";
    }

    private string VerificarDadosLivro(int registro)
    {
        var livroEmVerificacao = new LivroServices(_context);
        var registroValido = livroEmVerificacao.PesquisarLivroPorRegistro(registro);
        if (registroValido == null)
        {
            return LivroErro.Erro041;
        }

        var pedenciaLivro = livroEmVerificacao.VerificarPendenciaLivro(registro);
        if (pedenciaLivro != "")
        {
            return pedenciaLivro;
        }

        return "";
    }

    public string DevolverLivro(int registro, int matricula)
    {
        var valido = ValidarDevolicao(registro, matricula);
        if (valido != "")
        {
            return valido;
        }

        try
        {
            var livroParaDevolucao = _context.Emprestimos.First(
                emprestado => emprestado.Registro == registro
            );
            _context.Emprestimos.Remove(livroParaDevolucao);
            _context.SaveChanges();
            return OperacaoConcluida.Sucesso005;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private string ValidarDevolicao(int registro, int matricula)
    {
        var aluno = VerificarDadosAluno(matricula);
        if (aluno == "")
        {
            return EmprestimoErro.Erro072;
        }

        var livro = VerificarDadosLivro(registro);
        if (livro == "")
        {
            return EmprestimoErro.Erro073;
        }

        var alunoPendencia = BuscarLivroPorMatricula(matricula);
        if (alunoPendencia == null)
        {
            return AlunoErro.Erro001;
        }

        var livroPendencia = BuscarLivroPorRegistro(registro);
        if (livroPendencia != alunoPendencia)
        {
            return EmprestimoErro.Erro074;
        }
    
        return "";
    }

    private Livro? BuscarLivroPorMatricula(int matricula)
    {
        var emprestimo = _context.Emprestimos.FirstOrDefault(
            emprestimo => emprestimo.Matricula == matricula
        );

        var livro = new LivroServices(_context);
        if (emprestimo != null)
        {
            return livro.PesquisarLivroPorRegistro(emprestimo.Registro);
        }

        return null;
    }

    private Livro? BuscarLivroPorRegistro(int registro)
    {
        var emprestimo = _context.Emprestimos.FirstOrDefault(
            emprestimo => emprestimo.Registro == registro
        );

        var livro = new LivroServices(_context);
        if (emprestimo != null)
        {
            return livro.PesquisarLivroPorRegistro(emprestimo.Registro);
        }

        return null;
    }
}
