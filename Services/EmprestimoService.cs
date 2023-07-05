using System.Text.Json.Serialization;
using BackBiblioteca.Data;
using BackBiblioteca.Errors;
using BackBiblioteca.Models;
using BackBiblioteca.Respostas;
using BackBiblioteca.Services.Dao;

namespace BackBiblioteca.Services;

public class EmprestimoService
{
    private readonly EmprestimoDao _emprestimoDao;
    private readonly AlunoService _alunoService;
    private readonly LivroService _livroService;

    public EmprestimoService(BibliotecContext context)
    {
        _emprestimoDao = new EmprestimoDao(context);
        _alunoService = new AlunoService(context);
        _livroService = new LivroService(context);
    }
    
    public string Emprestar(string registro, string matricula)
    {
        var novoEmprestimo = new Emprestimo()
        {
            IdEmprestimo = 1,
            Matricula = matricula,
            Registro = registro,
            DataEmprestimo = DateTime.Now.ToUniversalTime()
        };
        
        try
        {
            _emprestimoDao.Cadastrar(novoEmprestimo);
            return OperacaoConcluida.Sucesso004;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public void RegrasParaEmprestar(string registro, string matricula)
    {
        VerificarAlunoParaEmprestimo(matricula);
        VerificarLivroParaEmprestimo(registro);
    }

    private void VerificarAlunoParaEmprestimo(string matricula)
    {
        if (_alunoService.BuscarAlunoPorMatricula(matricula) == null)
        {
            throw new AlunoMatriculaNaoEncontradaException();
        }
        
        if (_alunoService.VerificarPendenciaAluno(matricula))
        {
            throw new AlunoPendenteException();
        }
    }
    private void VerificarLivroParaEmprestimo(string registro)
    {
        if (_livroService.BuscarPorRegistro(registro) == null)
        {
            throw new LivroRegistroNaoEncontradoException();
        }
        
        if (_livroService.VerificarPendenciaLivro(registro))
        {
            throw new LivroPendenteException();
        }
    }
    
    
    public string Devolver(string registro)
    {
        try
        {
            _emprestimoDao.Apagar(registro);
            return OperacaoConcluida.Sucesso005;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public void RegrasParaDevolver(string registro, string matricula)
    {
        VerificarAlunoParaDevolucao(matricula);
        VerificarLivroParaDevolucao(registro);
        
        var devolucaoCorreta = _emprestimoDao.BuscarPorRegistro(registro);
        if (devolucaoCorreta!.Matricula != matricula)
        {
            throw new LivroAutorIncompativelException();
        }
    }
    private void VerificarAlunoParaDevolucao(string matricula)
    {
        if (_alunoService.BuscarAlunoPorMatricula(matricula) == null)
        {
            throw new AlunoMatriculaNaoEncontradaException();
        }
        
        if (!_alunoService.VerificarPendenciaAluno(matricula))
        {
            throw new AlunoNaoPendenteException();
        }
    }
    private void VerificarLivroParaDevolucao(string registro)
    {
        if (_livroService.BuscarPorRegistro(registro) == null)
        {
            throw new LivroRegistroNaoEncontradoException();
        }
        
        if (!_livroService.VerificarPendenciaLivro(registro))
        {
            throw new LivroNaoPendenteException();
        }
    }

    // public List<Object> ListarPendentes(string sala, string turno, int dias)
    // {
    
        
    //     var todosEmprestimos = _emprestimoDao.ListarPendentes();
    //     var todosPendentes = new List<Object>();
    
    //     foreach (var emprestimo in todosEmprestimos)
    //     {
    //         var aluno = _alunoService.BuscarAlunoPorMatricula(emprestimo.Matricula);
        
    //         if (sala > 0 && aluno!.Sala != sala)
    //         {
    //             continue; // pula para a próxima iteração se a sala não corresponder
    //         }

    //         if (turno > 0 && aluno!.Turno != turno.ToString())
    //         {
    //             continue; // pula para a próxima iteração se o turno não corresponder
    //         }
        
    //         if (dias > 0 && (DateTime.UtcNow - emprestimo.DataEmprestimo).TotalDays > dias)
    //         {
    //             continue; // pula para a próxima iteração se o empréstimo for mais antigo que o período especificado
    //         }
        
    //         var pendente = new { 
    //             Registro = emprestimo.Registro, 
    //             Matricula = emprestimo.Matricula, 
    //             Nome = aluno!.Nome,
    //             Sala = aluno.Sala,
    //             Turno = aluno.Turno,
    //             DataEmprestimo = emprestimo.DataEmprestimo.ToString("dd-MM-yyyy")
    //         };
        
    //         todosPendentes.Add(pendente);
    //     }
    
    //     return todosPendentes;
    // }
}

