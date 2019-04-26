using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class AddOrdenViewModel
    {
        public List<int> id { get; set; }
        public List<int> cantidad { get; set; }
        public int mesa { get; set; }
        public String cliente { get; set; }
        public int numpersonas { get; set; }
        public int estado { get; set; }
        public String termino { get; set; }
    }
}