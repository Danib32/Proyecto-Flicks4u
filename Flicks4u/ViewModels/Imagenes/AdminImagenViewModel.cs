using Microsoft.AspNetCore.Http;

namespace Flicks4u.ViewModels.Imagenes
{
    public class AdminImagenViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public IFormFile Archivo { get; set; }
    }
}
