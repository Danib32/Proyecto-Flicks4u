using System;

namespace DTOS
{
    public class DTOSUsuario
    {
        public DTOSUsuario(int id, String contra, string correo, int tiposuscripcion)
        {
            IDusuario = id;
            Contrasenia = contra;
            CorreoElectronico = correo;
            tipoSuscripcion = tiposuscripcion;
        }
        public int IDusuario { get; set; }
        public string Contrasenia { get; set; }
        public string CorreoElectronico { get; set; }
        public int tipoSuscripcion { get; set; }
    }
}
