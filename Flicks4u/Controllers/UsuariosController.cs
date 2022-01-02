using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Flicks4u.Models;
using System.Collections.Generic;
using System;

namespace Flicks4u.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly AplicacionPeliculasContext _context;
        public UsuariosController(AplicacionPeliculasContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index(string nombre, int pg=1)
        {
            if(!string.IsNullOrEmpty(nombre))
            {
                var aplicacionPeliculasContext = _context.Usuarios.Where(u => u.Nombres.Contains(nombre));
                return View(await aplicacionPeliculasContext.ToListAsync());
            }
            else
            {
                List<Usuario> usuarios = _context.Usuarios.ToList();
                const int pageSize = 2;
                if (pg < 1)
                    pg = 1;
                int recsCount = usuarios.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var data = usuarios.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                return View(data);
            }
            
            
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewData["TarjetumID"] = new SelectList(_context.Tarjeta, "Id", "NumeroTarjeta");
            ViewData["TipoSubscripcionID"] = new SelectList(_context.TipoSubscripcions, "Id", "Nombre");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombres,Apellidos,Contrasenia,CorreoElectronico,TipoSubscripcionID,FechaNacimiento,TarjetumID,Esactivo")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Esactivo = true;
                Tarjetum tarjeta = new Tarjetum();
                var fecha = DateTime.Now;
                tarjeta.NumeroTarjeta = "Continuar sin tarjeta";
                tarjeta.FechaEmision = fecha.ToString();
                tarjeta.CodigoSeguridad = 000;
                usuario.Tarjeta = tarjeta;
                tarjeta.Usuario = usuario;
                usuario.TipoSubscripcionID = 1;
                _context.Add(tarjeta);
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("CuentaCreada", "Login");
            }
            ViewData["TarjetumID"] = new SelectList(_context.Tarjeta, "Id", "NumeroTarjeta", usuario.TarjetumID);
            ViewData["TipoSubscripcionID"] = new SelectList(_context.TipoSubscripcions, "Id", "Nombre", usuario.TipoSubscripcionID);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["TarjetumID"] = new SelectList(_context.Tarjeta, "Id", "NumeroTarjeta", usuario.TarjetumID);
            ViewData["TipoSubscripcionID"] = new SelectList(_context.TipoSubscripcions, "Id", "Nombre", usuario.TipoSubscripcionID);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombres,Apellidos,Contrasenia,CorreoElectronico,TipoSubscripcionID,FechaNacimiento,TarjetumID,Esactivo")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            ViewData["TarjetumID"] = new SelectList(_context.Tarjeta, "Id", "Id", usuario.TarjetumID);
            ViewData["TipoSubscripcionID"] = new SelectList(_context.TipoSubscripcions, "Id", "Id", usuario.TipoSubscripcionID);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
