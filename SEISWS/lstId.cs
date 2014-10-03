using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEISWS
{
    public class lstId
    {
        public string CodigoProyecto { get; set; }
        public string Proyecto { get; set; }
        public string IdTAM { get; set; }
        public string IdENR { get; set; }

        public lstId()
        {
            this.CodigoProyecto = "";
            this.Proyecto = "";
            this.IdTAM = "";
            this.IdENR = "";
        }

        public lstId(string CodigoProyecto, string Proyecto, string IdTAM, string IdENR)
        {
            this.CodigoProyecto = CodigoProyecto;
            this.Proyecto = Proyecto;
            this.IdTAM = IdTAM;
            this.IdENR = IdENR;

        }
    }
}