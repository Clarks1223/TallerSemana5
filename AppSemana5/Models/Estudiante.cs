using SQLite;

namespace AppSemana5.Models
{
    // Tabla nueva "estudiantes" (la antigua "estudiante" solo tenía Id y Nombre).
    [Table("estudiantes")]
    public class Estudiante
    {
        [PrimaryKey, AutoIncrement]
        public int Codigo { get; set; }

        [NotNull, MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [NotNull, MaxLength(50)]
        public string Apellido { get; set; } = string.Empty;

        public int Edad { get; set; }
    }
}
