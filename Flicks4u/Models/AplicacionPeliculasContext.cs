using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Flicks4u.Models
{
    public class AplicacionPeliculasContext : DbContext
    {

        public AplicacionPeliculasContext(DbContextOptions<AplicacionPeliculasContext> options)
            : base(options)
        {
        }

        public DbSet<Censura> Censuras { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Imagen> Imagens { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Tarjetum> Tarjeta { get; set; }
        public DbSet<TipoSubscripcion> TipoSubscripcions { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<VotoPelicula> VotoPeliculas { get; set; }

    }
}
