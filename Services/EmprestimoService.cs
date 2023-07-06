using BackBiblioteca.Data;
using BackBiblioteca.Models;
using BackBiblioteca.Services.Dao;
using static BackBiblioteca.Errors.Aluno.MatriculaErros.LivroTituloNaoEncontradoException;
using static BackBiblioteca.Errors.Aluno.PendenciaErros.LivroTituloNaoEncontradoException;
using static BackBiblioteca.Errors.Livro.PendenteErros;
using static BackBiblioteca.Errors.Livro.RegistroErros;

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
    
    public void Emprestar(string registro, string matricula)
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
    
    
    public void Devolver(string registro)
    {
        try
        {
            _emprestimoDao.Apagar(registro);
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
            throw new LivroComDevolucaoIncompativel();
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

    internal List<Emprestimo> ListarPendentes(int tempo, int skip, int take)
    {
        return _emprestimoDao.ListarPendentes(tempo,skip,take);
    }
}

