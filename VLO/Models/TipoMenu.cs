using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class TipoMenu
    {
        [Key]
        public int IdTipoMenu { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MinLength(3, ErrorMessage = "No puede ingresar menos de 3 caracteres")]
        [MaxLength(75, ErrorMessage = "No puede ingresar más de 75 caracteres")]
        [Display(Name = "Tipo Menu")]
        public string Nombre { get; set; }

        //Relación con Productos
        public virtual List<Menu> Menus { get; set; }
    }
}