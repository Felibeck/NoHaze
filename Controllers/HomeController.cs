using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NoHaze.Models;

namespace NoHaze.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }   

    public IActionResult Home()
    {
        Usuario hayQuePedirloAlaBaseDeDatos = new Usuario();
        List <Desafio> hayQuePedirloAlaBaseDeDatos2 = new List <Desafio>();
        ViewBag.nombre = hayQuePedirloAlaBaseDeDatos.username;
        ViewBag.racha = hayQuePedirloAlaBaseDeDatos.racha;
        ViewBag.desafio = hayQuePedirloAlaBaseDeDatos2;
        return View("Home");
    }
     public IActionResult Playlist()
    {
        Playlist hayQuePedirloAlaBaseDeDatos = new Playlist();
        List <Tag> hayQuePedirloAlaBaseDeDatos2 = new List <Tag>();
        ViewBag.Playlist = hayQuePedirloAlaBaseDeDatos;
        ViewBag.Tags = hayQuePedirloAlaBaseDeDatos2;
        return View("Playlist-Frecuencias");
    }

    public IActionResult Playlist_Catalogo()
    {
        List <AppOcio> hayQuePedirloAlaBaseDeDatos2 = new List <AppOcio>();
        ViewBag.ApsDeOcio = hayQuePedirloAlaBaseDeDatos2;
        return View("AppsDeOcio-Catalogo");
    }

    public IActionResult Tienda_Catalogo()
    {
        List <AppOcio> hayQuePedirloAlaBaseDeDatos1 = new List <AppOcio>();
        ViewBag.ApsDeOcio = hayQuePedirloAlaBaseDeDatos1;
        return View("Tienda-Catalogo");
    }
    

}
