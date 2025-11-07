using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using EV_FINAL.Models;
using EV_FINAL.Data;
using System.Collections.Generic;
using System.Linq;

namespace EV_FINAL
{
    public sealed partial class MainWindow : Window
    {
        private readonly ServicioRepository repo = new();
        private List<Servicio> servicios;

        public MainWindow()
        {
            this.InitializeComponent();
            servicios = repo.Cargar;
            ResultadoText.Text = $"Servicios cargados: {servicios.Count}";
        }

        private void Registrar_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(IdBox.Text, out int id) || !decimal.TryParse(PrecioBox.Text, out decimal precio))
            {
                ResultadoText.Text = "ID y precio deben ser numéricos.";
                return;
            }

            var nuevo = new Servicio
            {
                Id = id,
                Nombre = NombreBox.Text.Trim(),
                Precio = precio
            };

            if (!nuevo.EsValido(out string mensaje))
            {
                ResultadoText.Text = mensaje;
                return;
            }

            if (ServicioRepository.ExisteDuplicado(servicios, nuevo))
            {
                ResultadoText.Text = "Ya existe un servicio con ese ID o nombre.";
                return;
            }

            servicios.Add(nuevo);
            repo.Guardar(servicios);
            ResultadoText.Text = "Servicio registrado correctamente.";
        }

        private void Consultar_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(IdBox.Text, out int id))
            {
                var servicio = servicios.FirstOrDefault(s => s.Id == id);
                ResultadoText.Text = servicio != null
                    ? $"Servicio encontrado:\nNombre: {servicio.Nombre}\nPrecio: ${servicio.Precio}"
                    : "No se encontró el servicio por ID.";
            }
            else if (!string.IsNullOrWhiteSpace(NombreBox.Text))
            {
                var nombre = NombreBox.Text.Trim();
                var servicio = servicios.FirstOrDefault(s => s.Nombre.Equals(nombre, System.StringComparison.OrdinalIgnoreCase));
                ResultadoText.Text = servicio != null
                    ? $"Servicio encontrado:\nID: {servicio.Id}\nPrecio: ${servicio.Precio}"
                    : "No se encontró el servicio por nombre.";
            }
            else
            {
                ResultadoText.Text = "Ingresa un ID o nombre para consultar.";
            }
        }

        private void Modificar_Click(object sender, RoutedEventArgs e)
        {
            Servicio? servicio = null;

            if (int.TryParse(IdBox.Text, out int id))
            {
                servicio = servicios.FirstOrDefault(s => s.Id == id);
            }
            else if (!string.IsNullOrWhiteSpace(NombreBox.Text))
            {
                var nombre = NombreBox.Text.Trim();
                servicio = servicios.FirstOrDefault(s => s.Nombre.Equals(nombre, System.StringComparison.OrdinalIgnoreCase));
            }

            if (servicio == null)
            {
                ResultadoText.Text = "No se encontró el servicio para modificar.";
                return;
            }

            string nuevoNombre = NombreBox.Text.Trim();
            bool nombreValido = !string.IsNullOrWhiteSpace(nuevoNombre);
            bool precioValido = decimal.TryParse(PrecioBox.Text, out decimal nuevoPrecio) && nuevoPrecio > 0;

            if (!nombreValido && !precioValido)
            {
                ResultadoText.Text = "Ingresa al menos un nuevo nombre o precio válido.";
                return;
            }

            if (nombreValido) servicio.Nombre = nuevoNombre;
            if (precioValido) servicio.Precio = nuevoPrecio;

            repo.Guardar(servicios);
            ResultadoText.Text = "Servicio modificado correctamente.";
        }
    }
}