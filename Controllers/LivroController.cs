using BackBiblioteca.Data;
using BackBiblioteca.Models;
using BackBiblioteca.Services;
using Microsoft.AspNetCore.Mvc;
using static BackBiblioteca.Errors.Livro.PendenteErros;
using static BackBiblioteca.Errors.Livro.RegistroErros;
using static BackBiblioteca.Errors.Livro.TituloErros;

namespace BackBiblioteca.Controllers;

[ApiController]
[Route("livro")]
public class LivroController : Controller
{
    private readonly LivroService _livroService;
    private readonly TextosService _textoService;
    public LivroController(BibliotecContext context)
    {
        _livroService = new LivroService(context);
        _textoService = new TextosService();
    }

    [HttpGet("estoque/{skip}/{take}")]
    public ActionResult ListarTodosLivrosEmEstoque(int skip = 0, int take = 25)
    {
        try
        {
            var todoEstoque = _livroService.ListarEstoqueDisponivelParaEmprestimo(skip, take);
            return Ok(todoEstoque);
        }
        catch (Exception e)
        {
            return HandleException(e, e.Message);
        }
    }

    [HttpGet("buscar/{registro}")]
    public ActionResult BuscarLivro(string registro)
    {
        try
        {
            var livro = _livroService.BuscarPorRegistro(registro);
            if (livro == null)
            {
                throw new LivroRegistroNaoEncontradoException();
            }
            return Ok(livro);
        }
        catch (Exception e)
        {
            return HandleException(e, e.Message);
        }
    }

    [HttpGet("buscar/todos/{skip}/{take}")]
    public ActionResult BuscarTodosLivros(int skip = 0, int take = 25)
    {
        try{
            List<Livro> lisvrosExistentes = _livroService.BuscarTodosLivros(skip,take);
            return Ok(lisvrosExistentes);
        }
        catch (Exception e)
        {
            return HandleException(e, e.Message);
        }
    }

    [HttpGet("localizar/{titulo}")]
    public ActionResult LocalizarLivroPeloTitulo(string titulo){
        try
        {
            string tituloFormatado = _textoService.FormatarTextos(titulo);
            List<string> localizacao = _livroService.LocalizarLivroPeloTitulo(tituloFormatado)!;
            return Ok(localizacao);
        }
        catch (Exception e)
        {
            return HandleException(e, e.Message);
        }
    }


    [HttpPost("cadastro")]
    public ActionResult CadastrarNovoLivro([FromBody] Livro livro)
    {
        try
        {
            Livro livroFormatado = _livroService.FormatarCampos(livro);
            _livroService.RegrasParaCadastrar(livroFormatado);
            Livro livroCadastrado = _livroService.Cadastrar(livroFormatado);
            return Ok(livroCadastrado);
        }
        catch (Exception e)
        {
            return HandleException(e, e.Message);
        }
    }

    [HttpPut]
    [Route("editar")]
    public ActionResult EditarLivroExistente([FromBody] Livro livro)
    {
        try
        {
            Livro livroFormatado = _livroService.FormatarCampos(livro);
            _livroService.RegrasParaEditar(livroFormatado);
            Livro livroEditado = _livroService.Editar(livroFormatado)!;
            return Ok(livroEditado);
        }
        catch (Exception e)
        {
            return HandleException(e, e.Message);
        }
    }

    [HttpDelete("apagar/{registro}")]
    public ActionResult RemoverLivroDaBiblioteca(string registro)
    {
        try
        {
            var livro = _livroService.BuscarPorRegistro(registro);
            if (livro == null)
            {
                throw new LivroRegistroNaoEncontradoException();
            }
            _livroService.RegrasParaEditar(livro);
            Livro livroApagado = _livroService.Apagar(livro.Registro!)!;
            return Ok(livroApagado);
        }
        catch (Exception e)
        {
            return HandleException(e, e.Message);
        }
    }

    private ActionResult HandleException(Exception e, string errorMessage)
    {
        switch (e)
        {
            // 400 - Requisição não atende requisitos
            case LivroRegistroNuloException:
                return StatusCode(400, e.Message);
            // 403 - Permissão negada'
            case LivroPendenteException:
                return StatusCode(403, e.Message);
            // 404 - Não encontrado
            case LivroTituloNaoEncontradoException:
            case LivroRegistroNaoEncontradoException:
                return StatusCode(404, e.Message);
            // 409 - Conflito de ID
            case LivroRegistroExistenteException:
                return StatusCode(409, e.Message);
            default:
                return StatusCode(500, e.Message);
        }
    }
}