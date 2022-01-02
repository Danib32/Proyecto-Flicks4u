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
using ejemploapiflcks.Models;

namespace ejemploapiflcks.Controllers
{
    public class CensurasController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/Censuras
        public IQueryable<Censuras> GetCensuras()
        {
            return db.Censuras;
        }

        // GET: api/Censuras/5
        [ResponseType(typeof(Censuras))]
        public IHttpActionResult GetCensuras(int id)
        {
            Censuras censuras = db.Censuras.Find(id);
            if (censuras == null)
            {
                return NotFound();
            }

            return Ok(censuras);
        }

        // PUT: api/Censuras/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCensuras(int id, Censuras censuras)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != censuras.Id)
            {
                return BadRequest();
            }

            db.Entry(censuras).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CensurasExists(id))
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

        // POST: api/Censuras
        [ResponseType(typeof(Censuras))]
        public IHttpActionResult PostCensuras(Censuras censuras)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Censuras.Add(censuras);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = censuras.Id }, censuras);
        }

        // DELETE: api/Censuras/5
        [ResponseType(typeof(Censuras))]
        public IHttpActionResult DeleteCensuras(int id)
        {
            Censuras censuras = db.Censuras.Find(id);
            if (censuras == null)
            {
                return NotFound();
            }

            db.Censuras.Remove(censuras);
            db.SaveChanges();

            return Ok(censuras);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CensurasExists(int id)
        {
            return db.Censuras.Count(e => e.Id == id) > 0;
        }
    }
}