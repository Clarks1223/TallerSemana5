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
        }

        // Se ejecuta al abrir la página y cada vez que se vuelve a ella desde
        // agregar o actualizar/eliminar, así la tabla siempre está al día.
        protected override void OnAppearing()
        {
            base.OnAppearing();
            CargarLista();
        }

        private async void OnAgregarClicked(object? sender, EventArgs e)
        {
            await Navigation.PushAsync(new vAgregarEstudiante(_repositorio));
        }

        private async void OnEstudianteSeleccionado(object? sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is not Estudiante estudiante)
                return;

            // Se limpia la selección para poder volver a tocar la misma fila al regresar.
            lstEstudiantes.SelectedItem = null;

            await Navigation.PushAsync(new vActualizarEliminar(_repositorio, estudiante));
        }

        private void CargarLista()
        {
            try
            {
                lstEstudiantes.ItemsSource = _repositorio.ObtenerTodos();
                lblMensaje.IsVisible = false;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"No se pudo cargar la lista: {ex.Message}";
                lblMensaje.IsVisible = true;
            }
        }
    }
}
