using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEISWS
{
    public class Login
    {
        public int CodigoUsuario { get; set; }
        public string Mensaje { get; set; }

        public Login()
        {
            this.CodigoUsuario = 0;
            this.Mensaje = "";
        }

        public Login(int CodigoUsuario, string Mensaje)
        {
            this.CodigoUsuario = CodigoUsuario;
            this.Mensaje = Mensaje;

        }
    }
}