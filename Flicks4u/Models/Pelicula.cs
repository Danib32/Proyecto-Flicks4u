using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Flicks4u.Models
{
    public class Pelicula
    {

        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Sinopsis { get; set; }
        public DateTime Lanzamiento { get; set; }
        [Required]
        public string Escritores { get; set; }
        public int CensuraID { get; set; }
        public int GeneroID { get; set; }
        public bool Esactivo { get; set; }
        public int? ImagenID { get; set; }
        public virtual Genero Genero { get; set; }
        public virtual Censura Censura { get; set; }
        public virtual Imagen Imagen { get; set; }
        public virtual ICollection<VotoPelicula> Votos { get; set; }
    }
}
