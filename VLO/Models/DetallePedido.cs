using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class DetallePedido
    {
        [Key]
        public int IdDetalle { get; set; }

        //Relaciones
        //Tabla Menu
        public int IdMenu { get; set; }
        public virtual Menu Menu { get; set; }
        //Tabla Pedido
        public int IdPedido { get; set; }
        public virtual Pedido Pedido { get; set; }
        public virtual List<Factura> Factura { get; set; }

        public int cantidad { get; set; }
        public int sesion { get; set; }
        public int Estado { get; set; }
        public string Termino { get; set; }


    }
}