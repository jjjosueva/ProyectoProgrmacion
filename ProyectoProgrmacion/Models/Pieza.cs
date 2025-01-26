using SQLite;

namespace ProyectoProgrmacion.Models
{
    [Table("Piezas")] 
    public class Pieza
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }  

        [NotNull]
        public string Nombre { get; set; }

        [NotNull]
        public string Descripcion { get; set; }

        [NotNull]
        public decimal Precio { get; set; }

        public string ImagenURL { get; set; } 

        public override string ToString()
        {
            return $"{Id}: {Nombre} - {Descripcion} ({Precio:C})";
        }
    }
}
