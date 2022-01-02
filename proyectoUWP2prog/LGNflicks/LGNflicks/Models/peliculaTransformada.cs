using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace LGNflicks.Models
{
    class peliculaTransformada
    {
        public peliculaTransformada(int ID,string nombre, string sinopsis, DateTime lanzamiento, string escritores, string descripcion, BitmapImage imagen, string generoNombre, int cantVotos)
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
        }
        public int peliculaID { get; set; }
        public string Nombre { get; set; }
        public string Sinopsis { get; set; }
        public DateTime Lanzamiento { get; set; }
        public string Escritores { get; set; }
        public string Descripcion { get; set; }
        public BitmapImage Imagen { get; set; }
        public string GeneroNombre { get; set; }
        public int CantVotos { get; set; }
    }
}
