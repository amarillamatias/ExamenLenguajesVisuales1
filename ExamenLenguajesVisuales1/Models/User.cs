using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenLenguajesVisuales1.Models
{
    [Table("Usuarios")]
    public class User
    {
        [Key]
        [Column("id_usuario")]
        public int Id { get; set; }

        [Required]
        [Column("usuario")]
        public string Nombre { get; set; }

        [Required]
        [Column("contraseña")]
        public string Password { get; set; }

        [Column("rol")]
        public string Rol { get; set; }
    }
}