using BackBiblioteca.Data;
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

    [HttpPost]
    [Route("/Cadastro")]
    public void CadastrarNovoLivro([FromForm] int codigo, string nome, string autor, int prazo)
    {
        ValidacaoCadastroService validando = new ValidacaoCadastroService(_context);
        var a = validando.CadastrarLivro(codigo,nome,autor,prazo);

        Console.WriteLine(a);
    }

    [HttpPost]
    [Route("/Empretimo")]
    public void RealizarEmprestimo([FromForm] int codigo, int matricula)
    {
        EmprestimoLivroService emprestimo = new EmprestimoLivroService(_context);
        emprestimo.ValidarIntegrantes(codigo, matricula);

    }

    [HttpDelete]
    [Route("/Devolucao")]
    public void RealizarDevolucao([FromForm] int matricula , int codigo)
    {
        EmprestimoLivroService emprestimo = new EmprestimoLivroService(_context);
        emprestimo.DevolverLivro(matricula, codigo);
    }
}
