public class Usuario
{
    public int id {get;  set;}
    public string username {get;  set;}
    public string email {get;  set;}
    public string password {get;  set;}
    public DateTime fechaNacimiento {get;  set;}
    public string descripcion {get;  set;}
    public string objetivo {get;  set;}
    public int monedas {get;  set;}
    public bool aceptaNotis {get;  set;}
    public DateTime ultimoIngreso {get;  set;}
    public int racha {get;  set;}
    public string foto {get;  set;}

    

    public Usuario() {}

    public Usuario(int id, string username, string email, string password, DateTime fechanacimiento, string descripcion, string objetivo, int monedas, bool aceptaNotis, DateTime ultimaFecha, int racha)
    {
        this.id = id;
        this.username = username;
        this.email = email;
        this.password = password;
        this.fechaNacimiento = fechanacimiento;
        this.aceptaNotis = aceptaNotis;
        monedas = 0;
        racha = 0;
    }
}