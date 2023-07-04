using BackBiblioteca.Data;
using BackBiblioteca.Errors;
using BackBiblioteca.Models;
using BackBiblioteca.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackBiblioteca.Controllers;

[ApiController]
[Route("[controller]")]
public class LivroController : Controller
{
    private readonly LivroService _livroService;

    public LivroController(BibliotecContext context)
    {
        _livroService = new LivroService(context);
    }

    [HttpGet("estoque")]
    public ActionResult ListarTodosLivrosEmEstoque()
    {
        var todoEstoque = _livroService.ListarEstoque();

        if(todoEstoque.Count == 0){
            return NotFound(Json("Não há livros disponíveis para empréstimo."));
        }
        
        return Ok(Json(todoEstoque));
    }
    
    [HttpGet("buscar")]
    public ActionResult BuscarLivro([FromQuery]string registro)
    {
        try
        {
            var livro = _livroService.BuscarPorRegistro(registro);
            if (livro == null)
            {
                throw new LivroRegistroNaoEncontradoException();
            }
            return Ok(Json(livro));
        }
        catch (Exception e)
        {
            return NotFound(Json(e.Message));
        }
    }

    [HttpPost("cadastro")]
    public ActionResult CadastrarNovoLivro([FromForm] Livro livro)
    {
        try
        {
            _livroService.RegrasParaCadastrar(livro);
            return Ok(Json(_livroService.Cadastrar(livro)));
        }
        catch (Exception e)
        {
            switch (e)
            {
                // 400 - Requisição não atende requisitos
                case LivroRegistroNuloException:
                case LivroAutorNuloException:
                case LivroTituloNuloException:
                    return BadRequest(Json(e.Message));
                // 409 - Conflito de ID
                case LivroRegistroExistenteException:
                    return Conflict(Json(e.Message));
                default:
                    return StatusCode(500, Json(e.Message));
            }
        }
    }

    [HttpPut]
    [Route("editar")]
    public ActionResult EditarLivroExistente([FromForm] Livro livro)
    {
        try
        {
            _livroService.RegrasParaEditar(livro);
            return Ok(Json(_livroService.Editar(livro)));
        }
        catch (Exception e)
        {
            switch (e)
            {
                // 404 - ID não encontrado
                case LivroRegistroNaoEncontradoException:
                    return NotFound(Json(e.Message));
                // 403 - Permissão negada
                case LivroPendenteException:
                    return StatusCode(403,Json(e.Message));
                // 412 - Incompatibilidade de dados
                case LivroTituloIncompativelException:
                case LivroAutorIncompativelException:
                    return StatusCode(412, Json(e.Message));
                // 400 - Requisição não atende requisitos
                case LivroRegistroNuloException:
                case LivroAutorNuloException:
                case LivroTituloNuloException:
                    return BadRequest(Json(e.Message));
                default:
                    return StatusCode(500, Json(e.Message));
            }
        }
    }

    [HttpDelete("apagar")]
    public ActionResult<string> RemoverLivroDaBiblioteca([FromBody] string registro)
    {
        try
        {
            var livro = _livroService.BuscarPorRegistro(registro);
            if (livro == null)
            {
                throw new LivroRegistroNaoEncontradoException();
            }
            _livroService.RegrasParaEditar(livro);
            return Ok(Json(_livroService.Apagar(livro.Registro!)));
        }
        catch (Exception e)
        {
            switch (e)
            {
                // 404 - ID não encontrado
                case LivroRegistroNaoEncontradoException:
                    return NotFound(Json(e.Message));

                // 403 - Permissão negada'
                case LivroPendenteException:
                    return StatusCode(403,Json(e.Message));

                // 400 - Requisição não atende requisitos
                case LivroRegistroNuloException:
                case LivroAutorNuloException:
                case LivroTituloNuloException:
                    return BadRequest(Json(e.Message));
                default:
                    return StatusCode(500, Json(e.Message));
            }
        }
    }
}