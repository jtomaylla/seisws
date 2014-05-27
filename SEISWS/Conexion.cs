using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Data;

namespace SEISWS
{
   public class Conexion
    {
       SqlConnection con;
       string Conn;
       public Conexion()
        {
            if (con == null)
                con = conexion();
        }

       public SqlConnection conexion()
       {
           Conn = System.Configuration.ConfigurationManager.AppSettings["eConnectionString"];
           return new SqlConnection(Conn);
       }

        public void Abrir()
        {
            if (con.State == ConnectionState.Closed) con.Open();
        }

        public void Cerrar()
        {
            if (con.State == ConnectionState.Open) con.Close();
        }

       // METODOS DE CONEXION
        public String InicioSesion(String login, String pass, int codLocal)
        {
            String msje = "";
            String mensaje = "";
            //SqlCommand cmd;
            DataTable dt = new DataTable();
            try
            { 
                Abrir();
                dt = this.GetValidarUsuario(login, pass, codLocal);
                mensaje = dt.Rows[0]["Respuesta"].ToString();
                if (mensaje == "1")
                {
                    msje = "Usuario no Encontrado. Por favor, trate de nuevo.";
                }
                if (mensaje == "2")
                {
                    msje = "Contraseña errada. Por favor, trate de nuevo.";
                }
                if (mensaje == "3")
                {
                    msje = "Contraseña caducada";
                }
                if (mensaje == "4")
                {
                    msje = "Gracias por Iniciar Sesion";
                }        
                Cerrar();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return msje;
        }

        public Login[] InicioSesion1(String login, String pass, int codLocal)
        {
            String msje = "";
            String mensaje = "";
            List<Login> lista = new List<Login>();
            //SqlCommand cmd;
            DataTable dt = new DataTable();
            try
            {
                Abrir();
                dt = this.GetValidarUsuario(login, pass, codLocal);
                mensaje = dt.Rows[0]["Respuesta"].ToString();
                if (mensaje == "1")
                {
                    msje = "Usuario no Encontrado. Por favor, trate de nuevo.";
                }
                if (mensaje == "2")
                {
                    msje = "Contraseña errada. Por favor, trate de nuevo.";
                }
                if (mensaje == "3")
                {
                    msje = "Contraseña caducada";
                }
                if (mensaje == "4")
                {
                    msje = "Gracias por Iniciar Sesion";
                }
                Login log = new Login();
                if (mensaje == "1" || mensaje == "2")
                {
                    log.CodigoUsuario = 0;
                }else{
                    log.CodigoUsuario = Convert.ToInt32(dt.Rows[0]["CodigoUsuario"]);
                }
                log.Mensaje = msje;
                lista.Add(log);
                Cerrar();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return lista.ToArray();
        }

        public DataTable GetValidarUsuario(string strUsu, string strCont, int intIdLocal)
        {
            SqlDataAdapter dap = new SqlDataAdapter("SPS_VALIDAR_USUARIO", con);
            DataTable dt = new DataTable();
            dap.SelectCommand.CommandType = CommandType.StoredProcedure;
            dap.SelectCommand.Parameters.AddWithValue("@LoginUsuario", strUsu);
            dap.SelectCommand.Parameters.AddWithValue("@ContrasenaUsuario", strCont);
            dap.SelectCommand.Parameters.AddWithValue("@CodigoLocal", intIdLocal);
            dap.Fill(dt);
            return dt;
        }
    }
}