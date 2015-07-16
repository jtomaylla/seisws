using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEISWS
{
    //Comentario

    public class Geofence
    {
        public int codigogeofence{ get; set; }
        public int codigolocal { get; set; }
        public string nombre { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string radio { get; set; }
        public string duracionexpiracion { get; set; }
        public int tipotransicion { get; set; }

        public Geofence()
        {
            this.codigogeofence = 0;
            this.codigolocal = 0;
            this.nombre = "";
            this.latitud = "";
            this.longitud = "";
            this.radio = "";
            this.duracionexpiracion = "";
            this.tipotransicion = 0;
        }

        public Geofence(
            int codigogeofence,
            int codigolocal, 
            string nombre,
            string latitud,
            string longitud,
            string radio,
            string duracionexpiracion,
            int tipotransicion)
        {
            this.codigogeofence = codigogeofence;
            this.codigolocal = codigolocal;
            this.nombre = nombre;
            this.latitud = latitud;
            this.longitud = longitud;
            this.radio = radio;
            this.duracionexpiracion = duracionexpiracion;
            this.tipotransicion = tipotransicion;

        }
    }
}