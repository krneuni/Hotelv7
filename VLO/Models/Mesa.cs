using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class Mesa
    {
        [Key]
        public int IdMesa { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Número de mesa ")]
        public string NumMesa { get; set; }

        //[Required(ErrorMessage = "Este campo es requerido")]
        //[Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un valor numérico.")]
        //[Display(Name ="Número de mesa")]
        //public int NumMesa { get; set; }
        
        [Display(Name = "Estado")]
        public bool Estado { get; set; }

        public virtual List<Pedido> Pedido { get; set; }


    }
}