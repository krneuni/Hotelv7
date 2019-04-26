using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class Roles
    {

        [Key]
        public int IdRol { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(50, ErrorMessage = "Usuario debe tener al menos 50 caracteres mínimo", MinimumLength = 5)]
        [Display(Name = "Tipo de usuario ")]
        public string Tipo { get; set; }
        

        //Relaciones
        public virtual List<Usuarios> Usuarios { get; set; }
    }
}