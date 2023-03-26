using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using BackBiblioteca.Data;
using BackBiblioteca.Interfaces;
using BackBiblioteca.Models;
using BackBiblioteca.Respostas;
using BackBiblioteca.Services.Dao;

namespace BackBiblioteca.Services;

public class AlunoService : IAlunoService
{
    //private readonly BibliotecContext _context;
    private readonly AlunoDao _alunoDao;
    private readonly EmprestimoDao _emprestimoDao;
    public AlunoService(BibliotecContext context)
    {
        //_context = context;
        _alunoDao = new AlunoDao(context);
        _emprestimoDao = new EmprestimoDao(context);
    }
    
    public string Apagar(int matricula)
    {
        try
        {
            _alunoDao.Apagar(matricula);
            return OperacaoConcluida.Sucesso002;
        }
        catch (Exception e)
        {
            Console.WriteLine("[ X ] ERRO NÃO ESPERADO, POR FAVOR TENTE FAZER ESTÁ OPERAÇÃO NOVAMENTE !!!");
            Console.WriteLine(e);
            throw;
        }
    }

    public string Cadastrar(Aluno alunoParaAdicionar)
    {
        try
        {
            _alunoDao.Cadastrar(alunoParaAdicionar);
            return OperacaoConcluida.Sucesso001;    
        }
        catch (Exception e)
        {
            Console.WriteLine("[ X ] ERRO NÃO ESPERADO, POR FAVOR TENTE FAZER ESTÁ OPERAÇÃO NOVAMENTE !!!");
            Console.WriteLine(e);
            throw;
        }
    }

    public string CompararDadosDeAluno(Aluno alunoEmVerificacao)
    {

        var alunoCadastrado = _alunoDao.BuscarPorMatricula(alunoEmVerificacao.Matricula);
        if (alunoCadastrado != alunoEmVerificacao)
        {
            if (alunoCadastrado?.Nome != alunoEmVerificacao.Nome)
            {
                return AlunoErro.Erro021;
            }

            if (alunoCadastrado?.Sala != alunoEmVerificacao.Sala)
            {
                return AlunoErro.Erro011;
            }

            if (alunoCadastrado.Turno != alunoEmVerificacao.Turno)
            {
                return AlunoErro.Erro031;
            }
        }
        return "";
    }

    public Aluno? BuscarPorMatricula(int matricula)
    {
        return _alunoDao.BuscarPorMatricula(matricula);
    }

    public string Editar(Aluno alunoParaEditar)
    {
        try
        {
            _alunoDao.Editar(alunoParaEditar);
            return OperacaoConcluida.Sucesso003;
        }
        catch (Exception e)
        {
            Console.WriteLine("[ X ] ERRO NÃO ESPERADO, POR FAVOR TENTE FAZER ESTÁ OPERAÇÃO NOVAMENTE !!!");
            Console.WriteLine(e);
            throw;
        }
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

    public string VerificarCampos(Aluno alunoEmVerificacao)
    {
        if (alunoEmVerificacao.Matricula <= 0)
        {
            return AlunoErro.Erro000;
        }

        if (alunoEmVerificacao.Nome == "")
        {
            return AlunoErro.Erro020;
        }

        if (alunoEmVerificacao.Sala <= 0)
        {
            return AlunoErro.Erro010;
        }

        if (alunoEmVerificacao.Turno != "1" && alunoEmVerificacao.Turno != "2")
        {
            return AlunoErro.Erro030;
        }
        return "";
    }

    public bool VerificarMatricula(int matricula)
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
