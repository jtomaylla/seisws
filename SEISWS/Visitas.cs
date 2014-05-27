using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEISWS
{
    public class Visitas
    {
        public string Proyecto { get; set; }
        public string Visita { get; set; }
        public string FechaVisita { get; set; }
        public string HoraCita { get; set; }
        public string EstadoVisita { get; set; }


        public Visitas()
        {
            this.Proyecto = "";
            this.Visita = "";
            this.FechaVisita = "";
            this.HoraCita = "";
            this.EstadoVisita = "";
        }

        public Visitas(
            string Proyecto,
            string Visita,
            string FechaVisita,
            string HoraCita,
            string EstadoVisita)
        {
            this.Proyecto = Proyecto;
            this.Visita = Visita;
            this.FechaVisita = FechaVisita;
            this.HoraCita = HoraCita;
            this.EstadoVisita = EstadoVisita;
        }
    }

}