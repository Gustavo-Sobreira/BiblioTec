using BackBiblioteca.Data;
using BackBiblioteca.Respostas;
using BackBiblioteca.Models;
using BackBiblioteca.Interfaces;
using BackBiblioteca.Services.Dao;

namespace BackBiblioteca.Services;

public class LivroService : ILivroService
{
    private readonly LivroDao _livroDao;
    private readonly EmprestimoDao _emprestimoDao;
    private readonly AlunoService _alunoService;
    public LivroService(BibliotecContext context)
    {
        _livroDao = new LivroDao(context);
        _emprestimoDao = new EmprestimoDao(context);
        _alunoService = new AlunoService(context);
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
            throw new  Exception(e.Message);
        }
    }
    public void RegrasParaCadastrar(Livro livro)
    {
        livro.Autor = _alunoService.FormatarTextos(livro.Autor!);
        livro.Titulo = _alunoService.FormatarTextos(livro.Titulo!);
        VerificarCampos(livro);
        
        if (VerificarRegistro(livro.Registro))
        {
            throw new Exception(ErrorMensage.LivroRegistroExistente);
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
            throw new Exception(e.Message);
        }
    }
    public void RegrasParaEditar(Livro livro)
    {
        livro.Autor = _alunoService.FormatarTextos(livro.Autor!);
        livro.Titulo = _alunoService.FormatarTextos(livro.Titulo!);
        VerificarCampos(livro);
        
        if (!VerificarRegistro(livro.Registro))
        {
            throw new Exception(ErrorMensage.LivroRegistroNaoEncontrado);
        }

        if (VerificarPendenciaLivro(livro.Registro))
        {
            throw new Exception(ErrorMensage.LivroPendente);
        }
    }
    
    
    public string Apagar(int registro)
    {
        try
        {
            _livroDao.Apagar(registro);
            return OperacaoConcluida.Sucesso002;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public void CompararCampos(Livro livroEmVerificacao)
    {
        var livroCadastrado = _livroDao.BuscarPorRegistro(livroEmVerificacao.Registro);
        if (livroCadastrado!.Autor != livroEmVerificacao.Autor)
        {
            throw new Exception(ErrorMensage.LivroAutorIncompativel);
        }

        if (livroCadastrado.Titulo != livroEmVerificacao.Titulo)
        {
            throw new Exception(ErrorMensage.LivroTituloIncompativel);
        }
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
    public Livro? BuscarPorRegistro(int registro)
    {
        return _livroDao.BuscarPorRegistro(registro);
    }
    public bool VerificarRegistro(int registro)
    {
        var livroEncontrado = _livroDao.BuscarPorRegistro(registro);
        return livroEncontrado == null ? false : true;
    }
    public void VerificarCampos(Livro livroEmVerificacao)
    {
        if (livroEmVerificacao.Registro <= 0) {
            throw new Exception(ErrorMensage.LivroRegistroNulo);
        }
        if (livroEmVerificacao.Autor == null) {
            throw new Exception(ErrorMensage.LivroAutorNulo);
        }
        if (livroEmVerificacao.Titulo == null) {
            throw new Exception(ErrorMensage.LivroTituloNulo);
        }
    }
    public bool VerificarPendenciaLivro(int registro)
    {
        var pendente = _emprestimoDao.BuscarPorRegistro(registro);
        return pendente == null ? false : true;
    }
}