public class informeAcotado
{
    public int horas {get; private set;}
    public DateTime fecha {get; private set;}


    public informeAcotado(int horas, DateTime fecha)
    {
        this.horas = horas;
        this.fecha = fecha;
    }
}
