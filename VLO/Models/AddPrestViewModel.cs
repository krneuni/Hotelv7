using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class AddPrestViewModel
    {
        public List<int> id { get; set; }
        public List<int> cantidad { get; set; }
        public List<int> unidadmedida { get; set; }


        public int estado { get; set; }          
    }
}