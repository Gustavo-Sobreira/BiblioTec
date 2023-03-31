using BackBiblioteca.Data;
using BackBiblioteca.Models;
using BackBiblioteca.Respostas;
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
    public ActionResult<IEnumerable<string>> ListarTodosLivrosEmEstoque()
    {
        return Ok(_livroService.ListarEstoque());
    }

    [HttpPost("cadastro")]
    public ActionResult CadastrarNovoLivro([FromForm] Livro livro)
    {
        try
        {
            _livroService.RegrasParaCadastrar(livro);
            return Ok(_livroService.Cadastrar(livro));
        }
        catch (Exception e)
        {
            // 400 - Requisição não atende requisitos
            if ((e.Message == ErrorMensage.LivroRegistroNulo) ||
                (e.Message == ErrorMensage.LivroAutorNulo) ||
                (e.Message == ErrorMensage.LivroTituloNulo))
            {
                return BadRequest(e.Message);
            }
            
            // 409 - Conflito de ID
            if (e.Message == ErrorMensage.LivroRegistroExistente)
            {
                return Conflict(e.Message);
            }
            return StatusCode(500, $"Houve um erro interno não identificado: {e.Message}");
        }
    }

    [HttpPut]
    [Route("editar")]
    public ActionResult EditarLivroExistente([FromForm] Livro livro)
    {
        try
        {
           _livroService.RegrasParaEditar(livro);
           return Ok(_livroService.Editar(livro));

        }
        catch (Exception e)
        {
            // 400 - Requisição não atende requisitos
            if ((e.Message == ErrorMensage.LivroRegistroNulo) ||
                (e.Message == ErrorMensage.LivroAutorNulo) ||
                (e.Message == ErrorMensage.LivroTituloNulo))
            {
                return BadRequest(e.Message);
            }
            
            // 404 - ID não encontrado
            if (e.Message == ErrorMensage.LivroRegistroNaoEncontrado)
            {
                return Conflict(e.Message);
            }
            
            // 403 - Permissão negada
            if (e.Message == ErrorMensage.LivroPendente)
            {
                return Forbid(e.Message);
            }
            
            // 412 - Incompatibilidade de dados
            if ((e.Message == ErrorMensage.LivroTituloIncompativel) ||
                (e.Message == ErrorMensage.LivroAutorIncompativel))
            {
                return StatusCode(412, e.Message);
            }
            return StatusCode(500, $"Houve um erro interno não identificado: {e.Message}");
        }
        
    }

    [HttpDelete("apagar")]
    public ActionResult<string> RemoverLivroDaBiblioteca([FromForm] Livro livro)
    {
        try
        {
            _livroService.RegrasParaEditar(livro);
            _livroService.CompararCampos(livro);
            return Ok(_livroService.Apagar(livro.Registro));
        }
        catch (Exception e)
        {
            // 400 - Requisição não atende requisitos
            if ((e.Message == ErrorMensage.LivroRegistroNulo) ||
                (e.Message == ErrorMensage.LivroAutorNulo) ||
                (e.Message == ErrorMensage.LivroTituloNulo))
            {
                return BadRequest(e.Message);
            }

            // 404 - ID não encontrado
            if (e.Message == ErrorMensage.LivroRegistroNaoEncontrado)
            {
                return Conflict(e.Message);
            }

            // 403 - Permissão negada
            if (e.Message == ErrorMensage.LivroPendente)
            {
                return Forbid(e.Message);
            }

            return StatusCode(500, $"Houve um erro interno não identificado: {e.Message}");
        }
    }
}
    