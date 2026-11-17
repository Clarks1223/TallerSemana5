using AppSemana5.Models;
using AppSemana5.Repositories;
using AppSemana5.Utils;

namespace AppSemana5.Views
{
    public partial class vActualizarEliminar : ContentPage
    {
        private readonly EstudianteRepository _repositorio;
        private readonly int _codigo;

        public vActualizarEliminar(EstudianteRepository repositorio, Estudiante datos)
        {
            InitializeComponent();
            _repositorio = repositorio;
            _codigo = datos.Codigo;

            txtCodigo.Text = datos.Codigo.ToString();
            txtNombre.Text = datos.Nombre;
            txtApellido.Text = datos.Apellido;
            txtEdad.Text = datos.Edad.ToString();
        }

        private async void OnActualizarClicked(object? sender, EventArgs e)
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
                _repositorio.Actualizar(new Estudiante { Codigo = _codigo, Nombre = nombre, Apellido = apellido, Edad = edad });
            }
            catch (Exception ex)
            {
                MostrarMensaje($"No se pudo actualizar: {ex.Message}");
                return;
            }

            await Navigation.PopAsync();
        }

        private async void OnEliminarClicked(object? sender, EventArgs e)
        {
            bool confirmar = await DisplayAlertAsync(
                "Eliminar estudiante",
                $"¿Seguro que quieres eliminar a {txtNombre.Text} {txtApellido.Text}?",
                "Eliminar",
                "Cancelar");

            if (!confirmar)
                return;

            try
            {
                _repositorio.Eliminar(_codigo);
            }
            catch (Exception ex)
            {
                MostrarMensaje($"No se pudo eliminar: {ex.Message}");
                return;
            }

            await Navigation.PopAsync();
        }

        private void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.IsVisible = true;
        }
    }
}
