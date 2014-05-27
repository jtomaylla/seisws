using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEISWS
{
    public class Proyecto
    {
        public int CodigoProyecto { get; set; }
        public string Nombre { get; set; }

        public Proyecto()
        {
            this.CodigoProyecto = 0;
            this.Nombre = "";
        }

        public Proyecto(int codigoProyecto, string nombre)
        {
            this.CodigoProyecto = codigoProyecto;
            this.Nombre = nombre;

        }
    }
}