using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class DetalleCompra
    {

        [Key]
        public int IdDetalle { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Cantidad")]
        [Range(0, double.MaxValue, ErrorMessage = "Debe ingresar un valor numérico.")]
        public double Cantidad { get; set; }
        
        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Precio Unitario ")]
        public double PrecioUnit { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Compra ")]
        public string FechaCompra { get; set; }
        
        [Display(Name = "Precio Total ")]
        public double PrecioTotal { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MinLength(3, ErrorMessage = "No puede ingresar menos de 3 caracteres")]
        [Display(Name = "Código de Factura")]
        public string Codigo { get; set; }

        //Relacione con la tabla Proveedor
        public int IdProveedor { get; set; }
        public virtual Proveedores Proveedores { get; set; }

        //Relacion con la tabla Productos
        public int IdProducto { get; set; }
        public virtual Productos Productos { get; set; }


    }
}