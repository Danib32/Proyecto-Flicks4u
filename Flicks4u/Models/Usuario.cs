using System;
using System.Collections.Generic;

#nullable disable

namespace Flicks4u.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Contrasenia { get; set; }
        public string CorreoElectronico { get; set; }
        public int TipoSubscripcionID { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int TarjetumID { get; set; }
        public bool Esactivo { get; set; }

        public virtual TipoSubscripcion TipoSubscripcion { get; set; }
        public virtual Tarjetum Tarjeta { get; set; }
        public virtual ICollection<VotoPelicula> VotoPeliculas { get; set; }
    }
}
