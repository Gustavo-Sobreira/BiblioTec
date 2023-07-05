using BackBiblioteca.Data;
using BackBiblioteca.Errors;
using BackBiblioteca.Models;
using BackBiblioteca.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackBiblioteca.Controllers;

[ApiController]
[Route("livro")]
public class LivroController : Controller
{
    private readonly LivroService _livroService;
    public LivroController(BibliotecContext context)
    {
        _livroService = new LivroService(context);
    }

    [HttpGet("estoque/{skip}/{take}")]
    public ActionResult ListarTodosLivrosEmEstoque(int skip, int take)
    {
        try
        {
            var todoEstoque = _livroService.ListarEstoqueDisponivelParaEmprestimo(skip,take);
            return Ok(todoEstoque);
        }
        catch (Exception e)
        {
            return HandleException(e,e.Message);
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
            return HandleException(e,e.Message);
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
            return HandleException(e,e.Message);
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
            return HandleException(e,e.Message);
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
            return HandleException(e,e.Message);
        }
    }

    private ActionResult HandleException(Exception e, string errorMessage)
    {
        switch(e)
        {
            // 400 - Requisição não atende requisitos
            case LivroRegistroNuloException:
            case LivroAutorNuloException:
            case LivroTituloNuloException:
                return StatusCode(400,e.Message);
            // 403 - Permissão negada'
            case LivroPendenteException:
                return StatusCode(403,e.Message);
            // 404 - ID não encontrado
                case LivroRegistroNaoEncontradoException:
                    return StatusCode(404,e.Message);
            // 409 - Conflito de ID
            case LivroRegistroExistenteException:
                return StatusCode(409,e.Message);
            // 412 - Incompatibilidade de dados
            case LivroTituloIncompativelException:
            case LivroAutorIncompativelException:
                return StatusCode(412, e.Message);
            default:
                return StatusCode(500, e.Message);               
        }
    }
}