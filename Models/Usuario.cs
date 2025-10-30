public class Usuario
{
    public int id {get; private set;}
    public string username {get; private set;}
    public string email {get; private set;}
    public string password {get; private set;}
    public DateTime fechaNacimiento {get; private set;}
    public string descripcion {get; private set;}
    public string objetivo {get; private set;}
    public int monedas {get; private set;}
    public bool aceptaNotis {get; private set;}
    public DateTime ultimoIngreso {get; private set;}
    public int racha {get; private set;}
    public string foto {get; private set;}

    

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