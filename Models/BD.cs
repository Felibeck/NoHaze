using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;


namespace NoHaze;

static public class BD
{
    // Inicio Conexion a la base de datos NORMAL

    private static string _connectionString = @"Server=localhost;DataBase=NoHaze;Integrated Security=True;TrustServerCertificate=True;";

    // Cierre de conexion a la base de datos NORMAL

    // Inicio Conexion a la base de datos de CASA
    // private static string _connectionString = @"Server=localhost\SQLEXPRESS;Database=NoHaze;Integrated Security=True;TrustServerCertificate=True;";

    // Cierre de conexion a la base de datos de CASA



    // login
    public static int Login(string username, string password)
    {
        int IDUsuarioBuscado = 0;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT ID FROM Usuarios WHERE username = @pUsername AND password = @pPassword";
            IDUsuarioBuscado = connection.QueryFirstOrDefault<int>(query, new { pUsername = username, pPassword = password });
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


// get lista de playlists
    public static List<Playlist> getListaPlaylists(int IDUsuario)
    {
        List<Playlist> playlists = new List<Playlist>();
        using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Playlists WHERE IDUsuario = @pIDUsuario";
                playlists = connection.Query<Playlist>(query, new { pIDUsuario = IDUsuario}).ToList();
            }

        return playlists;
        // despues para comprobar si esta vacia simplemente usamos el empty si lo necesitamos
    }

//get Usuario

    public static Usuario GetUsuario(int IDUsuario)
    {
        Usuario UsuarioBuscado = new Usuario(); 
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuarios WHERE ID = @pIDUsuario";
            UsuarioBuscado = connection.QueryFirstOrDefault<Usuario>(query, new { pIDUsuario = IDUsuario});
        }
            return UsuarioBuscado;
    }

    // get frecuencias por id de playlist

     public static List<Frecuencia> getListaFrecuencias(int IDPlaylist)
    {
        List<Frecuencia> frecuencias = new List<Frecuencia>();
        using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "exec getFrecuencias @pIDPlaylist";
                frecuencias = connection.Query<Frecuencia>(query, new { pIDPlaylist = IDPlaylist}).ToList();
            }

        return frecuencias;
    }

    // get desafios por id de usuario

    public static List<Desafio> getListaDesafios(int IDUsuario)
    {
        List<Desafio> desafios = new List<Desafio>();
        using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "exec getDesafios @pIDUsuario";
                desafios = connection.Query<Desafio>(query, new { pIDUsuario = IDUsuario}).ToList();
            }

        return desafios;
    }

    // get tags por id playlist
   public static List<Tag> getListaTags(int IDPlaylist)
    {
        List<Tag> tags = new List<Tag>();
        using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "exec getListaTags @pIDPlaylist";
                tags = connection.Query<Tag>(query, new { pIDPlaylist = IDPlaylist}).ToList();
            }

        return tags;
    }

    // get apps de ocio por id usuario

   public static List<AppOcio> getListaAppsOcio(int IDUsuario)
    {
        List<AppOcio> appsOcio = new List<AppOcio>();
        using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "exec getListaAppsOcio @pIDUsuario";
                appsOcio = connection.Query<AppOcio>(query, new { pIDUsuario = IDUsuario}).ToList();
            }

        return appsOcio;
    }

    


    // get playlist por IDPlaylist

    public static Playlist GetPlaylist(int IDUsuario)
    {
        Playlist playlistBuscada = new Playlist(); 
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Playlists WHERE Playlists.IDUsuario = @pIDUsuario";
            playlistBuscada = connection.QueryFirstOrDefault<Playlist>(query, new { pIDUsuario = IDUsuario});
        }
            return playlistBuscada;
    }

    // eliminar app de ocio del usuario

    public static int eliminarAppOcio(int IDApp, int IDUsuario)
    {
        int registrosAfectados = 0;
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "exec eliminarAppOcio @pIDApp @IDUsuario";
            registrosAfectados = connection.Execute(query, new { pIDApp = IDApp, pIDUsuario = IDUsuario});

        }
        return registrosAfectados;
    }

        public static List<int> getHorasProductivas(int IDUsuario, int dias)
    {
        List<Informe> informes = new List<Informe>();
        using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "exec getHorasProductivas @pIDUsuario,  @pdias";
                informes = connection.Query<Informe>(query, new { pIDUsuario = IDUsuario, pdias = dias}).ToList();
            }

        List<int> informesAcotados = new List<int>();
        DateTime Hoy = DateTime.Today;

        for (int i = 0; i < dias; i++)
        {
                int acuHoras = 0;
            for (int j = 0; j < informes.Count; j++)
            {
                if(informes[j].fecha == (Hoy.AddDays(-i)))
                {
                    acuHoras+= informes[j].horas;
                }
            }
            informesAcotados.Add(acuHoras);
        }
        return informesAcotados;
    }

    // actualizar los datos del perfil

    public static void actualizarPerfil(string username, DateTime fechaNacimiento, string descripcion, string objetivo, int IDUsuario)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "exec actualizarPerfil @pusername @pfechaNacimiento @pdescripcion @pobjetivo @pIDUsuario";
            connection.Execute(query, new { pusername = username,  pfechaNacimiento = fechaNacimiento, pdescripcion = descripcion, pobjetivo = objetivo, pIDUsuario = IDUsuario});
        }

        return;
    }

   
}