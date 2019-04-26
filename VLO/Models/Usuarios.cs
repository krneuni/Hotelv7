using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class Usuarios
    {
        [Key]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(150, ErrorMessage ="Sólo 150 caracteres")]
        [Display(Name = "Nombre Completo")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MinLength(3, ErrorMessage = "No puede ingresar menos de 3 caracteres")]
        [MaxLength(50, ErrorMessage = "No puede ingresar más de 50 caracteres")]
        [Display(Name = "Nombre de usuario ")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MinLength(3, ErrorMessage = "No puede ingresar menos de 3 caracteres")]
        [MaxLength(50, ErrorMessage = "No puede ingresar más de 50 caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña ")]
        public string Password { get; set; }

       

        //Tabla Roles
        public int IdRol { get; set; }
        public virtual Roles Roles { get; set; }


        ////Relaciones
        //Listar la tabla en las siguientes tablas
        public virtual List<Pedido> Pedido { get; set; }
        public virtual List<AsignacionTurno> AsignacionTurnos { get; set; }
        public virtual List<Prestamos> Prestamos { get; set; }

    }
}