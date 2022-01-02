namespace ejemploapiflcks.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Peliculas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Peliculas()
        {
            VotoPeliculas = new HashSet<VotoPeliculas>();
        }

        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Sinopsis { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Lanzamiento { get; set; }

        [Required]
        public string Escritores { get; set; }

        public int CensuraID { get; set; }

        public int GeneroID { get; set; }

        public bool Esactivo { get; set; }

        public int? ImagenID { get; set; }

        public virtual Censuras Censuras { get; set; }

        public virtual Generos Generos { get; set; }

        public virtual Imagens Imagens { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VotoPeliculas> VotoPeliculas { get; set; }
    }
}
