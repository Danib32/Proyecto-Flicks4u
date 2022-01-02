using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Flicks4u.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Collections.Generic;
using Flix4u.Controllers;

namespace Flicks4u.Controllers
{
    public class PeliculasController : Controller
    {
        private readonly AplicacionPeliculasContext _context;
        private readonly IWebHostEnvironment _environment;
        public string correo = LoginController.LoginString;
        public PeliculasController(AplicacionPeliculasContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Peliculas
        public async Task<IActionResult> Index(string nombre, int pg=1)
        {
            ViewBag.NombreLog = correo;
            if (!string.IsNullOrEmpty(nombre))
            {
                var aplicacionPeliculasContext = _context.Peliculas.Where(p => p.Nombre.Contains(nombre));
                return View(await aplicacionPeliculasContext.ToListAsync());
            }
            else
            {
                List<Pelicula> peliculas = _context.Peliculas.ToList();
                const int pageSize = 2;
                    if (pg < 1)
                        pg = 1;
                int recsCount = peliculas.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var data = peliculas.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                return View(data);
            }
        }

        // GET: Peliculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // GET: Peliculas/Create
        public IActionResult Create()
        {
            ViewData["CensuraID"] = new SelectList(_context.Censuras, "Id", "Nombre");
            ViewData["GeneroID"] = new SelectList(_context.Generos, "Id", "Nombre");
            ViewData["ImagenID"] = new SelectList(_context.Imagens, "Id", "RutaImagen");
            return View();
        }

        // POST: Peliculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Sinopsis,Lanzamiento,Escritores,CensuraID,GeneroID,Esactivo,ImagenID")] Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CensuraID"] = new SelectList(_context.Censuras, "Id", "Nombre", pelicula.CensuraID);
            ViewData["GeneroID"] = new SelectList(_context.Generos, "Id", "Nombre", pelicula.GeneroID);
            ViewData["ImagenID"] = new SelectList(_context.Imagens, "Id", "RutaImagen", pelicula.ImagenID);
            return View(pelicula);
        }

        // GET: Peliculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            ViewData["CensuraID"] = new SelectList(_context.Censuras, "Id", "Nombre", pelicula.CensuraID);
            ViewData["GeneroID"] = new SelectList(_context.Generos, "Id", "Nombre", pelicula.GeneroID);
            ViewData["ImagenID"] = new SelectList(_context.Imagens, "Id", "RutaImagen", pelicula.ImagenID);
            return View(pelicula);
        }

        // POST: Peliculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Sinopsis,Lanzamiento,Escritores,CensuraID,GeneroID,Esactivo,ImagenID")] Pelicula pelicula)
        {
            if (id != pelicula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CensuraID"] = new SelectList(_context.Censuras, "Id", "Nombre", pelicula.CensuraID);
            ViewData["GeneroID"] = new SelectList(_context.Generos, "Id", "Nombre", pelicula.GeneroID);
            ViewData["ImagenID"] = new SelectList(_context.Imagens, "Id", "RutaImagen", pelicula.ImagenID);
            return View(pelicula);
        }

        // GET: Peliculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula.Imagen != null)
            {
                string wwwroute = _environment.WebRootPath;
                var imagenes = await _context.Imagens.FindAsync(pelicula.Imagen.Id);
                string ruta = Path.Combine(wwwroute + "/" + imagenes.RutaImagen);
                System.IO.File.Delete(ruta);
                _context.Imagens.Remove(imagenes);
            }
            _context.Peliculas.Remove(pelicula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(int id)
        {
            return _context.Peliculas.Any(e => e.Id == id);
        }
    }
}
