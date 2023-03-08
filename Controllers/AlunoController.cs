using BackBiblioteca.Data;
using BackBiblioteca.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackBiblioteca.Controllers;

[ApiController]
[Route("[controller]")]
public class AlunoController : Controller
{
    private BibliotecContext _context;

    public AlunoController(BibliotecContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    [Route("/CadastroAluno")]
    public void CadastrarNovoAluno([FromForm] int matricula, string nome, int sala, string turno)
    {
        ValidacaoCadastroService validando = new ValidacaoCadastroService(_context);
        var a = validando.CadastrarAluno(matricula, nome, sala, turno);

        Console.WriteLine(a);
    }
}
