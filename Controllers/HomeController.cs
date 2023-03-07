using BiblioTec.Data;
using BiblioTec.Models.Livro;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTec.Controllers;

public class HomeController : Controller
{
    private TabelaLivroContext _context;

    public HomeController(TabelaLivroContext context)
    {
        _context = context;
    }
    public IActionResult Index()
     {
         return View();
     }
    
    public IQueryable<Livro> ListarLivros([FromQuery] int skip = 0, int take = 20)
    {
        return _context.livro.Skip(skip).Take(take);
    }
}