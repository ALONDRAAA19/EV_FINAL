using EV_FINAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
namespace EV_FINAL.Data;
public class ServicioRepository
{
    private static readonly string RutaArchivo =
    Path.Combine(AppContext.BaseDirectory, "servicios.json");

    public List<Servicio> Cargar
    {
        get
        {
            if (!File.Exists(RutaArchivo)) return new();
            var json = File.ReadAllText(RutaArchivo);
            return JsonSerializer.Deserialize<List<Servicio>>(json) ?? new();
        }
    }
    public void Guardar(List<Servicio> servicios)
    {
        JsonSerializerOptions jsonSerializerOptions = new() { WriteIndented = true };
        var opciones = jsonSerializerOptions;
        var json = JsonSerializer.Serialize(servicios, opciones);
        File.WriteAllText(RutaArchivo, json);
    }
    public static bool ExisteDuplicado(List<Servicio> lista, Servicio nuevo)
    {
        return lista.Any(s => s.Id == nuevo.Id || s.Nombre.Equals(nuevo.Nombre,
       System.StringComparison.OrdinalIgnoreCase));
    }
}