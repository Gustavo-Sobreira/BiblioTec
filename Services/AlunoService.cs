using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using BackBiblioteca.Data;
using BackBiblioteca.Errors;
using BackBiblioteca.Interfaces;
using BackBiblioteca.Models;
using BackBiblioteca.Respostas;
using BackBiblioteca.Services.Dao;

namespace BackBiblioteca.Services;

public class AlunoService : IAlunoService
{
    private readonly AlunoDao _alunoDao;
    private readonly EmprestimoDao _emprestimoDao;
    public AlunoService(BibliotecContext context)
    {
        _alunoDao = new AlunoDao(context);
        _emprestimoDao = new EmprestimoDao(context);
    }
    
    public Aluno? Cadastrar(Aluno alunoParaAdicionar)
    {
        try
        {
            _alunoDao.Cadastrar(alunoParaAdicionar);
            return alunoParaAdicionar;    
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public void RegrasParaCadastro(Aluno aluno)
    {
        aluno.Nome = FormatarTextos(aluno.Nome!);
        aluno.Turno = FormatarTextos(aluno.Turno!);
        VerificarCampos(aluno);
        
        if (VerificarMatriculaExiste(aluno.Matricula))
        {
            throw new AlunoMatriculaExistenteException();
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
        aluno.Nome = FormatarTextos(aluno.Nome!);
        aluno.Turno = FormatarTextos(aluno.Turno!);
        VerificarCampos(aluno);
        
        if (!VerificarMatriculaExiste(aluno.Matricula))
        {
            throw new AlunoMatriculaNaoEncontradaException();
        }

        if (VerificarPendenciaAluno(aluno.Matricula))
        {
            throw new AlunoPendenteException();
        }
    }
    

    public Aluno? Apagar(int matricula)
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

    
    public Aluno? BuscarAlunoPorMatricula(int matricula)
    {
        return _alunoDao.BuscarPorMatricula(matricula);
    }
    
    public string FormatarTextos(string campoEmVerificacao)
    {
        //Remover Espaços
        campoEmVerificacao = campoEmVerificacao.Trim();
        campoEmVerificacao = Regex.Replace(campoEmVerificacao, @"\s+", " ");

        //Remove maiusculas
        campoEmVerificacao = campoEmVerificacao.ToLower();

        //Remove acentos
        campoEmVerificacao = Regex.Replace(
        campoEmVerificacao.Normalize(NormalizationForm.FormD),
        @"[\p{M}]+", 
        "",
        RegexOptions.Compiled,
        TimeSpan.FromSeconds(0.5)
        );
        
        //Toda palavra começa com maiúscula
        CultureInfo cultureInfo = CultureInfo.CurrentCulture;
        TextInfo textInfo = cultureInfo.TextInfo;
        campoEmVerificacao = textInfo.ToTitleCase(campoEmVerificacao);

        return campoEmVerificacao;
    }

    public void VerificarCampos(Aluno alunoEmVerificacao)
    {
        if (alunoEmVerificacao.Matricula <= 0)
        {
            throw new AlunoMatriculaInvalidaException();
        }
        

        if (alunoEmVerificacao.Nome!.Length > 100 || alunoEmVerificacao.Nome.Length <= 0)
        {
            throw new AlunoNomeInvalidoException();
        }
  

        if (alunoEmVerificacao.Sala <= 0)
        {
            throw new AlunoSalaNuloException();
        }

        if (alunoEmVerificacao.Turno != "1" && alunoEmVerificacao.Turno != "2")
        {
            throw new AlunoTurnoIncorretoException();
        }
    }

    public bool VerificarMatriculaExiste(int matricula)
    {
        var aluno = _alunoDao.BuscarPorMatricula(matricula);
        return aluno == null ? false : true;
    }

    public bool VerificarPendenciaAluno(int matricula)
    {
        var alunoEstaPendente = _emprestimoDao.BuscarPorMatricula(matricula);
        return alunoEstaPendente == null ? false : true;
    }
}