using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;


namespace NoHaze;

static public class BD
{
    private static string _connectionString = @"Server=localhost;DataBase=NoHaze;Integrated Security=True;TrustServerCertificate=True;";


    // login
     public static int Login(string username, string password)
    {
        int IDUsuarioBuscado = -1;
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT ID FROM Usuarios WHERE username = @pUsername AND password = @pPassword";
            IDUsuarioBuscado = connection.QueryFirstOrDefault<int>(query, new { pPassword = password, pUsername = username});
        }
            return IDUsuarioBuscado;
    }

    // Registrarse
    public static void SignIn(string email, string username, string password, DateTime fechaNacimiento, bool aceptaNotificaciones)
    {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Usuarios (email, username, password, fechaNacimiento, monedas, aceptaNotificaciones, racha) VALUES (@pemail, @pusername, @ppassword, @pfechaNacimiento, @pmonedas, @paceptaNotificaciones, @pracha)";
                connection.Execute(query, new {pemail = email, pusername = username, ppassword = password, pfechaNacimiento = fechaNacimiento, pmonedas = 0, paceptaNotificaciones = aceptaNotificaciones, pracha = 0});
            }
    }
}
