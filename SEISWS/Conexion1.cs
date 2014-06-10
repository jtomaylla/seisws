using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Data;
namespace SEISWS
{
    public class Conexion1
    {
       SqlConnection con;
       string Conn;
       public Conexion1()
        {
            if (con == null)
                con = conexion();
        }

       public SqlConnection conexion()
       {
           Conn = System.Configuration.ConfigurationManager.AppSettings["eConnectionString1"];
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
    }
}