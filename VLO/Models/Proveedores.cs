using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class Proveedores
    {
        [Key]
        public int IdProveedor { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        [MinLength(3, ErrorMessage = "No puede ingresar menos de 3 caracteres")]
        [MaxLength(75, ErrorMessage = "No puede ingresar más de 75 caracteres")]
        [Display(Name = "Nombre Proveedor ")]
        public string Nombre { get; set; }

        //[Required(ErrorMessage = "Este campo es requerido")]
        [MinLength(3, ErrorMessage = "No puede ingresar menos de 3 caracteres")]
        [MaxLength(150, ErrorMessage = "No puede ingresar más de 150 caracteres")]
        [Display(Name = "Direccion: ")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.PhoneNumber, ErrorMessage ="Número no válido")]
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{4})$",
                   ErrorMessage = "Ingrese un número válido")]
        [Display(Name = "Teléfono: ")]
        public string Telefono { get; set; }

        
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Cuenta no valida")]
        [Display(Name = "Correo electronico")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MinLength(3, ErrorMessage = "No puede ingresar menos de 3 caracteres")]
        [MaxLength(75, ErrorMessage = "No puede ingresar más de 75 caracteres")]
        [Display(Name = "Nombre de contacto ")]
        public string NombreContacto { get; set; }

        [Display(Name = "Estado")]
        public bool Estado { get; set; }

        public virtual List<DetalleCompra> DetalleCompra { get; set; }
    }
}