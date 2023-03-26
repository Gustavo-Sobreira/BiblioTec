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
        var livroExiste = _livroService.VerificarRegistro(livro.Registro);
        if (livroExiste)
        {
            return BadRequest(LivroErro.Erro042);
        }

        livro.Autor = _livroService.FormatarTextos(livro.Autor!);
        livro.Titulo = _livroService.FormatarTextos(livro.Titulo!);

        var livroValido = _livroService.VerificarCampos(livro);
        if (livroValido != "")
        {
            return BadRequest(livroValido);
        }
        
        return Ok(_livroService.Cadastrar(livro));
    }

    [HttpPut]
    [Route("editar")]
    public ActionResult EditarLivroExistente([FromForm] Livro livro)
    {
        var livroRegistrado = _livroService.VerificarRegistro(livro.Registro);
        if (!livroRegistrado)
        {
            return BadRequest(LivroErro.Erro041);
        }
        
        livro.Autor = _livroService.FormatarTextos(livro.Autor!);
        livro.Titulo = _livroService.FormatarTextos(livro.Titulo!);
        
        var livroValido = _livroService.VerificarCampos(livro);
        if (livroValido != "")
        {
            return BadRequest(livroValido);
        }

        var livroPendente = _livroService.VerificarPendenciaLivro(livro.Registro);
        if (livroPendente)
        {
            return BadRequest(EmprestimoErro.Erro073);
        }
        
        return Ok(_livroService.Editar(livro));
    }

    [HttpDelete("apagar")]
    public ActionResult<string> RemoverLivroDaBiblioteca([FromForm] Livro livro)
    {
        var livroEncontrado = _livroService.VerificarRegistro(livro.Registro);
        if (!livroEncontrado)
        {
            return BadRequest(LivroErro.Erro041);
        }
        
        livro.Autor = _livroService.FormatarTextos(livro.Autor);
        livro.Titulo = _livroService.FormatarTextos(livro.Titulo);
        
        var livroValido = _livroService.VerificarCampos(livro);
        if (livroValido != "")
        {
            return BadRequest(livroValido);
        }

        var livroPendente = _livroService.VerificarPendenciaLivro(livro.Registro);
        if (livroPendente)
        {
            return BadRequest(EmprestimoErro.Erro073);
        }

        var livrosIguais = _livroService.CompararCampos(livro);
        if (livrosIguais != "")
        {
            return BadRequest(livrosIguais);
        }
        
        return Ok(_livroService.Apagar(livro.Registro));
    }
}
