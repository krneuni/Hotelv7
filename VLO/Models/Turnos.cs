using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class Turnos
    {
        [Key]
        public int IdTurno { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MinLength(3, ErrorMessage = "No puede ingresar menos de 3 caracteres")]
        [MaxLength(25, ErrorMessage = "No puede ingresar más de 25 caracteres")]
        [Display(Name = "Turno")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Hora de inicio")]
        [DataType(DataType.Time)]
        public string HoraInicial { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Hora de finalización")]
        [DataType(DataType.Time)]
        public string HoraFinal { get; set; }


        //Relaciones 
        public virtual List<AsignacionTurno> AsignacionTurno { get; set; }
    }
}