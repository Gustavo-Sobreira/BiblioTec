using BackBiblioteca.Data;
using BackBiblioteca.Errors;
using BackBiblioteca.Models;
using BackBiblioteca.Respostas;
using BackBiblioteca.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackBiblioteca.Controllers;

[ApiController]
[Route("aluno")]
public class AlunoController : Controller
{
    private readonly AlunoService _alunoAtual;

    public AlunoController(BibliotecContext context)
    {
        _alunoAtual = new AlunoService(context);
    }
    
    [HttpGet]
    [Route("buscar/{matricula}")]
    public ActionResult ProcurarAluno(string matricula)
    {
        try
        {
            Aluno alunoEncotrado = _alunoAtual.BuscarAlunoPorMatricula(matricula)!;
            return Ok(alunoEncotrado);
        }
        catch (Exception e)
        {
            return HandleException(e,e.Message);
        }
    }

    [HttpGet]
    [Route("buscar/todos/{skip}/{take}")]
    public ActionResult BuscarTodosAlunos(int skip, int take){
        try
        {
            List<Aluno> listaGerada = _alunoAtual.BuscarTodosAlunos(skip, take);
            return Ok(listaGerada);
        }
        catch (Exception e)
        {
            return HandleException(e,e.Message);
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
    public ActionResult EditarAlunoExistente([FromBody] Aluno aluno)
    {
        try
        {
            Aluno alunoFormatado = _alunoAtual.FormatarCampos(aluno);
            _alunoAtual.RegrasParaEdicao(alunoFormatado);
            
            Aluno dadosAntigos =_alunoAtual.BuscarAlunoPorMatricula(aluno
            .Matricula);

            Aluno alunoEditado = _alunoAtual.Editar(alunoFormatado);

            List<Aluno> comparacao = new List<Aluno>{dadosAntigos,alunoEditado};
            return Ok(comparacao);
        }
        catch (Exception e)
        {
            return HandleException(e,e.Message);
        }
    }

    [HttpDelete]
    [Route("apagar/{matricula}")]
    public ActionResult RemoverAlunoDosRegistros(string matricula)
    {
        try
        {
            var aluno = _alunoAtual.BuscarAlunoPorMatricula(matricula);
            if(aluno == null){
                throw new AlunoMatriculaNaoEncontradaException();
            }
            _alunoAtual.RegrasParaEdicao(aluno);
            return Ok(_alunoAtual.Apagar(aluno.Matricula!));
        }
        catch (Exception e)
        {
            return HandleException(e,e.Message);
        }
    }

    private ActionResult HandleException(Exception e, string errorMessage)
    {
        if (e is AlunoMatriculaExistenteException)
        {
            return StatusCode(409,e.Message);
        }
        else if (e is AlunoMatriculaInvalidaException ||
                e is AlunoNomeInvalidoException ||
                e is AlunoSalaNuloException ||
                e is AlunoTurnoIncorretoException)
        {
            return StatusCode(400,e.Message);
        }
        else if (e is AlunoMatriculaNaoEncontradaException)
        {
            return NotFound(e.Message);
        }
        else if (e is AlunoPendenteException)
        {
            return StatusCode(403, e.Message);
        }
        else
        {
            return StatusCode(500, e.Message);
        }
    }
}