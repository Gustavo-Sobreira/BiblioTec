using BackBiblioteca.Data;
using BackBiblioteca.Models;
using BackBiblioteca.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackBiblioteca.Controllers;

[ApiController]
[Route("[controller]")]
public class AlunoController : Controller
{
    private AlunoServices _alunoAtual;

    public AlunoController(BibliotecContext context)
    {
        _alunoAtual = new AlunoServices(context);
    }
    
    [HttpPost]
    [Route("cadastro")]
    public string CadastrarNovoAluno([FromForm] Aluno novoAluno)
    {   
        return _alunoAtual.Cadastrar(novoAluno);
    }

    [HttpPut]
    [Route("editar")]
    public string EditarAlunoExistente([FromForm] Aluno alunoEtitado)
    {
        return _alunoAtual.Editar(alunoEtitado);
    }

    [HttpDelete]
    [Route("apagar")]
    public string RemoverAlunoDosRegistros([FromForm] Aluno alunoParaSerApagado)
    {
        return _alunoAtual.Apagar(alunoParaSerApagado);
    }
}
