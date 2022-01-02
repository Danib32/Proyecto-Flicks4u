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
    public class VotoPeliculasController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/VotoPeliculas
        public IQueryable<VotoPeliculas> GetVotoPeliculas()
        {
            return db.VotoPeliculas;
        }

        // GET: api/VotoPeliculas/5
        [ResponseType(typeof(VotoPeliculas))]
        public IHttpActionResult GetVotoPeliculas(int id)
        {
            VotoPeliculas votoPeliculas = db.VotoPeliculas.Find(id);
            if (votoPeliculas == null)
            {
                return NotFound();
            }

            return Ok(votoPeliculas);
        }

        // PUT: api/VotoPeliculas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVotoPeliculas(int id, VotoPeliculas votoPeliculas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != votoPeliculas.Id)
            {
                return BadRequest();
            }

            db.Entry(votoPeliculas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VotoPeliculasExists(id))
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

        // POST: api/VotoPeliculas
        [ResponseType(typeof(VotoPeliculas))]
        public IHttpActionResult PostVotoPeliculas(VotoDto voto)
        {
            var usuario = db.Usuarios.Where(u => u.CorreoElectronico== voto.UsuarioMail).First();
            
            if (voto.Delete) 
            {
                var pelicula = db.VotoPeliculas.Where(vt => vt.UsuarioID == usuario.Id && vt.PeliculaID == voto.PeliculaID).First();
                db.VotoPeliculas.Remove(pelicula);
            }
            else
            {
                int valorVoto = 1; // TODO: Sacar el valor del voto.
                VotoPeliculas votoNuevo = new VotoPeliculas(voto.PeliculaID, usuario.Id, valorVoto);
                db.VotoPeliculas.Add(votoNuevo);
            }
            db.SaveChanges();


            return Ok();
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VotoPeliculasExists(int id)
        {
            return db.VotoPeliculas.Count(e => e.Id == id) > 0;
        }
    }
}