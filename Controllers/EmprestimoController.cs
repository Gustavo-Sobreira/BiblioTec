using BackBiblioteca.Data;
using BackBiblioteca.Models;
using BackBiblioteca.Respostas;
using BackBiblioteca.Services;
using BackBiblioteca.Services.Dao;
using Microsoft.AspNetCore.Mvc;

namespace BackBiblioteca.Controllers;

[ApiController]
[Route("[controller]")]

public class EmprestimoController : Controller
{
    private readonly EmprestimoDao _emprestimoDao;
    private readonly EmprestimoService _emprestimoService;

    public EmprestimoController(BibliotecContext context)
    {
        _emprestimoDao = new EmprestimoDao(context);
        _emprestimoService = new EmprestimoService(context);
    }
    

    [HttpPost("emprestar")]
    public ActionResult RealizarEmprestimo([FromForm] int registro, int matricula)
    {
        try
        {
            _emprestimoService.RegrasParaEmprestar(registro,matricula);
            return Ok(_emprestimoService.Emprestar(registro, matricula));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    
    [HttpDelete("devolver")]
    public ActionResult RealizarDevolucaoDeUmLivro([FromForm] int registro, int matricula)
    {
        try
        {
            _emprestimoService.RegrasParaDevolver(registro,matricula);
            return Ok(_emprestimoService.Devolver(registro));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}


