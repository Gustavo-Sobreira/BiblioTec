using BackBiblioteca.Data;
using BackBiblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using BackBiblioteca.Services;

namespace BackBiblioteca.Controllers;

[ApiController]
[Route("[controller]")]
public class LivroController : Controller
{
    private readonly BibliotecContext _context;

    public LivroController(BibliotecContext context)
    {
        _context = context;
    }

    
    [HttpGet]
    [Route("/Listar/TodosIguais")]
    public List<int> ListarLivrosIguais([FromHeader] string nome, string autor)
    {
        LivroServices novaConsulta = new LivroServices(_context);
        return novaConsulta.ListarLivrosIguais(nome,autor);
    }

    [HttpGet]
    [Route("/Listar/Emprestados")]
    public List<int> ListarLivrosIguaisEmprestados([FromHeader] string nome, string autor)
    {
        LivroServices novaConsulta = new LivroServices(_context);
        return novaConsulta.ListarLivrosIguaisEmprestados(nome, autor);
    }
    
    
    
    [HttpPost]
    [Route("/Cadastro")]
    public string CadastrarNovoLivro([FromForm] Livro novoLivro)
    {
        LivroServices validando = new LivroServices(_context);
        return validando.CadastrarLivro(novoLivro);
    }

    [HttpPost]
    [Route("/Empretimo")]
    public string RealizarEmprestimo([FromForm] int registro, int matricula)
    {
        LivroServices emprestimo = new LivroServices(_context);
        return emprestimo.EmprestarLivro(registro, matricula);
    }

    [HttpDelete]
    [Route("/Devolucao")]
    public string RealizarDevolucao([FromForm] int registro, int matricula)
    {
        LivroServices emprestimo = new LivroServices(_context);
        return emprestimo.DevolverLivro(registro, matricula);
    }
    
}
