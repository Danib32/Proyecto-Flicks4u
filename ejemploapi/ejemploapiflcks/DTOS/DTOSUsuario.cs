using System;

namespace DTOS
{
    public class DTOSUsuario
    {
        public DTOSUsuario(String contra, string correo)
        {
            Contrasenia = contra;
            CorreoElectronico = correo;
        }
        public string Contrasenia { get; set; }

        public string CorreoElectronico { get; set; }
    }
}
