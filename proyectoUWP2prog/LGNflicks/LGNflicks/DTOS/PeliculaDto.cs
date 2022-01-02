using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGNflicks.DTOS
{
    class PeliculaDto
    {
        public PeliculaDto(int ID, string nombre, string sinopsis, DateTime lanzamiento, string escritores, string descripcion, byte[] imagen, string generoNombre, int cantVotos, bool activo)
        {
            peliculaID = ID;
            Nombre = nombre;
            Sinopsis = sinopsis;
            Lanzamiento = lanzamiento;
            Escritores = escritores;
            Descripcion = descripcion;
            Imagen = imagen;
            GeneroNombre = generoNombre;
            CantVotos = cantVotos;
            esActivo = activo;
        }
        public int peliculaID { get; set; }
        public string Nombre { get; set; }
        public string Sinopsis { get; set; }
        public DateTime Lanzamiento { get; set; }
        public string Escritores { get; set; }
        public string Descripcion { get; set; }
        public byte[] Imagen { get; set; }
        public string GeneroNombre { get; set; }
        public int CantVotos { get; set; }
        public bool esActivo { get; set; }
    }
}
