namespace AppSemana5.Utils
{
    // Validación compartida por las vistas de agregar y de actualizar.
    public static class ValidacionEstudiante
    {
        // Devuelve el mensaje de error, o null si los datos son válidos.
        public static string? Validar(string nombre, string apellido, string edadTexto, out int edad)
        {
            edad = 0;

            if (nombre.Length == 0)
                return "Escribe el nombre.";

            if (apellido.Length == 0)
                return "Escribe el apellido.";

            if (!int.TryParse(edadTexto, out edad) || edad < 1 || edad > 120)
                return "Escribe una edad válida (de 1 a 120).";

            return null;
        }
    }
}
