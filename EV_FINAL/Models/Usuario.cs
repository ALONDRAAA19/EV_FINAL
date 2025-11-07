namespace EV_FINAL.Models;

public class Usuario
{
    public string NombreUsuario { get; set; } = string.Empty;
    public string Contraseña { get; set; } = string.Empty;

    public bool EsValido(out string mensaje)
    {
        if (string.IsNullOrWhiteSpace(NombreUsuario))
        {
            mensaje = "El nombre de usuario no puede estar vacío.";
            return false;
        }
        if (string.IsNullOrWhiteSpace(Contraseña))
        {
            mensaje = "La contraseña no puede estar vacía.";
            return false;
        }
        mensaje = "";
        return true;
    }
}