using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class AsignacionTurno
    {
        [Key]
        public int IdAsignacion { get; set; }


        public int IdTurno { get; set; }
        public virtual Turnos Turnos { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public string Fecha { get; set; }

        public int IdUser { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}