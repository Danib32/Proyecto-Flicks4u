using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ejemploapiflcks.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Modelejemplo")
        {
        }

        public virtual DbSet<C__EFMigrationsHistory> C__EFMigrationsHistory { get; set; }
        public virtual DbSet<Censuras> Censuras { get; set; }
        public virtual DbSet<Generos> Generos { get; set; }
        public virtual DbSet<Imagens> Imagens { get; set; }
        public virtual DbSet<Peliculas> Peliculas { get; set; }
        public virtual DbSet<Tarjeta> Tarjeta { get; set; }
        public virtual DbSet<TipoSubscripcions> TipoSubscripcions { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<VotoPeliculas> VotoPeliculas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Censuras>()
                .HasMany(e => e.Peliculas)
                .WithRequired(e => e.Censuras)
                .HasForeignKey(e => e.CensuraID);

            modelBuilder.Entity<Generos>()
                .HasMany(e => e.Peliculas)
                .WithRequired(e => e.Generos)
                .HasForeignKey(e => e.GeneroID);

            modelBuilder.Entity<Imagens>()
                .HasMany(e => e.Peliculas)
                .WithOptional(e => e.Imagens)
                .HasForeignKey(e => e.ImagenID);

            modelBuilder.Entity<Peliculas>()
                .HasMany(e => e.VotoPeliculas)
                .WithRequired(e => e.Peliculas)
                .HasForeignKey(e => e.PeliculaID);

            modelBuilder.Entity<Tarjeta>()
                .HasMany(e => e.Usuarios)
                .WithRequired(e => e.Tarjeta)
                .HasForeignKey(e => e.TarjetumID);

            modelBuilder.Entity<TipoSubscripcions>()
                .HasMany(e => e.Usuarios)
                .WithRequired(e => e.TipoSubscripcions)
                .HasForeignKey(e => e.TipoSubscripcionID);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.VotoPeliculas)
                .WithRequired(e => e.Usuarios)
                .HasForeignKey(e => e.UsuarioID);
        }
    }
}
