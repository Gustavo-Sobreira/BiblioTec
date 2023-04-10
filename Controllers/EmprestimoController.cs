using BackBiblioteca.Data;
using BackBiblioteca.Errors;
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
    public ActionResult RealizarEmprestimo([FromQuery] int registro, [FromQuery] int matricula)
    {
        try
        {
            _emprestimoService.RegrasParaEmprestar(registro,matricula);
            return Ok(Json(_emprestimoService.Emprestar(registro, matricula)));
        }
        catch (Exception e)
        {
            switch (e)
            { 
                case AlunoMatriculaNaoEncontradaException:
                case LivroRegistroNaoEncontradoException:
                    return NotFound(Json(e.Message ));
                case AlunoPendenteException:
                case LivroPendenteException:
                    return StatusCode(403,Json(e.Message));
                default:
                    return StatusCode(500, Json(e.Message));
            }
        }
    }
    
    [HttpDelete("devolver")]
    public ActionResult RealizarDevolucaoDeUmLivro([FromQuery] int registro, [FromQuery] int matricula)
    {
        try
        {
            _emprestimoService.RegrasParaDevolver(registro,matricula);
            return Ok(Json(_emprestimoService.Devolver(registro)));
        }
        catch (Exception e)
        {
            return BadRequest(Json(e.Message));
        }
    }

    [HttpGet("pendentes")]
    public ActionResult ListarTodosPendentes([FromQuery] int sala, int turno, int dias)
    {
        return Ok(Json(_emprestimoService.ListarPendentes( sala, turno, dias)));
    }
}


