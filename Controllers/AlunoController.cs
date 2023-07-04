using BackBiblioteca.Data;
using BackBiblioteca.Errors;
using BackBiblioteca.Models;
using BackBiblioteca.Respostas;
using BackBiblioteca.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackBiblioteca.Controllers;

[ApiController]
[Route("[controller]")]
public class AlunoController : Controller
{
    private readonly AlunoService _alunoAtual;

    public AlunoController(BibliotecContext context)
    {
        _alunoAtual = new AlunoService(context);
    }
    
    [HttpGet]
    [Route("buscar/matricula")]
    public ActionResult ProcurarAluno([FromQuery] string matricula)
    {
        if (!_alunoAtual.VerificarMatriculaExiste(matricula))
        {
            return NotFound(Json(ErrorMensage.AlunoMatriculaNaoEncontrada));
        }
        try
        {
            return Ok(Json(_alunoAtual.BuscarAlunoPorMatricula(matricula)));
        }
        catch (Exception e)
        {
            return BadRequest(Json(e));
        }
    }

    [HttpPost]
    [Route("cadastro")]
    public ActionResult CadastrarNovoAluno([FromBody] Aluno aluno)
    {
        try
        {
            Aluno alunoFormatado = _alunoAtual.FormatarCampos(aluno);
            _alunoAtual.RegrasParaCadastro(alunoFormatado);
            _alunoAtual.Cadastrar(alunoFormatado);
            return Ok(alunoFormatado);
        }
        catch (Exception e)
        {
            return HandleException(e,e.Message);
        }
    }

    [HttpPut]
    [Route("editar")]
    public ActionResult EditarAlunoExistente([FromForm] Aluno aluno)
    {
        try
        {
            _alunoAtual.RegrasParaEdicao(aluno);
            return Ok(Json(_alunoAtual.Editar(aluno)));
        }
        catch (Exception e)
        {
            switch (e)
            { 
                case AlunoMatriculaNaoEncontradaException:
                    return NotFound(Json(e.Message ));
                case AlunoPendenteException:
                    return StatusCode(403,Json(e.Message));
                case AlunoMatriculaInvalidaException:
                case AlunoNomeInvalidoException:
                case AlunoSalaNuloException:
                case AlunoTurnoIncorretoException:
                    return BadRequest(Json(e.Message));
                
                default:
                    return StatusCode(500, Json($"Houve um erro interno: {e.Message}"));
            }
        }
    }

    [HttpDelete]
    [Route("apagar")]
    public ActionResult RemoverAlunoDosRegistros([FromBody] string matricula)
    {
        try
        {
            var aluno = _alunoAtual.BuscarAlunoPorMatricula(matricula);
            if(aluno == null){
                throw new AlunoMatriculaNaoEncontradaException();
            }
            
            _alunoAtual.RegrasParaEdicao(aluno);
            return Ok(Json(_alunoAtual.Apagar(aluno.Matricula!)));
        }
        catch (Exception e)
        {
            switch (e)
            {
                case AlunoMatriculaNaoEncontradaException:
                    return NotFound(Json(e.Message));
                case AlunoMatriculaInvalidaException:
                case AlunoNomeInvalidoException:
                case AlunoSalaNuloException:
                case AlunoTurnoIncorretoException:
                    return BadRequest(Json(e.Message));
                case AlunoPendenteException:
                    return StatusCode(403,Json(e.Message));
                case AlunoNomeIncompativelException:
                case AlunoSalaIncompativelException:
                case AlunoTurnoIncompativelException:
                    return StatusCode(412, Json(e.Message));
                default:
                    return StatusCode(500, Json(e.Message));
            }
        }
    }

    private ActionResult HandleException(Exception e, string errorMessage)
{
    if (e is AlunoMatriculaExistenteException)
    {
        return Conflict(Json(e.Message));
    }
    else if (e is AlunoMatriculaInvalidaException ||
            e is AlunoNomeInvalidoException ||
            e is AlunoSalaNuloException ||
            e is AlunoTurnoIncorretoException)
    {
        return BadRequest(Json(e.Message));
    }
    else if (e is AlunoMatriculaNaoEncontradaException)
    {
        return NotFound(Json(e.Message));
    }
    else if (e is AlunoPendenteException)
    {
        return StatusCode(403, Json(e.Message));
    }
    else
    {
        return StatusCode(500, Json($"{errorMessage}: {e.Message}"));
    }
}
}