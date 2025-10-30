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
        int id = int.Parse(HttpContext.Session.GetString("ID"));
        string direccion = "Index";
            Usuario Usuario = BD.GetUsuario(id);
            List <Desafio> desafios = BD.getListaDesafios(id);
            ViewBag.nombre = Usuario.username;
            ViewBag.racha = Usuario.racha;
            ViewBag.desafio = desafios;
       
        return View(direccion);
    }
    public IActionResult Pasar(string Direccion)
    {

        string direccion = Direccion;
        Console.WriteLine(Direccion);
        string id = HttpContext.Session.GetString("ID");
        if (string.IsNullOrEmpty(id))
        {
            direccion = "Index";
        }
        return RedirectToAction(direccion, "Home");
    }
    
     public IActionResult Playlist()
    {
        int id = int.Parse(HttpContext.Session.GetString("ID"));

        List<Playlist> playlists = BD.getListaPlaylists(id);
        List <Tag> hayQuePedirloAlaBaseDeDatos2 = new List <Tag>();
        ViewBag.Playlist = playlists;
        ViewBag.Tags = hayQuePedirloAlaBaseDeDatos2;
        return View();
    }

    
    public IActionResult Playlist_Frecuencias(int idPlaylist)
    {
        List<Frecuencia> Frecuencias = BD.getListaFrecuencias(idPlaylist);
        ViewBag.Frecuencias = Frecuencias;
        return View();
    }

    public IActionResult Tienda_Catalogo()
    {
        List <AppOcio> AppOcio = new List <AppOcio>();
        ViewBag.ApsDeOcio = AppOcio;
        return View();
    }

    public IActionResult AppsDeOcio_Catalogo()
    {
        int id = int.Parse(HttpContext.Session.GetString("ID"));

        ViewBag.listaAppsOcio = BD.getListaAppsOcio(id);

        return View();
    }
    

}
