namespace ejemploapiflcks.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Usuarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuarios()
        {
            VotoPeliculas = new HashSet<VotoPeliculas>();
        }

        public int Id { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Contrasenia { get; set; }

        public string CorreoElectronico { get; set; }

        public int TipoSubscripcionID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime FechaNacimiento { get; set; }

        public int TarjetumID { get; set; }

        public bool Esactivo { get; set; }

        public virtual Tarjeta Tarjeta { get; set; }

        public virtual TipoSubscripcions TipoSubscripcions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VotoPeliculas> VotoPeliculas { get; set; }
    }
}
