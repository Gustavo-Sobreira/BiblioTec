using BackBiblioteca.Data;
using BackBiblioteca.Errors;
using BackBiblioteca.Respostas;
using BackBiblioteca.Models;
using BackBiblioteca.Interfaces;
using BackBiblioteca.Services.Dao;
using BackBiblioteca.stringerfaces;

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
    
    public Livro? Cadastrar(Livro livroParaAdicionar)
    {
        try
        {
            _livroDao.Cadastrar(livroParaAdicionar);
            return livroParaAdicionar;
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
            throw new LivroRegistroExistenteException();
        }
    }
    
    
    public Livro? Editar(Livro livroParaEditar)
    {
        try
        {
            _livroDao.Editar(livroParaEditar);
            return livroParaEditar;
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
            throw new LivroRegistroNaoEncontradoException();
        }

        if (VerificarPendenciaLivro(livro.Registro))
        {
            throw new LivroPendenteException();
        }
    }
    
    
    public Livro? Apagar(string registro)
    {
        try
        {
            var livro = BuscarPorRegistro(registro); 
            _livroDao.Apagar(livro!);
            return livro;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    // public void CompararCampos(Livro livroEmVerificacao)
    // {
    //     var livroCadastrado = _livroDao.BuscarPorRegistro(livroEmVerificacao.Registro);
    //     if (livroCadastrado!.Autor != livroEmVerificacao.Autor)
    //     {
    //         throw new LivroAutorIncompativelException();
    //     }
    //
    //     if (livroCadastrado.Titulo != livroEmVerificacao.Titulo)
    //     {
    //         throw new LivroTituloIncompativelException();
    //     }
    // }
    
    
    // public List<Livro> ListarEstoque()
    // {
    //     var listaLivrosContados = new List<Livro>();
        
    //     var todosLivros = _livroDao.ListarEstoqueCompleto();
    //     for (int i = 0; i < todosLivros.Count -1; i++)
    //     {
    //         var count = 1;
    //         while (todosLivros[i].Autor == todosLivros[i + 1].Autor 
    //         && todosLivros[i].Titulo == todosLivros[i + 1].Titulo )
    //         {
    //             count++;
    //             if (i + 2 >= todosLivros.Count)
    //             {
    //                 break;
    //             }
    //             i++;
    //         }

    //         Livro livroAdd = new Livro();
    //         livroAdd.Registro = count;
    //         livroAdd.Titulo = todosLivros[i].Titulo;
    //         livroAdd.Autor = todosLivros[i].Autor;
            
    //         listaLivrosContados.Add(livroAdd);
    //     }
    //     return listaLivrosContados;
    // }
    public Livro? BuscarPorRegistro(string registro)
    {
        return _livroDao.BuscarPorRegistro(registro);
    }
    public bool VerificarRegistro(string registro)
    {
        var livroEncontrado = _livroDao.BuscarPorRegistro(registro);
        return livroEncontrado == null ? false : true;
    }
    public void VerificarCampos(Livro livroEmVerificacao)
    {
        int registro = int.Parse(livroEmVerificacao.Registro!);
        if (registro <= 0) {
            throw new LivroRegistroNuloException();
        }
        if (livroEmVerificacao.Autor == null) {
            throw new LivroAutorNuloException();
        }
        if (livroEmVerificacao.Titulo == null) {
            throw new LivroTituloNuloException();
        }
    }
    public bool VerificarPendenciaLivro(string registro)
    {
        var pendente = _emprestimoDao.BuscarPorRegistro(registro);
        return pendente == null ? false : true;
    }

    public List<Livro> ListarEstoque()
    {
        throw new NotImplementedException();
    }
}


