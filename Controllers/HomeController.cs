using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NoHaze.Models;

namespace NoHaze.Controllers;

public class HomeController : Controller
{
    

    private readonly IWebHostEnvironment _env;

    public HomeController(IWebHostEnvironment env)
    {
        _env = env;
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
            List<int> MinutosProductivos = BD.getHorasProductivas(id, 1);
            ViewBag.Minutos = MinutosProductivos[0];
            ViewBag.nombre = Usuario.username;
            ViewBag.racha = Usuario.racha;
            ViewBag.desafio = desafios;

        return View();
    }
    public IActionResult Pasar(string Direccion)
    {
        
        string direccion = Direccion;
        string id = HttpContext.Session.GetString("ID");
        int ID = int.Parse(id);
        ViewBag.id = ID;



        if (string.IsNullOrEmpty(id))
        {
            direccion = "Index";
        }
        else
        {
            Usuario user = BD.GetUsuario(ID);
            ViewBag.cantMonedas = user.monedas;

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

    // Asumimos: informes[0] = hoy, informes[1] = ayer, ...
// Queremos un array en orden Lunes(0) .. Domingo(6)
List<int> horasSemanales = new List<int>(new int[7]);

for (int i = 0; i < 7; i++)
{
    // día de la semana del elemento informes[i]
    int dow = ((int)DateTime.Now.DayOfWeek - i + 7) % 7; // 0=Dom,1=Lun,...6=Sáb
    int indexMondayFirst = (dow + 6) % 7; // convierte a 0=Lun ... 6=Dom
    horasSemanales[indexMondayFirst] = informes[i];
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
        int id = int.Parse(HttpContext.Session.GetString("ID"));
        List<AppOcio> appsOcio = BD.getListaAppsOcio(id);
        ViewBag.listaAppsOcio = appsOcio;
        return View();
    }

    public IActionResult Tienda_Compra()
    {
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

    // para agregar la app de ocio

    public int agregarAppOcio(int id)
    {
        int ID = int.Parse(HttpContext.Session.GetString("ID"));
        BD.agregarAppOcio(ID, id);
        return 1;
    }

    //Completar una mision
    public int CompletarMision(int id, int recompensa)
    {
        int ID = int.Parse(HttpContext.Session.GetString("ID"));
        BD.agregarRecompensas(ID,recompensa);
        BD.CompletarMision(id);
        return 1;
    }

    // aceptar cambios en editar perfil

    [HttpPost] 

    public IActionResult aceptarCambiosPerfil(Usuario user, IFormFile foto)
    {

        if (foto != null && foto.Length > 0){

            string nombreArchivo = foto.FileName;

            string rutaCarpeta = Path.Combine(_env.WebRootPath, "Imagenes");

            string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                foto.CopyTo(stream);
            }

            user.foto = (nombreArchivo);

        }

            int id = int.Parse(HttpContext.Session.GetString("ID"));

            BD.actualizarPerfil(user);

            // Recargar los datos del usuario

            ViewBag.Usuario = BD.GetUsuario(id);

    return View("Perfil");
    }
    
    public IActionResult Pomodoro_Timer()
    {
        return View();
    }
    
    public int AgregarRegistro(int Tiempo)
    {
        int id = int.Parse(HttpContext.Session.GetString("ID"));
        DateTime Hoy = DateTime.Today;
        BD.AgregarRegistro(id, Tiempo, Hoy);
        return 1;
    }
}
