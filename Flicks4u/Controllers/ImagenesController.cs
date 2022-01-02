using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Flicks4u.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Flicks4u.ViewModels.Imagenes;
using System;
using System.Collections.Generic;
using Flicks4u.Controllers;
using Microsoft.AspNetCore.Http;

namespace Flix4u.Controllers
{
    public class ImagenesController : Controller
    {
        private readonly AplicacionPeliculasContext _context;
        private readonly IWebHostEnvironment _environment;
        public string correo = LoginController.LoginString;

        public ImagenesController(AplicacionPeliculasContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Imagenes
        public async Task<IActionResult> Index(string descripcion, int pg=1)
        {
            ViewBag.NombreLog = correo;
            if(!string.IsNullOrEmpty(descripcion))
            {
                var imagenes = _context.Imagens.Where(d => d.Descripcion.Contains(descripcion));
                return View(await imagenes.ToListAsync());
            }
            else
            {
                List<Imagen> imagenes = _context.Imagens.ToList();
                const int pageSize = 2;
                if (pg < 1)
                    pg = 1;
                int recsCount = imagenes.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var data = imagenes.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                return View(data);
            }
            
        }

        // GET: Imagens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagen = await _context.Imagens.FirstOrDefaultAsync(m => m.Id == id);
            if (imagen == null)
            {
                return NotFound();
            }
            
            return View(imagen);
        }

        // GET: Imagens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Imagens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminImagenViewModel model)
        {
            if (ModelState.IsValid)
            {
                string wwwRoute = _environment.WebRootPath;
                string nombreImagen = Path.GetFileNameWithoutExtension(model.Archivo.FileName);
                string extension = Path.GetExtension(model.Archivo.FileName);
                string nombre = $"{nombreImagen}{DateTime.Now.ToString("yymmssfff")}{extension}";
                string path = Path.Combine(wwwRoute + "\\imagenes\\" + nombre);
                string rutaAux = "imagenes/" + nombre;
                Imagen imagen = new Imagen();
                using (var FileStream = new FileStream(path, FileMode.Create))
                {
                    await model.Archivo.CopyToAsync(FileStream);
                    List<IFormFile> imag64 = new List<IFormFile>();
                    imag64.Add(model.Archivo);
                    foreach(var file in imag64)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            imagen.ImagenBase64 = fileBytes;
                        }
                    }
                }
                imagen.RutaImagen = rutaAux;
                imagen.Descripcion = model.Descripcion;
                _context.Add(imagen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Imagens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagen = await _context.Imagens.FindAsync(id);
            if (imagen == null)
            {
                return NotFound();
            }
            return View(imagen);
        }

        // POST: Imagens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,RutaImagen")] Imagen imagen)
        {
            if (id != imagen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imagen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImagenExists(imagen.Id))
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
            return View(imagen);
        }

        // GET: Imagens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagen = await _context.Imagens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imagen == null)
            {
                return NotFound();
            }
            return View(imagen);
        }

        // POST: Imagens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imagen = await _context.Imagens.FindAsync(id);
            var imagePath = Path.Combine(_environment.WebRootPath + "/" + imagen.RutaImagen);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            if(imagen.Pelicula != null)
            {
                imagen.Pelicula.ImagenID = null;
            }
            _context.Imagens.Remove(imagen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImagenExists(int id)
        {
            return _context.Imagens.Any(e => e.Id == id);
        }
    }
}
