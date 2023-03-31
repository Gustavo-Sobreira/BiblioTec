using BackBiblioteca.Data;
using BackBiblioteca.Models;
using BackBiblioteca.Respostas;
using BackBiblioteca.Services;
using BackBiblioteca.Errors;
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
    public ActionResult ProcurarAluno([FromQuery] int matricula)
    {
        if (!_alunoAtual.VerificarMatriculaExiste(matricula))
        {
            return NotFound(ErrorMensage.AlunoMatriculaNaoEncontrada);
        }
        try
        {
            return Ok(_alunoAtual.BuscarAlunoPorMatricula(matricula));
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost]
    [Route("cadastrar")]
    public ActionResult CadastrarNovoAluno([FromForm] Aluno aluno)
    {
        try
        {
            _alunoAtual.RegrasParaCadastro(aluno);
            return Ok(_alunoAtual.Cadastrar(aluno));
        }
        catch (Exception e)
        {
            switch (e)
            {
                case AlunoMatriculaExistenteException:
                    return Conflict(e.Message);
                case AlunoMatriculaInvalidaException:
                case AlunoNomeInvalidoException:
                case AlunoSalaNuloException:
                case AlunoTurnoIncorretoException:
                    return BadRequest(e.Message);
                default:
                    return StatusCode(500, $"Houve um erro interno não identificado: {e.Message}");
            }
        }
    }

    [HttpPut]
    [Route("editar")]
    public ActionResult EditarAlunoExistente([FromForm] Aluno aluno)
    {
        try
        {
            _alunoAtual.RegrasParaEdicao(aluno);
            return Ok(_alunoAtual.Editar(aluno));
        }
        catch (Exception e)
        {
            switch (e)
            { 
                case AlunoMatriculaNaoEncontradaException:
                    return NotFound(e.Message);
                     
                case AlunoPendenteException:
                    return StatusCode(403,e.Message);
                
                case AlunoMatriculaInvalidaException:
                case AlunoNomeInvalidoException:
                case AlunoSalaNuloException:
                case AlunoTurnoIncorretoException:
                    return BadRequest(e.Message);
                
                default:
                    return StatusCode(500, $"Houve um erro interno: {e.Message}");
            }
        }
    }

    [HttpDelete]
    [Route("apagar")]
    public ActionResult RemoverAlunoDosRegistros([FromForm] Aluno aluno)
    {
        try
        {
            _alunoAtual.RegrasParaEdicao(aluno);
            _alunoAtual.CompararDadosDeAluno(aluno);
            return Ok(_alunoAtual.Apagar(aluno.Matricula));
        }
        catch (Exception e)
        {
            switch (e)
            {
                case AlunoMatriculaNaoEncontradaException:
                    return NotFound(e.Message);
                case AlunoMatriculaInvalidaException:
                case AlunoNomeInvalidoException:
                case AlunoSalaNuloException:
                case AlunoTurnoIncorretoException:
                    return BadRequest(e.Message);
                case AlunoPendenteException:
                    return StatusCode(403,e.Message);
                case AlunoNomeIncompativelException:
                case AlunoSalaIncompativelException:
                case AlunoTurnoIncompativelException:
                    return StatusCode(412, e.Message);
                default:
                    return StatusCode(500, $"Houve um erro interno: {e.Message}");
            }
        }
    }
}

