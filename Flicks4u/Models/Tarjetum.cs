using System;
using System.Collections.Generic;

#nullable disable

namespace Flicks4u.Models
{
    public class Tarjetum
    {
        public int Id { get; set; }
        public string NumeroTarjeta { get; set; }
        public string FechaEmision { get; set; }
        public int CodigoSeguridad { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
