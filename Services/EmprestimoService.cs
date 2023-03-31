using BackBiblioteca.Data;
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
    
    public string Emprestar(int registro, int matricula)
    {
        var novoEmprestimo = new Emprestimo()
        {
            IdEmprestimo = _emprestimoDao.BuscarUltimoId() + 1,
            Matricula = matricula,
            Registro = registro,
            DataEmprestimo = DateTime.Now.ToUniversalTime()
        };
        
        try
        {
            _emprestimoDao.Cadastrar(novoEmprestimo);
            return OperacaoConcluida.Sucesso001;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public void RegrasParaEmprestar(int registro, int matricula)
    {
        VerificarAlunoParaEmprestimo(matricula);
        VerificarLivroParaEmprestimo(registro);
    }

    private void VerificarAlunoParaEmprestimo(int matricula)
    {
        if (!_alunoService.VerificarMatriculaExiste(matricula))
        {
            throw new Exception(ErrorMensage.AlunoMatriculaNaoEncontrada);
        }
        
        if (_alunoService.VerificarPendenciaAluno(matricula))
        {
            throw new Exception(ErrorMensage.AlunoPendente);
        }
    }
    private void VerificarLivroParaEmprestimo(int registro)
    {
        if (!_livroService.VerificarRegistro(registro))
        {
            throw new Exception(ErrorMensage.LivroRegistroNaoEncontrado);
        }
        
        if (_livroService.VerificarPendenciaLivro(registro))
        {
            throw new Exception(ErrorMensage.LivroPendente);
        }
    }
    
    
    public string Devolver(int registro)
    {
        try
        {
            _emprestimoDao.Apagar(registro);
            return OperacaoConcluida.Sucesso001;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public void RegrasParaDevolver(int registro, int matricula)
    {
        VerificarAlunoParaDevolucao(matricula);
        VerificarLivroParaDevolucao(registro);
        
        var devolucaoCorreta = _emprestimoDao.BuscarPorRegistro(registro);
        if (devolucaoCorreta!.Matricula != matricula)
        {
            throw new Exception(ErrorMensage.AlunoComLivroErrado);
        }
    }
    private void VerificarAlunoParaDevolucao(int matricula)
    {
        if (!_alunoService.VerificarMatriculaExiste(matricula))
        {
            throw new Exception(ErrorMensage.AlunoMatriculaNaoEncontrada);
        }
        
        if (!_alunoService.VerificarPendenciaAluno(matricula))
        {
            throw new Exception(ErrorMensage.AlunoNaoPendente);
        }
    }
    private void VerificarLivroParaDevolucao(int registro)
    {
        if (!_livroService.VerificarRegistro(registro))
        {
            throw new Exception(ErrorMensage.LivroRegistroNaoEncontrado);
        }
        
        if (!_livroService.VerificarPendenciaLivro(registro))
        {
            throw new Exception(ErrorMensage.LivroNaoPendente);
        }
    }
}