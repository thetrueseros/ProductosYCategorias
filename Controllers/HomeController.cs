using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductosYCategorias.Models;

namespace ProductosYCategorias.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int? statusCode = null)
    {
        var code = statusCode ?? StatusCodes.Status500InternalServerError;
        Response.StatusCode = code;

        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            StatusCode = code,
            Message = code == StatusCodes.Status404NotFound
                ? "La página que buscas no existe."
                : "Ha ocurrido un error inesperado. Inténtalo de nuevo."
        });
    }
}
