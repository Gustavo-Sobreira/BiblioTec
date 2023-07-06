using BackBiblioteca.Data;
using BackBiblioteca.Models;
using BackBiblioteca.Services;
using BackBiblioteca.Services.DTO;
using Microsoft.AspNetCore.Mvc;
using static BackBiblioteca.Errors.Aluno.MatriculaErros.LivroTituloNaoEncontradoException;
using static BackBiblioteca.Errors.Aluno.PendenciaErros.LivroTituloNaoEncontradoException;
using static BackBiblioteca.Errors.Livro.PendenteErros;
using static BackBiblioteca.Errors.Livro.RegistroErros;

namespace BackBiblioteca.Controllers;

[ApiController]
[Route("emprestimo")]
public class EmprestimoController : Controller
{
    private readonly EmprestimoService _emprestimoService;

    public EmprestimoController(BibliotecContext context)
    {
        _emprestimoService = new EmprestimoService(context);
    }
    

    [HttpPost("novo")]
    public ActionResult RealizarEmprestimo([FromBody] EmprestimoDTO dadosEmprestimos)
    {
        try
        {
            _emprestimoService.RegrasParaEmprestar(dadosEmprestimos.Registro!, dadosEmprestimos.Matricula!);
            _emprestimoService.Emprestar(dadosEmprestimos.Registro!, dadosEmprestimos.Matricula!);
            return Ok();
        }
        catch (Exception e)
        {
            return HandleException(e,e.Message);
        }
    }
    
    [HttpDelete("devolver")]
    public ActionResult RealizarDevolucaoDeUmLivro([FromBody] EmprestimoDTO dadosEmprestimos)
    {
        try
        {
            _emprestimoService.RegrasParaDevolver(dadosEmprestimos.Registro!, dadosEmprestimos.Matricula!);
            _emprestimoService.Devolver(dadosEmprestimos.Registro!);
            return Ok();
        }
        catch (Exception e)
        {
            return HandleException(e,e.Message);
        }
    }

    [HttpGet("pendentes/{tempo}/{skip}/{take}")]
    public ActionResult ListarPendentes(int tempo, int skip, int take)
    {
        try
        {
            List<Emprestimo> pendentes = _emprestimoService.ListarPendentes(tempo, skip, take);
            return Ok(pendentes);            
        }
        catch (Exception e)
        {
            return HandleException(e,e.Message);
        }
    }

    private ActionResult HandleException(Exception e, string errorMessage)
    {
        switch (e)
            { 
                case AlunoMatriculaNaoEncontradaException:
                case LivroRegistroNaoEncontradoException:
                    return NotFound(e.Message );
                case AlunoPendenteException:
                case LivroPendenteException:
                    return StatusCode(403, e.Message);
                default:
                    return StatusCode(500, e.Message);
            }
    }
}