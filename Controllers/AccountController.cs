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
        return View();
    }

    [HttpPost] 

    public IActionResult guardarLogin(string username, string password)
    {   

        int IDusuario = BD.Login(username, password);

        if(IDusuario != -1)
        {
             HttpContext.Session.SetString("ID",IDusuario.ToString());
             return RedirectToAction("Home", "Home");
        }else   
        {
            return RedirectToAction("Login");
        }

    }

     public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost]
    public IActionResult guardarSignIn(string email, string username, string password, DateTime fechaNacimiento, bool aceptaNotificaciones)
    {
        if(BD.Login(username, password) == -1)
        {
            BD.SignIn(email, username, password, fechaNacimiento, aceptaNotificaciones);

            HttpContext.Session.SetString("ID",BD.Login(username, password).ToString());
            return View();
        }
        return RedirectToAction("SignIn");
    }

    public IActionResult logOut()
    {
        HttpContext.Session.Remove("ID");
        return RedirectToAction("Index", "Home");
    }

}   
