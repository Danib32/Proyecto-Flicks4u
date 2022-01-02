using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Flicks4u.Models
{
    public class Imagen
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        [Required]
        public string RutaImagen { get; set; }
        public byte[] ImagenBase64 { get; set; }
        public virtual Pelicula Pelicula { get; set; }
    }
}
