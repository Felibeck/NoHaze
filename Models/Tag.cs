public class Tag
{
    public int id {get; private set;}
    public string titulo {get; private set;}
    
    public Tag() {}
    
    // Constructor para Dapper
    public Tag(int id, string titulo)
    {
        this.id = id;
        this.titulo = titulo;
    }
}