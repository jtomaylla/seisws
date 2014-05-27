using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEISWS
{
    public class Participante
    {
        public string CodigoPaciente { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }        
        public string ApellidoMaterno { get; set; }
        public int CodigoTipoDocumento { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string FechaNacimiento { get; set; }
        public int Sexo { get; set; }

        public Participante()
        {
            this.CodigoPaciente = "";
            this.Nombres = "";
            this.ApellidoPaterno = "";
            this.ApellidoMaterno = "";
            this.CodigoTipoDocumento = 0;
            this.DocumentoIdentidad = "";
            this.FechaNacimiento = "";
            this.Sexo = 0;
        }

        public Participante(string CodigoPaciente,string Nombres, string ApellidoPaterno, 
            string ApellidoMaterno, int CodigoTipoDocumento, string DocumentoIdentidad, 
            string FechaNacimiento, int Sexo)
        {
            this.CodigoPaciente = CodigoPaciente;
            this.Nombres = Nombres;
            this.ApellidoPaterno = ApellidoPaterno;
            this.ApellidoMaterno = ApellidoMaterno;
            this.DocumentoIdentidad = DocumentoIdentidad;
            this.FechaNacimiento = FechaNacimiento;
            this.CodigoTipoDocumento = CodigoTipoDocumento;
            this.Sexo = Sexo;
        }
    }
}
