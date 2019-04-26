using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class Menu
    {
        [Key]
        public int IdMenu{ get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MinLength(3, ErrorMessage = "No puede ingresar menos de 3 caracteres")]
        [MaxLength(75, ErrorMessage = "No puede ingresar más de 75 caracteres")]
        [Display(Name = "Nombre Menú ")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.Currency)]
        [Display(Name = "Precio ")]
        public double Precio { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MinLength(3, ErrorMessage = "No puede ingresar menos de 3 caracteres")]
        [MaxLength(500, ErrorMessage = "No puede ingresar más de 500 caracteres")]
        [DataType(DataType.Text)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        

        //Relación con Tipo de Menu
        public int IdTipoMenu { get; set; }
        public virtual TipoMenu TipoMenu { get; set; }

        //Listar tabla en DetallePedido
        public virtual List<DetallePedido> DetallePedido { get; set; }

    }
}