using System.Data;

namespace libLogIn
{
    public class rnLogIn : adLogIn
    {
        public DataTable dt { get; set; }
        public entLogIn entLogIn { get; set; }

        public rnLogIn(string sConex = "Empleados") : base(sConex)
        {
        }

		public DataTable ListarDatos()
		{
			Bandera = "s1";
			dt = Listar();
			Propiedades = Propiedades;
			return dt;
		}

	}
}
