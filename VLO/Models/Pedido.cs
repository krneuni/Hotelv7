using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class Pedido
    {

        [Key]
        public int IdPedido { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un valor numérico.")]
        [Display(Name = "Cantidad de comensales")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MinLength(3, ErrorMessage = "No puede ingresar menos de 3 caracteres")]
        [MaxLength(75, ErrorMessage = "No puede ingresar más de 75 caracteres")]
        [Display(Name = "Nombre de Cliente")]
        public string Cliente { get; set; }

        public int Estado { get; set; }
        
        //Relaciones

        //Numero de mesa
        public int IdMesa { get; set; }
        public virtual Mesa Mesa { get; set; }

        //Usuarios
        public int IdUser { get; set; }
        public virtual Usuarios Usuarios { get; set; }
        

    }
}