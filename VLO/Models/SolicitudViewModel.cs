using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class SolicitudViewModel
    {
        public List<Prestamos> Prestamos { get; set; }
        public List<Usuarios> Usuarios { get; set; }
        public List<Productos> Productos { get; set; }
        public List<Categoria> Categorias { get; set; }
    }
}