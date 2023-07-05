using BackBiblioteca.Data;
using BackBiblioteca.Errors;
using BackBiblioteca.Interfaces;
using BackBiblioteca.Models;
using BackBiblioteca.Services.Dao;

namespace BackBiblioteca.Services;

public class AlunoService : IAlunoService
{
    private readonly AlunoDao _alunoDao;
    private readonly EmprestimoDao _emprestimoDao;
    private readonly TextosService _textoService;
    public AlunoService(BibliotecContext context)
    {
        _alunoDao = new AlunoDao(context);
        _emprestimoDao = new EmprestimoDao(context);
        _textoService = new TextosService();
    }

    public Aluno? BuscarAlunoPorMatricula(string matricula)
    {
        return _alunoDao.BuscarPorMatricula(matricula);
    }

    public bool VerificarPendenciaAluno(string matricula)
    {
        var alunoEstaPendente = _emprestimoDao.BuscarPorMatricula(matricula);
        return alunoEstaPendente == null ? false : true;
    }

    public Aluno FormatarCampos(Aluno alunoSemFormatacao)
    {
        Aluno alunoFormatado = new Aluno{
            Matricula = _textoService.FormatarIds(alunoSemFormatacao.Matricula!),
            Nome = _textoService.FormatarTextos(alunoSemFormatacao.Nome!),
            Professor = _textoService.FormatarTextos(alunoSemFormatacao.Professor!),
            Sala = _textoService.FormatarTextos(alunoSemFormatacao.Sala!),
            Turno = _textoService.FormatarTextos(alunoSemFormatacao.Turno!),
            Serie = _textoService.FormatarTextos(alunoSemFormatacao.Serie!)
        };
        return alunoFormatado;
    }


    public void RegrasParaCadastro(Aluno aluno)
    {
        VerificarCampos(aluno);

        if (BuscarAlunoPorMatricula(aluno.Matricula!) == null)
        {
            throw new AlunoMatriculaExistenteException();
        }
    }

    public void Cadastrar(Aluno alunoParaAdicionar)
    {
        try
        {
            _alunoDao.Cadastrar(alunoParaAdicionar);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }



    public Aluno? Editar(Aluno alunoParaEditar)
    {
        try
        {
            _alunoDao.Editar(alunoParaEditar);
            return alunoParaEditar;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public void RegrasParaEdicao(Aluno aluno)
    {
        VerificarCampos(aluno);

        if (BuscarAlunoPorMatricula(aluno.Matricula!) == null)
        {
            throw new AlunoMatriculaNaoEncontradaException();
        }

        if (VerificarPendenciaAluno(aluno.Matricula!))
        {
            throw new AlunoPendenteException();
        }
    }


    public Aluno? Apagar(string matricula)
    {
        try
        {
            var aluno = BuscarAlunoPorMatricula(matricula);
            _alunoDao.Apagar(aluno!);
            return aluno;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    // public void CompararDadosDeAluno(Aluno alunoEmVerificacao)
    // {
    //     var alunoCadastrado = BuscarAlunoPorMatricula(alunoEmVerificacao.Matricula);
    //     if (alunoCadastrado != alunoEmVerificacao)
    //     {
    //         if (alunoCadastrado?.Nome != alunoEmVerificacao.Nome)
    //         {
    //             throw new AlunoNomeIncompativelException();
    //         }
    //
    //         if (alunoCadastrado?.Sala != alunoEmVerificacao.Sala)
    //         {
    //             throw new AlunoSalaIncompativelException();
    //         }
    //
    //         if (alunoCadastrado.Turno != alunoEmVerificacao.Turno)
    //         {
    //             throw new AlunoTurnoIncompativelException();
    //         }
    //     }
    // }


   
    

    public void VerificarCampos(Aluno alunoEmVerificacao)
    {
        long id_matricula = long.Parse(alunoEmVerificacao.Matricula!);
        if (id_matricula <= 0)
        {
            throw new AlunoMatriculaInvalidaException();
        }

        int vl_sala = int.Parse(alunoEmVerificacao.Sala!);
        if (vl_sala <= 0)
        {
            throw new AlunoSalaNuloException();
        }

        int vl_serie = int.Parse(alunoEmVerificacao.Serie!);
        if (vl_serie <= 0)
        {
            throw new AlunoSerieException();
        }

        string vl_turno = alunoEmVerificacao.Turno!;
        if (vl_turno != "1" && vl_turno != "2")
        {
            throw new AlunoTurnoIncorretoException();
        }
    }

    public List<Aluno> BuscarTodosAlunos(int skip, int take)
    {
        return _alunoDao.BuscarTodosAlunos(skip,take);
    }
}