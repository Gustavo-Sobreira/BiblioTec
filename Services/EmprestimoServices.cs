using BackBiblioteca.Data;
using BackBiblioteca.Models;

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
            Registro = registro
        };
        try
        {
            _context.Emprestimos.Add(novoEmprestimo);
            _context.SaveChanges();
            return $"Emprétimo realizado com sucesso. ID = {novoEmprestimo.IdEmprestimo}";
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
            return "Esta matrícula não está registrada, favor registrar o aluno.";
        }
        
        var pendenciaAluno = alunoEmVerificacao.VerificarPendenciaAluno(matricula);
        if (pendenciaAluno != null)
        {
            return $"Este aluno possui pendências, referentes ao N° de Registro {pendenciaAluno.Registro}."
                   + $"\nPara que o empréstimo seja liberado, deve ser feita a devolução.";
        }

        return "";
    }

    private string VerificarDadosLivro(int registro)
    {
        var livroEmVerificacao = new LivroServices(_context);
        var registroValido = livroEmVerificacao.PesquisarLivroPorRegistro(registro);
        if (registroValido == null)
        {
            return "Livro não encontrado";
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
            var livroParaDevolucao = _context.Emprestimos
                .First(emprestado => emprestado.Registro == registro);
            _context.Emprestimos.Remove(livroParaDevolucao);
            _context.SaveChanges();
            return "Devolução concuída!";
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
            return "Não há livro emprestado a este aluno.";
        }

        var livro = VerificarDadosLivro(registro);
        if (livro == "")
        {
            return "Este livro está no estoque";
        }

        var alunoPendencia = BuscarLivroPorMatricula(matricula);
        var livroPendencia = BuscarLivroPorRegistro(registro);

        if (livroPendencia != alunoPendencia)
        {
            return "Este aluno não pegou este livro.\n" +
                   $"Livro emprestado a ele: \n" +
                   $"Registro: {alunoPendencia?.Registro}\n" +
                   $"Título: {alunoPendencia?.Titulo}\n" +
                   $"Autor: {alunoPendencia?.Autor}";
        }
        return "";
    }

    private Livro? BuscarLivroPorMatricula(int matricula)
    {
        var emprestimo = _context.Emprestimos
            .FirstOrDefault(emprestimo => emprestimo.Matricula == matricula);

        var livro = new LivroServices(_context);
        if (emprestimo != null)
        {
            return livro.PesquisarLivroPorRegistro(emprestimo.Registro);
        }

        return null;
    }

    private Livro? BuscarLivroPorRegistro(int registro)
    {
        var emprestimo = _context.Emprestimos
            .FirstOrDefault(emprestimo => emprestimo.Registro == registro);

        var livro = new LivroServices(_context);
        if (emprestimo != null)
        {
            return livro.PesquisarLivroPorRegistro(emprestimo.Registro);
        }

        return null;
    }
}