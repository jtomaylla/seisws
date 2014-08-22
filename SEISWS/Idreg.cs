using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEISWS
{
    public class Idreg
    {
        public string NombreCompleto { get; set; }
        public int TipoTAM { get; set; }
        public string IdTAM { get; set; }
        public int TipoENR { get; set; }
        public string IdENR { get; set; }

        public Idreg()
        {
            this.NombreCompleto = "";
            this.TipoTAM = 0;
            this.IdTAM = "";
            this.TipoENR = 0;
            this.IdENR = "";
        }

        public Idreg(string NombreCompleto, int TipoTAM, string IdTAM, int TipoENR, string IdENR)
        {
            this.NombreCompleto = NombreCompleto;
            this.TipoTAM = TipoTAM;
            this.IdTAM = IdTAM;
            this.TipoENR = TipoENR;
            this.IdENR = IdENR;

        }
    }
}