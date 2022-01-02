namespace ejemploapiflcks.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VotoPeliculas
    {
        public VotoPeliculas() { }
        public VotoPeliculas(int peliculaID, int usuarioID, int valor)
        {
            PeliculaID = peliculaID;
            UsuarioID = usuarioID;
            Valor = valor;
        }

        public int Id { get; set; }

        public int PeliculaID { get; set; }

        public int UsuarioID { get; set; }

        public int Valor { get; set; }

        public virtual Peliculas Peliculas { get; set; }

        public virtual Usuarios Usuarios { get; set; }
    }
}
