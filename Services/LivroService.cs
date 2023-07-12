using BackBiblioteca.Data;
using BackBiblioteca.Models;
using BackBiblioteca.Services.Dao;
using BackBiblioteca.Interface;
using BackBiblioteca.Services.DTO;
using static BackBiblioteca.Errors.Livro.PendenteErros;
using static BackBiblioteca.Errors.Livro.RegistroErros;
using static BackBiblioteca.Errors.Livro.TituloErros;


namespace BackBiblioteca.Services;

public class LivroService : ILivroService
{
    private readonly LivroDao _livroDao;
    private readonly EmprestimoDao _emprestimoDao;

    private readonly TextosService _textoService;
    public LivroService(BibliotecContext context)
    {
        _livroDao = new LivroDao(context);
        _emprestimoDao = new EmprestimoDao(context);
        _textoService = new TextosService();
    }

    public Livro Cadastrar(Livro livroParaAdicionar)
    {
        try
        {
            _livroDao.Cadastrar(livroParaAdicionar);
            return livroParaAdicionar;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Livro FormatarCampos(Livro livroSemFormatacao)
    {
        Livro livroFormatado = new Livro
        {
            Registro = _textoService.FormatarIds(livroSemFormatacao.Registro!),
            Autor = _textoService.FormatarTextos(livroSemFormatacao.Autor!),
            Titulo = _textoService.FormatarTextos(livroSemFormatacao.Titulo!),
            Editora = _textoService.FormatarTextos(livroSemFormatacao.Editora!),
            Genero = _textoService.FormatarTextos(livroSemFormatacao.Genero!),
            Prateleira = _textoService.FormatarTextos(livroSemFormatacao.Prateleira!)
        };
        return livroFormatado;
    }

    public void RegrasParaCadastrar(Livro livro)
    {

        VerificarCampos(livro);

        if (BuscarPorRegistro(livro.Registro!) != null)
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
        VerificarCampos(livro);

        if (BuscarPorRegistro(livro.Registro!) == null)
        {
            throw new LivroRegistroNaoEncontradoException();
        }

        if (VerificarPendenciaLivro(livro.Registro!))
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

    public IEnumerable<LivroDTO> ListarEstoqueDisponivelParaEmprestimo(int skip, int take)
    {
        var listaLivrosContados = new List<LivroDTO>();

        var todosLivrosDisponiveisEmEstoque = _livroDao.ListarTodosLivrosDisponiveis();
        for (int i = 0; i < todosLivrosDisponiveisEmEstoque.Count;)
        {
            int contador = ContarLivrosIguais(i, 0, todosLivrosDisponiveisEmEstoque);

            LivroDTO livroAdd = new LivroDTO
            {
                TotalEmEstoque = contador,
                Titulo = todosLivrosDisponiveisEmEstoque[i].Titulo,
                Autor = todosLivrosDisponiveisEmEstoque[i].Autor,
                Editora = todosLivrosDisponiveisEmEstoque[i].Editora,
                Genero = todosLivrosDisponiveisEmEstoque[i].Genero
            };

            listaLivrosContados.Add(livroAdd);

            i += contador;
        }
        var resultado = listaLivrosContados.Skip(skip).Take(take);
        return resultado;
    }


    private int ContarLivrosIguais(int indice, int contador, List<Livro> listaDeEstoque)
    {
        if (indice >= listaDeEstoque.Count - 1)
        {
            return contador + 1;
        }

        if (listaDeEstoque[indice].Autor != listaDeEstoque[indice + 1].Autor
        || listaDeEstoque[indice].Titulo != listaDeEstoque[indice + 1].Titulo)
        {
            return contador + 1;
        }

        return ContarLivrosIguais(indice + 1, contador + 1, listaDeEstoque);
    }

    public Livro? BuscarPorRegistro(string registro)
    {
        return _livroDao.BuscarPorRegistro(registro);
    }

    public void VerificarCampos(Livro livroEmVerificacao)
    {
        long registro = long.Parse(livroEmVerificacao.Registro!);
        if (registro <= 0)
        {
            throw new LivroRegistroNuloException();
        }
    }
    public bool VerificarPendenciaLivro(string registro)
    {
        var pendente = _emprestimoDao.BuscarPorRegistro(registro);
        return pendente == null ? false : true;
    }

    internal List<Livro> BuscarTodosLivros(int skip, int take)
    {
        return _livroDao.ListarTodosLivrosExistentes(skip, take);
    }

    internal List<Livro?> BuscarLivrosPeloTitulo(string titulo, int skip, int take)
    {
        List<Livro> livrosEncontrados = _livroDao.LocalizarLivroDisponiveisPeloTitulo(titulo, skip, take)!;
        if (livrosEncontrados.Count == 0)
        {
            throw new LivroTituloNaoEncontradoException();
        }
        
        return livrosEncontrados!;    
    }
}


