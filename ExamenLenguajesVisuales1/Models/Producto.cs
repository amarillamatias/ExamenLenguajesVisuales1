using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenLenguajesVisuales1.Models
{
    [Table("Producto")]
    public class Producto
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Required]
        [Column("precio")]
        public decimal Precio { get; set; }

        [Required]
        [Column("stock")]
        public int Stock { get; set; }

        [Required]
        [Column("categoria_id")]
        public int CategoriaId { get; set; }

        [Required]
        [Column("usuario_id")]
        public int UsuarioId { get; set; }
    }
}