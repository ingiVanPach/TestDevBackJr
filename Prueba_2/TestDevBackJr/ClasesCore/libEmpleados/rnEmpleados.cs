using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libEmpleados
{
    public class rnEmpleados:adEmpleados
    {
        public DataTable dt { get; set; }
        public entEmpleados entEmpleados { get; set; }

        public rnEmpleados(string sConex = "Empleados") : base(sConex)
        {
        }

		public void IngresaDatos()
		{
			Bandera = "i1";
			NonQueryFunction();
		}
		public void ActualizaDatos()
		{
			Bandera = "u1";
			NonQueryFunction();
		}

		public DataTable ListarDatos()
		{
			Bandera = "s1";
			dt = Listar();
			Propiedades = Propiedades;
			return dt;
		}
		public DataTable ListarDatosReporte()
		{
			Bandera = "s2";
			dt = Listar();
			Propiedades = Propiedades;
			return dt;
		}

	}
}
