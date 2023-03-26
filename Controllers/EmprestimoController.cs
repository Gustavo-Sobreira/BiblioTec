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
    private readonly AlunoService _alunoService;
    private readonly LivroService _livroService;

    public EmprestimoController(BibliotecContext context)
    {
        _emprestimoDao = new EmprestimoDao(context);
        _alunoService = new AlunoService(context);
        _livroService = new LivroService(context);
    }
    

    [HttpPost("empretimo")]
    public ActionResult RealizarEmprestimo([FromForm] int registro, int matricula)
    {
        var aluno = _alunoService.VerificarMatricula(matricula);
        if (!aluno)
        {
            return BadRequest(AlunoErro.Erro001);
        }
        
        var livro = _livroService.VerificarRegistro(registro);
        if (!livro)
        {
            return BadRequest(LivroErro.Erro041);
        }

        var alunoPendente = _alunoService.VerificarPendenciaAluno(matricula);
        if (alunoPendente)
        {
            return BadRequest(EmprestimoErro.Erro071);
        }
        
        var livroPendente = _livroService.VerificarPendenciaLivro(registro);
        if (livroPendente)
        {
            return BadRequest(EmprestimoErro.Erro070);
        }
        
        _livroService.Emprestar(registro,matricula);
        return Ok(OperacaoConcluida.Sucesso004);
    }
    
    
    [HttpDelete("devolucao")]
    public ActionResult RealizarDevolucaoDeUmLivro([FromForm] int registro, int matricula)
    {
        var aluno = _alunoService.VerificarMatricula(matricula);
        if (!aluno)
        {
            return BadRequest(AlunoErro.Erro001);
        }
        
        var livro = _livroService.VerificarRegistro(registro);
        if (!livro)
        {
            return BadRequest(LivroErro.Erro041);
        }

        var alunoPendente = _alunoService.VerificarPendenciaAluno(matricula);
        if (!alunoPendente)
        {
            return BadRequest(EmprestimoErro.Erro072);
        }

        var livroPendente = _livroService.VerificarPendenciaLivro(registro);
        if (!livroPendente)
        {
            return BadRequest(EmprestimoErro.Erro073);
        }

        var devolucaoCorreta = _emprestimoDao.BuscarPorRegistro(registro);
        if (devolucaoCorreta!.Matricula != matricula)
        {
            return BadRequest(EmprestimoErro.Erro074);
        }

        try
        {
            _emprestimoDao.Apagar(matricula, registro);
            return Ok(OperacaoConcluida.Sucesso005);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}


