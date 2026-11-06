using SQLite;

namespace AppSemana5.Models
{
    [Table("estudiante")]
    public class Estudiante
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull, MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;
    }
}
