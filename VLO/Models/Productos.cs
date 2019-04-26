using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class Productos
    {

        [Key]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MinLength(3, ErrorMessage = "No puede ingresar menos de 3 caracteres")]
        [MaxLength(75, ErrorMessage = "No puede ingresar más de 75 caracteres")]
        [Display(Name = "Nombre Producto ")]
        public string Nombre { get; set; }
        

        [MinLength(3, ErrorMessage = "No puede ingresar menos de 3 caracteres")]
        [MaxLength(75, ErrorMessage = "No puede ingresar más de 75 caracteres")]
        [Display(Name = "Marca")]
        public string Marca { get; set; }
         

        [Display(Name = "Cantidad")]
        [Range(0, float.MaxValue, ErrorMessage = "Ingrese una cantidad válida")]
        public double Cantidad { get; set; }

        
        [DataType(DataType.Date, ErrorMessage = "Ingresar una fecha valida")]
        [Display(Name = "Fecha Vencimiento: ")]
        public string Fecha { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Cantidad Mínima")]
        [Range(0, double.MaxValue, ErrorMessage = "Debe ingresar un valor numérico.")]
        public double CantidadMinima { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MinLength(3, ErrorMessage = "No puede ingresar menos de 3 caracteres")]
        [MaxLength(25, ErrorMessage = "No puede ingresar más de 25 caracteres")]
        [Display(Name = "Unidad de medida")]
        public string UnidadMedida { get; set; }
        
        [Display(Name = "Estado")]
        public bool Estado { get; set; }


        //Relación con categoria de Producto
        public int IdCategoria { get; set; }
        public virtual Categoria Categoria { get; set; }

        //Listar los datos en las siguientes tablas
        public virtual List<DetalleCompra> DetalleCompra { get; set; }
        public virtual List<Receta> Receta { get; set; }
        public virtual List<Prestamos> Prestamos { get; set; }
        

    }
}