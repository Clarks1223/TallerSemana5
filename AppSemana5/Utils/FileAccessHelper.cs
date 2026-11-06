namespace AppSemana5.Utils
{
    public static class FileAccessHelper
    {
        // FileSystem.AppDataDirectory apunta a la carpeta privada de la app en
        // Android, iOS, Mac Catalyst y Windows, así que la misma línea sirve para todos.
        public static string GetLocalFilePath(string fileName)
        {
            Directory.CreateDirectory(FileSystem.AppDataDirectory);
            return Path.Combine(FileSystem.AppDataDirectory, fileName);
        }
    }
}
