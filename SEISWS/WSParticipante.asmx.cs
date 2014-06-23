using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;

namespace SEISWS
{

   

    /// <summary>
    /// Descripción breve de ServicioClientes
    /// </summary>
    [WebService(Namespace = "http://demo.sociosensalud.org.pe/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]


        

   public class WSParticipante : System.Web.Services.WebService
    {
        //hace referencia a la clase conexion, ahi esta la cadena de conexion y nuestros metodos
        Conexion con = new Conexion();
        DataTable dtDatos = new DataTable();
        [WebMethod]
        public String LoginUsuario(String login, String pass, int codLocal)
        {
            string msje = "";
            //int intLocal = 2;
            con.Abrir();
            msje = con.InicioSesion(login, pass, codLocal);
            con.Cerrar();
            return msje;
        }

        [WebMethod]
        public Login[] LoginUsuario1(String login, String pass, int codLocal)
        {
            con.Abrir();
            Login[] log = con.InicioSesion1(login, pass, codLocal);
            con.Cerrar();
            return log;
        }
        [WebMethod]
        public string ExisteParticipante(string DocIdentidad)
        {
            SqlConnection cn = con.conexion();
            string existe = "0";
            cn.Open();
            string sql = "select Nombres,ApellidoPaterno,ApellidoMaterno," +
                    "CodigoTipoDocumento,DocumentoIdentidad,convert(varchar(10),FechaNacimiento,103) FechaNacimiento," +
                    "Sexo from PACIENTE WHERE DocumentoIdentidad = '" + DocIdentidad + "'";

            SqlCommand cmd = new SqlCommand(sql, cn);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)    
            {
                existe = "1";
            }
            cn.Close();
            return existe;
        }


        [WebMethod]
        public Participante[] BuscarParticipante(string DocIdentidad)
        {
            SqlConnection cn = con.conexion();

            cn.Open();
            string sql = "select CONVERT(varchar(100), CodigoPaciente, 103) AS CodigoPaciente,Nombres,ApellidoPaterno,ApellidoMaterno," +
                    "CodigoTipoDocumento,DocumentoIdentidad,convert(varchar(10),FechaNacimiento,103) FechaNacimiento," +
                    "Sexo from PACIENTE WHERE DocumentoIdentidad = '" + DocIdentidad + "'";

            SqlCommand cmd = new SqlCommand(sql, cn);

            SqlDataReader reader = cmd.ExecuteReader();

            List<Participante> lista = new List<Participante>();

            while (reader.Read())
            {
                lista.Add(new Participante(
                    reader.GetString(0), 
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetInt32(4),
                    reader.GetString(5),
                    reader.GetString(6),
                    reader.GetInt32(7)));
            }

            cn.Close();

            return lista.ToArray();
        }

        [WebMethod]
        public string NuevoParticipanteSimple(string Nombres, string ApellidoP, string ApellidoM, int CodigoTipoDocumento, string DocumentoIdentidad, string FechaNacimiento, int Sexo)
        {
            string msje = "";

            con.Abrir();
            
            dtDatos = this.RegistrarPacientes(Nombres, ApellidoP, ApellidoM, CodigoTipoDocumento, DocumentoIdentidad, FechaNacimiento, Sexo);

            if (dtDatos.Rows.Count > 0)
            {
                string Resultado = dtDatos.Rows[0]["Mensaje"].ToString();
                if (Resultado == "1")
                {
                    msje = "El participante ya existe..por favor verifique bien los datos ingresados";
                }
                else
                {
                    msje = "Los datos se grabaron correctamente";
                }
            }


            return msje;
        }

        [WebMethod]
        public string NuevoParticipanteObjeto(Participante participante)
        {
            string msje = "";

            dtDatos = this.RegistrarPacientes(participante.Nombres, participante.ApellidoPaterno, participante.ApellidoMaterno, participante.CodigoTipoDocumento, participante.DocumentoIdentidad, participante.FechaNacimiento, participante.Sexo);

            if (dtDatos.Rows.Count > 0)
            {
                string Resultado = dtDatos.Rows[0]["Mensaje"].ToString();

                if (Resultado == "1")
                {
                    msje = "Los datos se grabaron correctamente";
                }
                else
                {
                    msje = "El participante ya existe..por favor verifique bien los datos ingresados";
                }
            }

            return msje;

        }

        [WebMethod]
        public Local[] ListadoLocales()
        {
            SqlConnection cn = con.conexion();

            cn.Open();

            string sql = "SELECT CodigoLocal, Nombre FROM local";

            SqlCommand cmd = new SqlCommand(sql, cn);

            SqlDataReader reader = cmd.ExecuteReader();

            List<Local> lista = new List<Local>();
            Local loc = new Local();
            loc.CodigoLocal = 0;
            loc.Nombre = "Seleccione Local";
            lista.Add(loc);

            while (reader.Read())
            {
                lista.Add(new Local(reader.GetInt32(0), reader.GetString(1)));
            }

            cn.Close();

            return lista.ToArray();
        }

        public DataTable RegistrarPacientes(string Nombres, string ApellidoP, string ApellidoM, int CodigoTipoDocumento, string DocumentoIdentidad, string FechaNacimiento, int Sexo)
        {
            SqlConnection cn = con.conexion();
            cn.Open();

            SqlDataAdapter dap = new SqlDataAdapter("SPI_PACIENTE", cn);
            DataTable dt = new DataTable();
            dap.SelectCommand.CommandType = CommandType.StoredProcedure;
            dap.SelectCommand.Parameters.AddWithValue("@Nombres", Nombres);
            dap.SelectCommand.Parameters.AddWithValue("@ApellidoP", ApellidoP);
            dap.SelectCommand.Parameters.AddWithValue("@ApellidoM", ApellidoM);
            dap.SelectCommand.Parameters.AddWithValue("@CodigoTipoDocumento", CodigoTipoDocumento);
            dap.SelectCommand.Parameters.AddWithValue("@DocumentoIdentidad", DocumentoIdentidad);
            dap.SelectCommand.Parameters.AddWithValue("@FechaNacimiento", FechaNacimiento);
            dap.SelectCommand.Parameters.AddWithValue("@Sexo", Sexo);
            dap.Fill(dt);

            cn.Close();
            return dt;

        }

        public DataTable BuscarPacxDocumento(string Documento)
        {
            SqlConnection cn = con.conexion();
            cn.Open();

            SqlDataAdapter dap = new SqlDataAdapter("SPS_PACIENTE_BS", cn);
            DataTable dt = new DataTable();
            dap.SelectCommand.CommandType = CommandType.StoredProcedure;
            dap.SelectCommand.Parameters.AddWithValue("@Documento", Documento);
            dap.Fill(dt);
            
            cn.Close(); 
            return dt;
        }

        [WebMethod]
        public Proyecto[] ListadoProyectos(string local)
        {
            SqlConnection cn = con.conexion();

            cn.Open();

            string sql = "SELECT p.CodigoProyecto,p.Nombre " + 
                "FROM LOCAL_PROYECTO AS lp INNER JOIN " +
                "PROYECTO AS p ON lp.CodigoProyecto = p.CodigoProyecto " + 
                "WHERE lp.estado=1 AND CodigoLocal = " +  local;

            SqlCommand cmd = new SqlCommand(sql, cn);

            SqlDataReader reader = cmd.ExecuteReader();

            List<Proyecto> lista = new List<Proyecto>();

            while (reader.Read())
            {
                lista.Add(new Proyecto(reader.GetInt32(0), reader.GetString(1)));
            }

            cn.Close();

            return lista.ToArray();
        }

        [WebMethod]
        public Visita[] ListadoGrupoVisitas(string CodigoPaciente, int CodigoLocal, int CodigoProyecto)
        {
            SqlConnection cn = con.conexion();

            cn.Open();
            SqlDataAdapter dap = new SqlDataAdapter("SPS_DATOS_SEDE_PROY1", cn);
            DataTable dt = new DataTable();
            dap.SelectCommand.CommandType = CommandType.StoredProcedure;
            dap.SelectCommand.Parameters.AddWithValue("@CodigoPaciente", CodigoPaciente);
            dap.SelectCommand.Parameters.AddWithValue("@CodigoLocal", CodigoLocal);
            dap.SelectCommand.Parameters.AddWithValue("@CodigoProyecto", CodigoProyecto);
            dap.Fill(dt);

            DataTableReader reader = dt.CreateDataReader();

            List<Visita> lista = new List<Visita>();

            while (reader.Read())
            {
                lista.Add(new Visita(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetString(2), 
                    reader.GetInt32(3),
                    reader.GetString(4),
                    reader.GetBoolean(5),
                    reader.GetInt32(6)));
            }

            cn.Close();
            return lista.ToArray();
        }
        [WebMethod]
        public int InsertarVisitas(int CodigoLocal, int CodigoProyecto, int CodigoGrupoVisita, int CodigoVisita, string CodigoPaciente, string FechaVisita, string HoraCita, int CodigoUsuario)
        {
            SqlConnection cn = con.conexion();
            SqlCommand cmd = new SqlCommand("SPI_VISITAS", cn);
            SqlTransaction trx;
            int intretorno;
            string strRespuesta;

            try
            {
                cn.Open();
                trx = cn.BeginTransaction();
                cmd.Transaction = trx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CodigoLocal", SqlDbType.Int)).Value = CodigoLocal;
                cmd.Parameters.Add(new SqlParameter("@CodigoProyecto", SqlDbType.Int)).Value = CodigoProyecto;
                cmd.Parameters.Add(new SqlParameter("@CodigoGrupoVisita", SqlDbType.Int)).Value = CodigoGrupoVisita;
                cmd.Parameters.Add(new SqlParameter("@CodigoVisita", SqlDbType.Int)).Value = CodigoVisita;
                cmd.Parameters.Add(new SqlParameter("@CodigoPaciente", SqlDbType.VarChar, 50)).Value = CodigoPaciente;
                cmd.Parameters.Add(new SqlParameter("@FechaVisita", SqlDbType.VarChar, 10)).Value = FechaVisita;
                cmd.Parameters.Add(new SqlParameter("@HoraCita", SqlDbType.VarChar, 5)).Value = HoraCita;
                cmd.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.Int)).Value = CodigoUsuario;
                cmd.Transaction = trx;
                intretorno = cmd.ExecuteNonQuery();
                trx.Commit();
                cn.Close();
                return intretorno;
            }
            catch (SqlException sqlException)
            {
                strRespuesta = sqlException.Message.ToString();
                cn.Close();
                return -1;
            }
            catch (Exception exception)
            {
                strRespuesta = exception.Message.ToString();
                cn.Close();
                return -1;
            }
        }

        [WebMethod]
        public Visitas[] ListadoVisitas(string CodigoPaciente)
 
        {
            SqlConnection cn = con.conexion();
            cn.Open();
//            SELECT        PY.Nombre AS Proyecto, E.Nombre AS Visita, SUBSTRING(DATENAME(dw, V.FechaVisita), 1, 3) + ' ' + CONVERT(varchar(10), V.FechaVisita, 103) 
//                         AS FechaVisita, CONVERT(varchar(5), V.HoraInicio, 108) AS HoraCita, EC.Descripcion AS EstadoVisita
//            FROM         VISITAS AS V INNER JOIN PROYECTO AS PY ON V.CodigoProyecto = PY.CodigoProyecto AND V.Estado = 1 
//                         INNER JOIN VISITA AS E ON V.CodigoProyecto = E.CodigoProyecto AND V.CodigoGrupoVisita = E.CodigoGrupoVisita AND V.CodigoVisita = E.CodigoVisita 
//                         INNER JOIN PARAMETROS AS EC ON V.CodigoEstadoVisita = EC.CodigoParametro AND EC.Codigo = 5
//            WHERE        (V.CodigoPaciente = 'b0875796-a823-455b-a48a-4da85d050fca') AND (V.CodigoLocal = 1) AND (V.CodigoProyecto = 1)
            string sql = "SELECT PY.Nombre AS Proyecto, E.Nombre AS Visita, " +
                "SUBSTRING(DATENAME(dw, V.FechaVisita), 1, 3) + ' ' + CONVERT(varchar(10), V.FechaVisita, 103) AS FechaVisita," +
                "CONVERT(varchar(5), V.HoraInicio, 108) AS HoraCita, EC.Descripcion AS EstadoVisita " +
                "FROM  VISITAS AS V INNER JOIN PROYECTO AS PY ON V.CodigoProyecto = PY.CodigoProyecto AND V.Estado = 1 " +
                "INNER JOIN VISITA AS E ON V.CodigoProyecto = E.CodigoProyecto AND V.CodigoGrupoVisita = E.CodigoGrupoVisita AND V.CodigoVisita = E.CodigoVisita " +
                "INNER JOIN PARAMETROS AS EC ON V.CodigoEstadoVisita = EC.CodigoParametro AND EC.Codigo = 5 " +
                "WHERE V.CodigoPaciente = '" + CodigoPaciente + "'";

            SqlCommand cmd = new SqlCommand(sql, cn);

            SqlDataReader reader = cmd.ExecuteReader();

            List<Visitas> lista = new List<Visitas>();

            while (reader.Read())
            {
                lista.Add(new Visitas(
                    reader.GetString(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4)));
            }

            cn.Close();
            return lista.ToArray();
        }
        [WebMethod]
        public String ListadoFormatos(String CodigoUsuario,int CodigoLocal, int CodigoProyecto, int CodigoGrupoVisita, int CodigoVisita)
        {
            SqlConnection cn = con.conexion();
            //WHERE        (F.IdTipoDeFormato = '04') AND (U.CodigoUsuarioSP = '0') AND (PU.CodigoProyecto = 5) AND (U.CodigoLocal = 1) AND (R.CodigoVisita = 1) AND 
            //             
            cn.Open();
            string sql = "SELECT F.IdFormatoNemotecnico AS FormID " +
                         "FROM SEIS_DATA.dbo.Usuarios AS U INNER JOIN " +
                         "SEIS_DATA.dbo.Proyecto_Usuario AS PU ON U.CodigoUsuario = PU.CodigoUsuario INNER JOIN " +
                         "SEIS_DATA.dbo.RutaServicioFormato AS R ON PU.CodigoProyecto = R.CodigoProyecto INNER JOIN " +
                         "SEIS_DATA.dbo.Formato AS F ON R.IdFormato = F.IdFormato " +
                         "WHERE F.IdTipoDeFormato = '04' AND U.CodigoUsuarioSP = '" + CodigoUsuario + "' AND " +
                            "PU.CodigoProyecto = " + CodigoProyecto + " AND U.CodigoLocal = " + CodigoLocal + " AND " +
                            "R.CodigoVisita = " + CodigoVisita + " AND R.CodigoGrupoVisita = "+CodigoGrupoVisita;
            SqlCommand cmd = new SqlCommand(sql, cn);

            SqlDataReader reader = cmd.ExecuteReader();

            String lstFormatos = "";

            while (reader.Read())
            {
                lstFormatos += reader.GetString(0) + "/";
            }

            cn.Close();

            return lstFormatos;
        }
        [WebMethod]
        public Visitas1[] ListadoVisitas1(string CodigoPaciente)
        {
            SqlConnection cn = con.conexion();
            cn.Open();
           string sql = "SELECT PY.Nombre AS Proyecto, E.Nombre AS Visita, " +
                "SUBSTRING(DATENAME(dw, V.FechaVisita), 1, 3) + ' ' + CONVERT(varchar(10), V.FechaVisita, 103) AS FechaVisita," +
                "CONVERT(varchar(5), V.HoraInicio, 108) AS HoraCita, EC.Descripcion AS EstadoVisita ,CONVERT(varchar(5), V.CodigoProyecto, 103) AS CodigoProyecto," + 
                "CONVERT(varchar(5), V.CodigoGrupoVisita, 103) AS CodigoGrupoVisita,CONVERT(varchar(5), V.CodigoVisita, 103) AS CodigoVisita " +
                "FROM  VISITAS AS V INNER JOIN PROYECTO AS PY ON V.CodigoProyecto = PY.CodigoProyecto AND V.Estado = 1 " +
                "INNER JOIN VISITA AS E ON V.CodigoProyecto = E.CodigoProyecto AND V.CodigoGrupoVisita = E.CodigoGrupoVisita AND V.CodigoVisita = E.CodigoVisita " +
                "INNER JOIN PARAMETROS AS EC ON V.CodigoEstadoVisita = EC.CodigoParametro AND EC.Codigo = 5 " +
                "WHERE V.CodigoPaciente = '" + CodigoPaciente + "'";

            SqlCommand cmd = new SqlCommand(sql, cn);

            SqlDataReader reader = cmd.ExecuteReader();

            List<Visitas1> lista = new List<Visitas1>();

            while (reader.Read())
            {
                lista.Add(new Visitas1(
                    reader.GetString(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4),
                    reader.GetString(5),
                    reader.GetString(6),
                    reader.GetString(7)));
            }

            cn.Close();
            return lista.ToArray();
        }
    }
}

