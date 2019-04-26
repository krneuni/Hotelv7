using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class OrdenesViewModel
    {
        public List<Menu> Menus { get; set; }
        public List<TipoMenu> TiposMenu { get; set; }
        public List<Productos> Productos { get; set; }
        public List<Pedido> Pedido { get; set; }
        public List<DetallePedido> DetallePedido { get; set; }
        public List<Mesa> Mesa { get; set; }
    }
}