using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BiblioTec.Controllers;

public class CadastrarLivroController : Controller
{
    public IActionResult CadastrarLivros()
    {
        return View();
    }
}