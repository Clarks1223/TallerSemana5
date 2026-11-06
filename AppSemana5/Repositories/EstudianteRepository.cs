using AppSemana5.Models;
using SQLite;

namespace AppSemana5.Repositories
{
    public class EstudianteRepository
    {
        private readonly string _dbPath;
        private SQLiteConnection? _conn;

        public EstudianteRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        // Abre la conexión la primera vez que se usa. SQLite crea el archivo .db si
        // no existe y CreateTable solo crea la tabla si todavía no está creada.
        private SQLiteConnection Conexion()
        {
            if (_conn is null)
            {
                _conn = new SQLiteConnection(_dbPath);
                _conn.CreateTable<Estudiante>();
            }

            return _conn;
        }

        public List<Estudiante> ObtenerTodos()
        {
            return Conexion().Table<Estudiante>()
                             .OrderByDescending(e => e.Id)
                             .ToList();
        }

        public void Agregar(string nombre)
        {
            Conexion().Insert(new Estudiante { Nombre = nombre });
        }

        public void Eliminar(int id)
        {
            Conexion().Delete<Estudiante>(id);
        }
    }
}
