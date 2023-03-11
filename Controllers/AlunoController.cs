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
        // ValidacaoCadastroService validando = new ValidacaoCadastroService(_context);
        return _alunoAtual.CadastrarAluno(novoAluno);
    }

    [HttpDelete]
    [Route("apagar")]
    public string RemoverAlunoDosRegistros([FromForm] Aluno alunoParaSerApagado)
    {
        return _alunoAtual.ApagarAluno(alunoParaSerApagado);
    }
}
