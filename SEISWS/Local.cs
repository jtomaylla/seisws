using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEISWS
{

    public class Local
    {
        public int CodigoLocal { get; set; }
        public string Nombre { get; set; }

        public Local()
        {
            this.CodigoLocal = 0;
            this.Nombre = "";
        }

        public Local(int codigolocal, string nombre)
        {
            this.CodigoLocal = codigolocal;
            this.Nombre = nombre;

        }
    }
}