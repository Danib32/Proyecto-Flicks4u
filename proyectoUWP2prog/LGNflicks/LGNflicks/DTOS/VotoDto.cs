using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGNflicks.DTOS
{
    class VotoDto
    {
        public int PeliculaID { get; set; }
        public string UsuarioID { get; set; }
        public bool Delete { get; set; }
    }
}
