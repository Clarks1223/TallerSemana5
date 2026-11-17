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
        // no existe (base embebida, igual que H2 en Java) y CreateTable solo crea la
        // tabla si todavía no está creada.
        private SQLiteConnection Conexion()
        {
            if (_conn is null)
            {
                _conn = new SQLiteConnection(_dbPath);

                // Los datos de prueba se insertan solo cuando la tabla se crea por
                // primera vez; si después borras todo, no vuelven a aparecer.
                if (_conn.CreateTable<Estudiante>() == CreateTableResult.Created)
                    _conn.InsertAll(DatosDePrueba());
            }

            return _conn;
        }

        private static List<Estudiante> DatosDePrueba() =>
        [
            new Estudiante { Nombre = "Ana", Apellido = "Pérez", Edad = 20 },
            new Estudiante { Nombre = "Luis", Apellido = "Mora", Edad = 22 },
            new Estudiante { Nombre = "María", Apellido = "Torres", Edad = 19 },
        ];

        public List<Estudiante> ObtenerTodos()
        {
            return Conexion().Table<Estudiante>()
                             .OrderBy(e => e.Codigo)
                             .ToList();
        }

        public void Agregar(Estudiante estudiante)
        {
            Conexion().Insert(estudiante);
        }

        public void Actualizar(Estudiante estudiante)
        {
            Conexion().Update(estudiante);
        }

        public void Eliminar(int codigo)
        {
            Conexion().Delete<Estudiante>(codigo);
        }
    }
}
