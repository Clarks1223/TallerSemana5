using AppSemana5.Models;
using AppSemana5.Repositories;

namespace AppSemana5.Views
{
    public partial class vEstudiante : ContentPage
    {
        private readonly EstudianteRepository _repositorio;

        public vEstudiante(EstudianteRepository repositorio)
        {
            InitializeComponent();
            _repositorio = repositorio;
            CargarLista();
        }

        private void OnAgregarClicked(object? sender, EventArgs e)
        {
            string nombre = txtNombre.Text?.Trim() ?? string.Empty;

            if (nombre.Length == 0)
            {
                MostrarMensaje("Escribe un nombre.");
                return;
            }

            try
            {
                _repositorio.Agregar(nombre);
            }
            catch (Exception ex)
            {
                MostrarMensaje($"No se pudo guardar: {ex.Message}");
                return;
            }

            txtNombre.Text = string.Empty;
            lblMensaje.IsVisible = false;
            CargarLista();
        }

        private void OnEliminarClicked(object? sender, EventArgs e)
        {
            if (sender is not Button { BindingContext: Estudiante estudiante })
                return;

            try
            {
                _repositorio.Eliminar(estudiante.Id);
            }
            catch (Exception ex)
            {
                MostrarMensaje($"No se pudo eliminar: {ex.Message}");
                return;
            }

            lblMensaje.IsVisible = false;
            CargarLista();
        }

        private void CargarLista()
        {
            try
            {
                lstEstudiantes.ItemsSource = _repositorio.ObtenerTodos();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"No se pudo cargar la lista: {ex.Message}");
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.IsVisible = true;
        }
    }
}
