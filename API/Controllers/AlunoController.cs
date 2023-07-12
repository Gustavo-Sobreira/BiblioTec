using BackBiblioteca.Data;
using BackBiblioteca.Models;
using BackBiblioteca.Services;
using Microsoft.AspNetCore.Mvc;
using static BackBiblioteca.Errors.Aluno.MatriculaErros.LivroTituloNaoEncontradoException;
using static BackBiblioteca.Errors.Aluno.NomeErros.AlunoNomeException;
using static BackBiblioteca.Errors.Aluno.PendenciaErros.LivroTituloNaoEncontradoException;
using static BackBiblioteca.Errors.Aluno.SalaErros.LivroTituloNaoEncontradoException;
using static BackBiblioteca.Errors.Aluno.TurnoErros.LivroTituloNaoEncontradoException;

namespace BackBiblioteca.Controllers;

[ApiController]
[Route("aluno")]
public class AlunoController : Controller
{
    private readonly AlunoService _alunoAtual;
    private readonly TextosService _formatarTextos;

    public AlunoController(BibliotecContext context)
    {
        _alunoAtual = new AlunoService(context);
        _formatarTextos = new TextosService();

    }
    
    [HttpGet]
    [Route("buscar/{matricula}")]
    public ActionResult ProcurarAlunoPelaMatricula(string matricula)
    {
        try
        {
            Aluno alunoEncotrado = _alunoAtual.BuscarAlunoPorMatricula(matricula)!;
            if(alunoEncotrado == null){
                throw new AlunoMatriculaNaoEncontradaException();
            }
            return StatusCode(200,alunoEncotrado);
        }
        catch (Exception e)
        {
            return HandleException(e,e.Message);
        }
    }

    [HttpGet("buscar/aluno/{nome}")]
    public ActionResult ProcurarAlunoPeloNome(string nome)
    {
        try
        {
            string nomeFormatado = _formatarTextos.FormatarTextos(nome);
            List<Aluno> alunoEncotrado = _alunoAtual.BuscarAlunoPeloNome(nomeFormatado)!;
            if(alunoEncotrado.Count == 0){
                throw new AlunoNomeNaoEncontradoException();
            }
            return StatusCode(200,alunoEncotrado);
        }
        catch (Exception e)
        {
            return HandleException(e,e.Message);
        }
    }


    [HttpGet]
    [Route("buscar/todos/{skip}/{take}")]
    public ActionResult BuscarTodosAlunos(int skip = 0, int take = 25){
        try
        {
            List<Aluno> listaGerada = _alunoAtual.BuscarTodosAlunos(skip, take);
            return StatusCode(200,listaGerada);
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
            return StatusCode(200,Json(alunoFormatado));
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

            Aluno dadosAntigos =_alunoAtual.BuscarAlunoPorMatricula(aluno
            .Matricula!)!;
            if(dadosAntigos == null){
                throw new AlunoMatriculaNaoEncontradaException();
            }

            _alunoAtual.RegrasParaEdicao(alunoFormatado);
            Aluno alunoEditado = _alunoAtual.Editar(alunoFormatado)!;

            List<Aluno> comparacao = new List<Aluno>{dadosAntigos,alunoEditado};
            return StatusCode(200,comparacao);
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
            Aluno? alunoApagado = _alunoAtual.Apagar(aluno.Matricula!);
            return StatusCode(200,alunoApagado);
        }
        catch (Exception e)
        {
            return HandleException(e,e.Message);
        }
    }

    private ActionResult HandleException(Exception e, string errorMessage)
    {
        if (e is AlunoMatriculaInvalidaException ||
                e is AlunoSalaNuloException ||
                e is AlunoTurnoIncorretoException)
        {
            return StatusCode(400,Json(e.Message));
        }
        else if (e is AlunoPendenteException)
        {
            return StatusCode(403,Json(e.Message));
        }
        else if (e is AlunoMatriculaNaoEncontradaException)
        {
            return StatusCode(404,Json(e.Message));
        }
        else if (e is AlunoMatriculaExistenteException)
        {
            return StatusCode(409,Json(e.Message));
        }
        else
        {
            return StatusCode(500, "[ ERRO ] - " + e.Message);
        }
    }
}