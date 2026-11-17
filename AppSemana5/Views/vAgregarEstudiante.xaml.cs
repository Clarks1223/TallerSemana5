using AppSemana5.Models;
using AppSemana5.Repositories;
using AppSemana5.Utils;

namespace AppSemana5.Views
{
    public partial class vAgregarEstudiante : ContentPage
    {
        private readonly EstudianteRepository _repositorio;

        public vAgregarEstudiante(EstudianteRepository repositorio)
        {
            InitializeComponent();
            _repositorio = repositorio;
        }

        private async void OnAgregarClicked(object? sender, EventArgs e)
        {
            string nombre = txtNombre.Text?.Trim() ?? string.Empty;
            string apellido = txtApellido.Text?.Trim() ?? string.Empty;

            string? error = ValidacionEstudiante.Validar(nombre, apellido, txtEdad.Text?.Trim() ?? string.Empty, out int edad);
            if (error is not null)
            {
                MostrarMensaje(error);
                return;
            }

            try
            {
                _repositorio.Agregar(new Estudiante { Nombre = nombre, Apellido = apellido, Edad = edad });
            }
            catch (Exception ex)
            {
                MostrarMensaje($"No se pudo guardar: {ex.Message}");
                return;
            }

            // Regresa a la tabla, que se recarga sola en OnAppearing.
            await Navigation.PopAsync();
        }

        private async void OnCancelarClicked(object? sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.IsVisible = true;
        }
    }
}
