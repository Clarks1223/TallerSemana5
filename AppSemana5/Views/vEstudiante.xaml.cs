using AppSemana5.Models;
using AppSemana5.Repositories;

namespace AppSemana5.Views
{
    public partial class vEstudiante : ContentPage
    {
        private readonly EstudianteRepository _repositorio;

        // Estudiante que se está editando; null significa que se va a agregar uno nuevo.
        private Estudiante? _enEdicion;

        public vEstudiante(EstudianteRepository repositorio)
        {
            InitializeComponent();
            _repositorio = repositorio;
            CargarLista();
        }

        private void OnGuardarClicked(object? sender, EventArgs e)
        {
            string nombre = txtNombre.Text?.Trim() ?? string.Empty;

            if (nombre.Length == 0)
            {
                MostrarMensaje("Escribe un nombre.");
                return;
            }

            try
            {
                if (_enEdicion is null)
                    _repositorio.Agregar(nombre);
                else
                    _repositorio.Actualizar(_enEdicion.Id, nombre);
            }
            catch (Exception ex)
            {
                MostrarMensaje($"No se pudo guardar: {ex.Message}");
                return;
            }

            LimpiarFormulario();
            CargarLista();
        }

        private void OnEditarClicked(object? sender, EventArgs e)
        {
            if (sender is not Button { BindingContext: Estudiante estudiante })
                return;

            _enEdicion = estudiante;
            txtNombre.Text = estudiante.Nombre;
            btnGuardar.Text = "Actualizar";
            btnCancelar.IsVisible = true;
            lblMensaje.IsVisible = false;
            txtNombre.Focus();
        }

        private void OnCancelarClicked(object? sender, EventArgs e)
        {
            LimpiarFormulario();
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

            // Si se elimina el estudiante que estaba en edición, el formulario vuelve a "Agregar".
            if (_enEdicion?.Id == estudiante.Id)
                LimpiarFormulario();

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

        private void LimpiarFormulario()
        {
            _enEdicion = null;
            txtNombre.Text = string.Empty;
            btnGuardar.Text = "Agregar";
            btnCancelar.IsVisible = false;
            lblMensaje.IsVisible = false;
        }

        private void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.IsVisible = true;
        }
    }
}
