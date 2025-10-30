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
        int id = int.Parse(HttpContext.Session.GetString("id"));
        string direccion = "Index";
            Usuario Usuario = BD.GetUsuario(id);
            List <Desafio> desafios = BD.getListaDesafios(id);
            ViewBag.nombre = Usuario.username;
            ViewBag.racha = Usuario.racha;
            ViewBag.desafio = desafios;
       
        return View(direccion);
    }
    public IActionResult Pasar(string direccion)
    {
        int id = int.Parse(HttpContext.Session.GetString("id"));
        string direccion = "index";
        if(id != null)
        {
            direccion = direccion;
        }
        return RedirectToAction(direccion);
    }
    
     public IActionResult Playlist()
    {
        int id = int.Parse(HttpContext.Session.GetString("id"));

        List<Playlist> playlists = BD.getListaPlaylists(id);
        List <Tag> hayQuePedirloAlaBaseDeDatos2 = new List <Tag>();
        ViewBag.Playlist = playlists;
        ViewBag.Tags = hayQuePedirloAlaBaseDeDatos2;
        return View("Playlist");
    }

    
    public IActionResult PlaylistFrecuencias(int idPlaylist)
    {
        Frecuencia Frecuencias = BD.getListaFrecuencias(idPlaylist);
        ViewBag.Frecuencias = Frecuencias;
        return View("Playlist-Frecuencias");
    }

    public IActionResult Tienda_Catalogo()
    {
        List <AppOcio> AppOcio = BD.get
        ViewBag.ApsDeOcio = hayQuePedirloAlaBaseDeDatos1;
        return View("Tienda-Catalogo");
    }
    

}
