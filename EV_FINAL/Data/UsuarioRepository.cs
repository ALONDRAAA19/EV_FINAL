using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using EV_FINAL.Models;

namespace EV_FINAL.Data;

public class UsuarioRepository
{
    private static readonly string RutaArchivo = Path.Combine(AppContext.BaseDirectory, "usuarios.json");

    public List<Usuario> Cargar()
    {
        if (!File.Exists(RutaArchivo)) return new();
        var json = File.ReadAllText(RutaArchivo);
        return JsonSerializer.Deserialize<List<Usuario>>(json) ?? new();
    }

    public void Guardar(List<Usuario> usuarios)
    {
        var opciones = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(usuarios, opciones);
        File.WriteAllText(RutaArchivo, json);
    }

    public static bool ExisteUsuario(List<Usuario> lista, Usuario usuario)
    {
        return lista.Any(u => u.NombreUsuario.Equals(usuario.NombreUsuario, StringComparison.OrdinalIgnoreCase));
    }

    public static bool ValidarCredenciales(List<Usuario> lista, string nombre, string contraseña)
    {
        return lista.Any(u => u.NombreUsuario.Equals(nombre, StringComparison.OrdinalIgnoreCase) && u.Contraseña == contraseña);
    }
}