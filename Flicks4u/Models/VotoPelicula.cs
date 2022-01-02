using System;
using System.Collections.Generic;

#nullable disable

namespace Flicks4u.Models
{
    public class VotoPelicula
    {
        public int Id { get; set; }
        public int PeliculaID { get; set; }
        public int UsuarioID { get; set; }
        public int Valor { get; set; }

        public virtual Pelicula Pelicula { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
