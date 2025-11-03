using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NoHaze.Models;

namespace NoHaze.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }


    public IActionResult Login()
    {
        string id = HttpContext.Session.GetString("ID");
        if (!string.IsNullOrEmpty(id))
        {
            ViewBag.ID = int.Parse(id);
        }
        else
        {
            ViewBag.ID = 0;
        }
        return View();
    }

    [HttpPost]

    public IActionResult guardarLogin(string username, string password)
    {
        if (username != null && password != null)
        {
            int IDusuario = BD.Login(username, password);

            if (IDusuario > 0)
            {
                HttpContext.Session.SetString("ID", IDusuario.ToString());
                return RedirectToAction("Pasar", "Home", new { Direccion = "Home" });
            }
        }
        return RedirectToAction("Login");


    }

    public IActionResult SignIn()
    {
        string id = HttpContext.Session.GetString("ID");
        if (!string.IsNullOrEmpty(id))
        {
            ViewBag.ID = int.Parse(id);
        }
        else
        {
            ViewBag.ID = 0;
        }
        return View();
    }

    [HttpPost]
    public IActionResult guardarSignIn(string email, string username, string password, DateTime fechaNacimiento, bool aceptaNotificaciones)
    {
        Console.WriteLine(BD.Login(username, password));
        if (BD.Login(username, password) < 1)
        {
            BD.SignIn(email, username, password, fechaNacimiento, aceptaNotificaciones);
            HttpContext.Session.SetString("ID", BD.Login(username, password).ToString());
            return RedirectToAction("Pasar", "Home", new { Direccion = "Home" });
        }
        return RedirectToAction("SignIn");
    }

    public IActionResult logOut()
    {
        HttpContext.Session.Remove("ID");
        return RedirectToAction("Index", "Home");
    }

}
