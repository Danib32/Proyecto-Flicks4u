using Flicks4u.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Flix4u.Controllers;

namespace Flicks4u.Controllers
{
    public class AdministradorController : Controller
    {
        private readonly ILogger<AdministradorController> _logger;
        private readonly AplicacionPeliculasContext _context;
        public string correo = LoginController.LoginString;

        public AdministradorController(ILogger<AdministradorController> logger, AplicacionPeliculasContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.NombreLog = correo;
            List<Pelicula> peliculas = await _context.Peliculas.Where(p => p.Esactivo).ToListAsync();
            return View(peliculas);
        }

        //public async Task<IActionResult> Votar(int id)
        //{
        //    var pelicula = await _context.Peliculas.FindAsync(id);
        //    var usuario = (from objusuario in _context.Usuarios
        //                   where objusuario.CorreoElectronico==correo
        //                   select objusuario).FirstOrDefault();
        //    VotoPelicula voto = new VotoPelicula();
        //    voto.Pelicula = pelicula;
        //    voto.PeliculaID = pelicula.Id;
        //    voto.Valor += 1;
        //    voto.Usuario = usuario;
        //    voto.UsuarioID = usuario.Id;
        //    _context.Add(voto);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
