using Flicks4u.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flix4u.Controllers
{
    public class LoginController : Controller
    {
        public static string LoginString;
        private readonly AplicacionPeliculasContext _context;
        public LoginController(AplicacionPeliculasContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(String Txt_Usuariolog, String Txt_Password)
        {
            if (Txt_Usuariolog == null || Txt_Password == null)
            {
                return NotFound();
            }
            var userlog = (from ObjUsuario in _context.Usuarios
                           where ObjUsuario.CorreoElectronico == Txt_Usuariolog
                           select ObjUsuario).FirstOrDefault();
            var passlog = (from ObjPasslog in _context.Usuarios
                           where ObjPasslog.Contrasenia == Txt_Password
                           select ObjPasslog).FirstOrDefault();
            if (userlog == null) 
            {
                ViewBag.MensajeErrorCorreo = "Usuario incorrecto o no registrado";
                return View();
            }
            if (passlog == null)
            {
                ViewBag.MensajeErrorPassLog = "Contraseña incorrecta";
                return View();
            }
            if(userlog.TipoSubscripcionID==1)
            {
                ViewBag.MensajeErrorCorreo = "La cuenta no es de administrador";
                return View();
            }
            LoginString = userlog.CorreoElectronico;
            return RedirectToAction("Index", "Administrador");
        }

        public IActionResult CuentaCreada()
        {
            return View();
        }

    }
}
