using BiblioTec.Data;
using BiblioTec.Models.Livro;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTec.Controllers;

public class LivroController : Controller
{
    private TabelaLivroContext _context;
    public LivroController(TabelaLivroContext context)
    {
        _context = context;
    }
    public IActionResult Cadastro()
    {
        return View();
    }
    public IActionResult CadastrarLivro([FromForm]int _codigo, string _nome, string _autor, int _prazo)
    {
        Livro livroParaAdicionar = new Livro()
        {
            codigo = _codigo,
            nome = _nome,
            autor = _autor,
            prazo = _prazo
        };
        try
        {
            _context.Add(livroParaAdicionar);
            _context.SaveChanges();
            ViewBag.Titulo = "Livro cadastrado com sucesso!";
            return View();
        }
        catch(Exception e)
        {
            ViewBag.Titulo = "!!! Houve um erro !!!";
            ViewBag.Erro = "Erros comuns:";
            ViewBag.Erro1 = "1° - Código do livro já existe. Troque o código e tente novamente";
            return View();
        }
    }
}