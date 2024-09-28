using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libEmpleados
{
    public class adEmpleados : entEmpleados, IDisposable
    {
        protected string Bandera { get; set; }
        public string sXML { get; set; }
        public PropertyCollection Propiedades { get; set; }
        public Errores objError { get; set; } = new Errores() { bError = false, uException = null };

        private SqlConnection cn;
        protected adEmpleados(string sConex)
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings[sConex].ConnectionString);
        }

		protected void NonQueryFunction()
		{
			try
			{
				cn.Open();
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.CommandText = "uspEmpleados";
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Connection = cn;
					cmd.Parameters.AddWithValue("@UserId", UserId);
					cmd.Parameters.AddWithValue("@Sueldo", Sueldo);
					cmd.Parameters.AddWithValue("@FechaIngreso", FechaIngreso);
					cmd.Parameters.AddWithValue("@Login", Login);
					cmd.Parameters.AddWithValue("@Nombre", Nombre);
					cmd.Parameters.AddWithValue("@Paterno", Paterno);
					cmd.Parameters.AddWithValue("@Materno", Materno);
					cmd.Parameters.AddWithValue("@FechaAlta", FechaAlta);
					cmd.Parameters.AddWithValue("@Activo", Activo);
					cmd.Parameters.AddWithValue("@Bandera", Bandera);
					cmd.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				objError.bError = true;
				objError.uException = ex;
			}
			finally
			{
				cn.Close();
			}
		}

		protected DataTable Listar()
		{
			DataTable dt = new DataTable("Datos");

			try
			{
				cn.Open();
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.CommandText = "uspEmpleados";
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Connection = cn;
					cmd.Parameters.AddWithValue("@UserId", UserId);
					cmd.Parameters.AddWithValue("@Sueldo", Sueldo);
					cmd.Parameters.AddWithValue("@FechaIngreso", FechaIngreso);
					cmd.Parameters.AddWithValue("@Login", Login);
					cmd.Parameters.AddWithValue("@Nombre", Nombre);
					cmd.Parameters.AddWithValue("@Paterno", Paterno);
					cmd.Parameters.AddWithValue("@Materno", Materno);
					cmd.Parameters.AddWithValue("@FechaAlta", FechaAlta);
					cmd.Parameters.AddWithValue("@Activo", Activo);
					cmd.Parameters.AddWithValue("@Bandera", Bandera);

					using (SqlDataAdapter da = new SqlDataAdapter(cmd))
					{
						da.Fill(dt);
					}

					if (dt.Rows.Count == 1)
					{
						Propiedades = new PropertyCollection();
						foreach (DataColumn cols in dt.Columns)
						{
							Propiedades.Add(cols.ColumnName, (Convert.IsDBNull(dt.Rows[0][cols.ColumnName]) ? "" : dt.Rows[0][cols.ColumnName]));
						}

					}
				}
			}
			catch (Exception ex)
			{
				objError.bError = true;
				objError.uException = ex;
			}
			finally
			{
				cn.Close();
			}

			return dt;
		}



		public class Errores
        {
            public bool bError { get; set; }
            public Exception uException { get; set; }
        }

            private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: eliminar el estado administrado (objetos administrados)
                }

                // TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
                // TODO: establecer los campos grandes como NULL
                disposedValue = true;
            }
        }

        // // TODO: reemplazar el finalizador solo si "Dispose(bool disposing)" tiene código para liberar los recursos no administrados
        // ~adEmpleados()
        // {
        //     // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
