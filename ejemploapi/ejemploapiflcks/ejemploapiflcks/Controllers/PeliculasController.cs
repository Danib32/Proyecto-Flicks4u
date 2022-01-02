using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ejemploapiflcks.DTOS;
using ejemploapiflcks.Models;

namespace ejemploapiflcks.Controllers
{
    public class PeliculasController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/Peliculas
        public IQueryable<PeliculaDto> GetPeliculas()
        {
            List<PeliculaDto> lista = new List<PeliculaDto>();
            var peliculas = db.Peliculas;
            foreach (Peliculas pelicula in peliculas)
            {
                PeliculaDto peliculaInfo = new PeliculaDto(pelicula.Id, pelicula.Nombre, pelicula.Sinopsis, pelicula.Lanzamiento, pelicula.Escritores, pelicula.Imagens.Descripcion, pelicula.Imagens.ImagenBase64, pelicula.Generos.Nombre, pelicula.VotoPeliculas.Count, pelicula.Esactivo);
                lista.Add(peliculaInfo);
            }
            var query = lista.AsQueryable();
            return query;
        }

        // GET: api/Peliculas/5
        [ResponseType(typeof(Peliculas))]
        public IHttpActionResult GetPeliculas(int id)
        {
            Peliculas peliculas = db.Peliculas.Find(id);
            if (peliculas == null)
            {
                return NotFound();
            }

            return Ok(peliculas);
        }

        // PUT: api/Peliculas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPeliculas(int id, Peliculas peliculas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != peliculas.Id)
            {
                return BadRequest();
            }

            db.Entry(peliculas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeliculasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Peliculas
        [ResponseType(typeof(Peliculas))]
        public IHttpActionResult PostPeliculas(Peliculas peliculas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Peliculas.Add(peliculas);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = peliculas.Id }, peliculas);
        }

        // DELETE: api/Peliculas/5
        [ResponseType(typeof(Peliculas))]
        public IHttpActionResult DeletePeliculas(int id)
        {
            Peliculas peliculas = db.Peliculas.Find(id);
            if (peliculas == null)
            {
                return NotFound();
            }

            db.Peliculas.Remove(peliculas);
            db.SaveChanges();

            return Ok(peliculas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PeliculasExists(int id)
        {
            return db.Peliculas.Count(e => e.Id == id) > 0;
        }
    }
}