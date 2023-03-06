using BiblioTec.Data;
using BiblioTec.Models.Livro;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTec.Controllers;

public class HomeController : Controller
{
    private BibliotecContext _context;

    public HomeController(BibliotecContext context)
    {
        _context = context;
    }
    public IActionResult Index()
     {
         return View();
     }

    public IActionResult ListarLivros()
    {
        return View();
    }
    public IQueryable<Livro> ListarLivros([FromQuery] int skip = 0, int take = 20)
    {
        return _context.Livros.Skip(skip).Take(take);
    }

    public void CriarLivro()
    {
        Livro novo_livro = new Livro
        {
            Codigo = "02",Nome = "Vamuuuuu"
        };
        Livro novoLivro = new Livro
        {
            Codigo = novo_livro.Codigo,
            Nome = novo_livro.Nome
        };
        _context.Add(novoLivro);
        _context.SaveChanges();
    }
}