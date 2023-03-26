using BackBiblioteca.Data;
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
    
    [HttpPost]
    [Route("cadastro")]
    public ActionResult CadastrarNovoAluno([FromForm] Aluno aluno)
    {
        var matriculaExiste = _alunoAtual.VerificarMatricula(aluno.Matricula);
        if (matriculaExiste == true)
        {
            return BadRequest(AlunoErro.Erro002);
        }

        aluno.Nome = _alunoAtual.FormatarTextos(aluno.Nome!);
        aluno.Turno = _alunoAtual.FormatarTextos(aluno.Turno!);

        var campoValido = _alunoAtual.VerificarCampos(aluno);
        if (campoValido != "")
        {
            return BadRequest(campoValido);
        }

        var cadastroFuncionou = _alunoAtual.Cadastrar(aluno);
        return cadastroFuncionou == OperacaoConcluida.Sucesso001
            ? Ok(cadastroFuncionou)
            : BadRequest(cadastroFuncionou);
    }

    [HttpPut]
    [Route("editar")]
    public ActionResult EditarAlunoExistente([FromForm] Aluno aluno)
    {
        var matriculaExiste = _alunoAtual.VerificarMatricula(aluno.Matricula);
        if (!matriculaExiste)
        {
            return BadRequest(AlunoErro.Erro001);
        }

        aluno.Nome = _alunoAtual.FormatarTextos(aluno.Nome!);
        aluno.Turno = _alunoAtual.FormatarTextos(aluno.Turno!);

        var campoValido = _alunoAtual.VerificarCampos(aluno);
        if (campoValido != "")
        {
            return BadRequest(campoValido);
        }

        var alunoTemPendencia = _alunoAtual.VerificarPendenciaAluno(aluno.Matricula);
        if (alunoTemPendencia)
        {
            return BadRequest(EmprestimoErro.Erro071);
        }
        
        return Ok(_alunoAtual.Editar(aluno));
    }

    [HttpDelete]
    [Route("apagar")]
    public ActionResult RemoverAlunoDosRegistros([FromForm] Aluno aluno)
    {
        var matriculaExiste = _alunoAtual.VerificarMatricula(aluno.Matricula);
        if (!matriculaExiste)
        {
            return BadRequest(AlunoErro.Erro001);
        }

        aluno.Nome = _alunoAtual.FormatarTextos(aluno.Nome!);
        aluno.Turno = _alunoAtual.FormatarTextos(aluno.Turno!);

        var campoValido = _alunoAtual.VerificarCampos(aluno);
        if (campoValido != "")
        {
            return BadRequest(campoValido);
        }

        var alunoTemPendencia = _alunoAtual.VerificarPendenciaAluno(aluno.Matricula);
        if (alunoTemPendencia)
        {
            return BadRequest(EmprestimoErro.Erro071);
        }

        var confereDado = _alunoAtual.CompararDadosDeAluno(aluno);
        if (confereDado != "")
        {
            return BadRequest(confereDado);
        }
        return Ok(_alunoAtual.Apagar(aluno.Matricula));
    }
}
