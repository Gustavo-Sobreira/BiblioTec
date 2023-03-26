using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using BackBiblioteca.Data;
using BackBiblioteca.Respostas;
using BackBiblioteca.Models;
using BackBiblioteca.Interfaces;
using BackBiblioteca.Services.Dao;

namespace BackBiblioteca.Services;

public class LivroService : ILivroService
{
    //private readonly BibliotecContext _context;
    private readonly LivroDao _livroDao;
    private readonly EmprestimoDao _emprestimoDao;
    public LivroService(BibliotecContext context)
    {
        //_context = context;
        _livroDao = new LivroDao(context);
        _emprestimoDao = new EmprestimoDao(context);
    }

    public string Apagar(Livro livroParaRemover)
    {
        try
        {
            _livroDao.Apagar(livroParaRemover);
            return OperacaoConcluida.Sucesso002;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Livro? BuscarPorRegistro(int registro)
    {
        try
        {
            return _livroDao.BuscarPorRegistro(registro);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public string Cadastrar(Livro livroParaAdicionar)
    {
        try
        {
            _livroDao.Cadastrar(livroParaAdicionar);
            return OperacaoConcluida.Sucesso001;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public string Emprestar(int registro, int matricula)
    {
        var buscarUltimoId = _emprestimoDao.BuscarUltimoId();
        var novoEmprestimo = new Emprestimo()
        {
            IdEmprestimo = buscarUltimoId + 1,
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
    
    public string Editar(Livro livroParaEditar)
    {
        try
        {
            _livroDao.Editar(livroParaEditar);
            return OperacaoConcluida.Sucesso003;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool VerificarRegistro(int registro)
    {
        var livroEncontrado = _livroDao.BuscarPorRegistro(registro);
        return livroEncontrado == null ? false : true;
    }

    public string CompararCampos(Livro livroEmVerificacao)
    {
        var livroCadastrado = _livroDao.BuscarPorRegistro(livroEmVerificacao.Registro);
        if (livroCadastrado!.Autor != livroEmVerificacao.Autor)
        {
            return LivroErro.Erro051;
        }

        if (livroCadastrado.Titulo != livroEmVerificacao.Titulo)
        {
            return LivroErro.Erro061;
        }

        return "";
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


    public List<string> ListarEstoque()
    {
        try
        {
            return _livroDao.ListarEstoque();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public string VerificarCampos(Livro livroEmVerificacao)
    {
        if (livroEmVerificacao.Registro == 0) {
            return LivroErro.Erro040;
        }
        if (livroEmVerificacao.Autor == null) {
            return LivroErro.Erro050;
        }
        if (livroEmVerificacao.Titulo == null) {
            return LivroErro.Erro060;
        }
        return "";
    }

    public bool VerificarPendenciaLivro(int registro)
    {
        var pendente = _emprestimoDao.BuscarPorRegistro(registro);
        return pendente == null ? false : true;
    }
    
}
