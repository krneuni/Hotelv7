using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class Prestamos
    {
        [Key]
        public int IdPrestamo { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Cantidad a retirar")]
        [Range(0, double.MaxValue, ErrorMessage = "Debe ingresar un valor numérico.")]
        public double Cantidad { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Cantidad a devolver")]
        [Range(0, double.MaxValue, ErrorMessage = "Debe ingresar un valor numérico.")]
        public double CantidadDevuelta { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Ingresar una fecha valida")]
        [Display(Name = "Fecha de prestamo ")]
        public string Fecha { get; set; }

        public int Estado { get; set; }
        //Relaciones
        public int IdProducto { get; set; }
        public virtual Productos Productos { get; set; }
        public int IdUser { get; set; }
        public virtual Usuarios Usuarios { get; set; }

    }
}