using BackBiblioteca.Data;
using BackBiblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using BackBiblioteca.Services;

namespace BackBiblioteca.Controllers;

[ApiController]
[Route("[controller]")]
public class LivroController : Controller
{
    private LivroServices _livroAtual;
    public LivroController(BibliotecContext context)
    {
        _livroAtual = new LivroServices(context);
    }
    
    [HttpGet]
    [Route("estoque")]
    public List<string> ListarEstoque()
    {
        return _livroAtual.ListarEstoque();
    }
    
    [HttpPost]
    [Route("cadastro")]
    public string CadastrarNovoLivro([FromForm] Livro novoLivro)
    {
        return _livroAtual.CadastrarLivro(novoLivro);
    }

    [HttpPost]
    [Route("empretimo")]
    public string RealizarEmprestimo([FromForm] int registro, int matricula)
    {
        return _livroAtual.EmprestarLivro(registro, matricula);
    }

    [HttpDelete]
    [Route("devolucao")]
    public string RealizarDevolucao([FromForm] int registro, int matricula)
    {
        return _livroAtual.DevolverLivro(registro, matricula);
    }

    [HttpDelete]
    [Route("apagar")]
    public string RemoverLivroDaBiblioteca([FromForm] Livro livroParaApagar)
    {
        return _livroAtual.ApagarLivro(livroParaApagar);
    }
}
