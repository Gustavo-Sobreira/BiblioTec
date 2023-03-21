using BackBiblioteca.Data;
using BackBiblioteca.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackBiblioteca.Controllers;

[ApiController]
[Route("[controller]")]

public class EmprestimoController
{
    private readonly EmprestimoServices _emprestimoAtual;
    public EmprestimoController(BibliotecContext context)
    {
        _emprestimoAtual = new EmprestimoServices(context);
    }
    

    [HttpPost("empretimo")]
    public ActionResult<string> RealizarEmprestimo([FromForm] int registro, int matricula)
    {
        return _emprestimoAtual.EmprestarLivro(registro, matricula);
    }

    // private ActionResult<string> Ok(string emprestarLivro)
    // {
    //     throw new NotImplementedException();
    // }

    [HttpDelete("devolucao")]
    public ActionResult<string> RealizarDevolucaoDeUmLivro([FromForm] int registro, int matricula)
    {
        return _emprestimoAtual.DevolverLivro(registro, matricula);
    }
    
}


