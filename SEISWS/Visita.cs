using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEISWS
{
    public class Visita
    {
        public int CodigoProyecto { get; set; }
        public int CodigoGrupoVisita { get; set; }
        public string NombreGrupoVisita { get; set; }
        public int CodigoVisita { get; set; }
        public string DescripcionVisita { get; set; }
        public Boolean GenerarAuto { get; set; }
        public int Dependiente { get; set; }


        public Visita()
        {
            this.CodigoProyecto = 0;
            this.CodigoGrupoVisita = 0;
            this.NombreGrupoVisita = "";
            this.CodigoVisita = 0;
            this.DescripcionVisita = "";
            this.GenerarAuto = false;
            this.Dependiente = 0;
        }

        public Visita(int CodigoProyecto,
            int CodigoGrupoVisita,
            string NombreGrupoVisita,
            int CodigoVisita,
            string DescripcionVisita,
            Boolean GenerarAuto,
            int Dependiente)
        {
            this.CodigoProyecto = CodigoProyecto;
            this.CodigoGrupoVisita = CodigoGrupoVisita;
            this.NombreGrupoVisita = NombreGrupoVisita;
            this.CodigoVisita = CodigoVisita ;
            this.DescripcionVisita = DescripcionVisita;
            this.GenerarAuto = GenerarAuto ;
            this.Dependiente = Dependiente;

        }
    }
}