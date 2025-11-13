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
        string id = HttpContext.Session.GetString("ID");
        if (!string.IsNullOrEmpty(id))
        {
        ViewBag.ID = int.Parse(id);
        }
        else{
        ViewBag.ID = 0;
        }
        return View();
    }   

    public IActionResult Home()
    {
        int id = int.Parse(HttpContext.Session.GetString("ID"));
            Usuario Usuario = BD.GetUsuario(id);
            List <Desafio> desafios = BD.getListaDesafios(id);
            ViewBag.nombre = Usuario.username;
            ViewBag.racha = Usuario.racha;
            ViewBag.desafio = desafios;

        return View();
    }
    public IActionResult Pasar(string Direccion)
    {
        
        string direccion = Direccion;
        string id = HttpContext.Session.GetString("ID");
        ViewBag.ID = int.Parse(id);
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
        ViewBag.Playlists = playlists;

      int i = 0;

        List<List<Tag>> listaDeListasTag = new List<List<Tag>>();
        foreach(Playlist item in playlists)
        {
            listaDeListasTag.Add(BD.getListaTags(item.id));
        }

        ViewBag.listaTags = listaDeListasTag    ;
    return View();
    }
    public IActionResult Estadisticas()
    {
        int id = int.Parse(HttpContext.Session.GetString("ID"));
        List<int>informes = BD.getHorasProductivas(id, 28);
        ViewBag.horaDiaria = informes[0];
        List<int> horasSemanales = new List<int>();
        for(int i=0; i < 7; i++)
        {
            horasSemanales.Add(informes[i]);
        }
        ViewBag.horasSemanales = horasSemanales;

        List<int>horasMensuales = new List<int>();

        for(int i = 0;i < 4; i++)
        {
            int acu = 0;
            for(int j = 0; j < 7; j++)
            {
                acu += informes[j + (i*7)];
            }
            horasMensuales.Add(acu/7);
        }
        
        ViewBag.horasMensuales = horasMensuales;

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
        public IActionResult AppsDeOcio_Selector()
    {
        int id = int.Parse(HttpContext.Session.GetString("ID"));

        ViewBag.listaAppsNoAsociadas = BD.getAppsNoAsociadas(id);

        return View();
    }
    public IActionResult Perfil()
    {
        int id = int.Parse(HttpContext.Session.GetString("ID"));
        ViewBag.Usuario = BD.GetUsuario(id);

        return View("Perfil");
    }
    public IActionResult Editar_Perfil()
    {
        int id = int.Parse(HttpContext.Session.GetString("ID"));
        ViewBag.Usuario = BD.GetUsuario(id);
        
        return View();
    }

    // para eliminar la app de ocio

    public int eliminarAppOcio(int Id)
    {
        BD.eliminarAppOcio(Id);
        return 1;
    }

    // aceptar cambios en editar perfil

    [HttpPost] 

    public IActionResult aceptarCambiosPerfil(string username, DateTime fechaNacimiento, string descripcion, string objetivo)
    {
        int id = int.Parse(HttpContext.Session.GetString("ID"));

        // Validar que la fecha sea válida para SQL Server
        if (fechaNacimiento.Year < 1753)
        {
            // Si la fecha es inválida, mantener la fecha actual del usuario
            // O puedes poner una fecha por defecto
            fechaNacimiento = new DateTime(2000, 1, 1);
        }

    BD.actualizarPerfil(username, fechaNacimiento, descripcion, objetivo, id);

    // Recargar los datos del usuario
    ViewBag.Usuario = BD.GetUsuario(id);

    return View("Perfil");
    }
    
    public IActionResult Pomodoro_Timer()
    {
        return View();
    }
    
}
