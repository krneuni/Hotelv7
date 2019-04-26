using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class Factura
    {
        [Key]
        public int NumFactura { get; set; }
        
        //Relacion con DetallePedido
        public int IdDetalle { get; set; }
        public virtual DetallePedido DetallePedido { get; set; }
        //Fin de la relación

        [DataType(DataType.Currency)]
        [Display(Name = "Precio Total")]
        public double Precio { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Descuento")]
        public double Descuento { get; set; }

        
        [DataType(DataType.Date, ErrorMessage = "Ingresar una fecha valida")]
        [Display(Name = "Fecha de facturación ")]
        public string FechaFactura { get; set; }

        
        public double Propina { get; set; }
        public string Descripcion { get; set; }


    }
}