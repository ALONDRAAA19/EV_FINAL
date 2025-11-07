namespace EV_FINAL.Models;
public class Servicio
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public bool EsValido(out string mensaje)
    {
        if (string.IsNullOrWhiteSpace(Nombre))
        {
            mensaje = "El nombre no puede estar vacío.";
            return false;
        }
        if (Precio <= 0)
        {
            mensaje = "El precio debe ser mayor a cero.";
            return false;
        }
        mensaje = "";
        return true;
    }
}