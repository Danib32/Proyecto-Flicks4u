using System;
using System.Collections.Generic;

#nullable disable

namespace Flicks4u.Models
{
    public class TipoSubscripcion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
