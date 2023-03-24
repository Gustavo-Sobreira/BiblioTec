using BackBiblioteca.Data;
using BackBiblioteca.Models;
using BackBiblioteca.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackBiblioteca.Controllers;

[ApiController]
[Route("[controller]")]
public class LivroController : Controller
{
    private readonly LivroServices _livroAtual;
    public LivroController(BibliotecContext context)
    {
        _livroAtual = new LivroServices(context);
    }
    
    [HttpGet("estoque")]
    public ActionResult<IEnumerable<string>> ListarTodosLivrosEmEstoque()
    {
        return Ok(_livroAtual.ListarEstoque());
    }
    
    [HttpPost("cadastro")]
    public ActionResult<string> CadastrarNovoLivro([FromForm] Livro novoLivro)
    {
        return Ok(_livroAtual.Cadastrar(novoLivro));
    }

    [HttpPut]
    [Route("editar")]
    public string EditarLivroExistente([FromForm] Livro livroEtitado)
    {
        return _livroAtual.Editar(livroEtitado);
    }

    [HttpDelete("apagar")]
    public ActionResult<string> RemoverLivroDaBiblioteca([FromForm] Livro livroParaApagar)
    {
        return Ok(_livroAtual.Apagar(livroParaApagar));
    }
}
