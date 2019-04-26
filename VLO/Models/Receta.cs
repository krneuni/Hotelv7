using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class Receta
    {

        [Key]
        public int IdReceta { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Cantidad Utilizada")]
        [Range(0, double.MaxValue, ErrorMessage = "Debe ingresar un valor numérico.")]
        public double CantidadUtilizada { get; set; }

        //Relación con Productos
        public int IdProducto { get; set; }
        public virtual Productos Productos { get; set; }

        //Relación con Menu
        public int IdMenu { get; set; }
        public virtual Menu Menu { get; set; }
    }
}