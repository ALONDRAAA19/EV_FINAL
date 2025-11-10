using EV_FINAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace EV_FINAL.Data
{
    public class UsuarioRepository
    {
        private static readonly string RutaArchivo =
            Path.Combine(AppContext.BaseDirectory, "usuarios.json");

        public List<Usuario> Cargar()
        {
            if (!File.Exists(RutaArchivo)) return new();
            var json = File.ReadAllText(RutaArchivo);
            return JsonSerializer.Deserialize<List<Usuario>>(json) ?? new();
        }

        public bool Validar(string usuario, string password)
        {
            var lista = Cargar();
            return lista.Exists(u =>
                u.Usuario.Equals(usuario, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);
        }

        public bool ExisteUsuario(string usuario)
        {
            var lista = Cargar();
            return lista.Exists(u =>
                u.Usuario.Equals(usuario, StringComparison.OrdinalIgnoreCase));
        }

        public bool Registrar(Usuario nuevo)
        {
            var lista = Cargar();

            if (ExisteUsuario(nuevo.Usuario))
                return false;

            lista.Add(nuevo);
            var json = JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(RutaArchivo, json);
            return true;
        }
    }
}
