using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace TestDevBackJr
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEmpleados();
            }
        }
        private void CargarEmpleados()
        {
            using (var objEmpleados = new libEmpleados.rnEmpleados() { })
            {
                DataTable dt = objEmpleados.ListarDatos();

                if (objEmpleados.objError.bError != true)
                {
                    if (dt.Rows.Count != 0)
                    {
                        gvEmpleado.DataSource = dt;
                        gvEmpleado.DataBind();
                    }
                }
                else
                {
                    MensajeError("Ups... Ocurrio un error, favor de intentar más tarde");
                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var objEmpleados = new libEmpleados.rnEmpleados() { Nombre = txtNombre.Text.Trim(), Paterno = txtPaterno.Text.Trim(), Materno = txtMaterno.Text.Trim(), Sueldo = Convert.ToDecimal(txtSueldo.Text.Trim()), Login = txtLogin.Text.Trim() })
                {
                    objEmpleados.IngresaDatos();

                    if (objEmpleados.objError.bError != true)
                    {
                        txtNombre.Text = "";
                        txtPaterno.Text = "";
                        txtMaterno.Text = "";
                        txtSueldo.Text = "";
                        txtLogin.Text = "";

                        CargarEmpleados();
                    }
                    else
                    {
                        MensajeError("Ups... Ocurrio un error, favor de intentar más tarde");
                    }
                }
            }
            catch  { MensajeError("Ups... Ocurrio un error, favor de intentar más tarde");  }


        }

        protected void gvEmpleados_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmpleado.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvEmpleados_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    int userId = Convert.ToInt32(gvEmpleado.DataKeys[e.RowIndex].Value);
                    TextBox txtSueldo = (TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtSueldo");
                    decimal nuevoSueldo = Convert.ToDecimal(txtSueldo.Text.Replace("$", "").Replace(",", ""));

                    UpdateSueldo(userId, nuevoSueldo);

                    gvEmpleado.EditIndex = -1;
                    BindGrid();
                }
            }
            catch
            {

                BindGrid();
                MensajeError("Ups... Ocurrio un error, favor de intentar más tarde");
            }
        }

        protected void gvEmpleados_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEmpleado.EditIndex = -1;
            BindGrid();
        }
        private void BindGrid()
        {

            using (var objEmpleados = new libEmpleados.rnEmpleados() { })
            {
                DataTable dt = objEmpleados.ListarDatos();

                if (objEmpleados.objError.bError != true)
                {
                    if (dt.Rows.Count != 0)
                    {
                        gvEmpleado.DataSource = dt;
                        gvEmpleado.DataBind();
                    }
                }
                else
                {
                    MensajeError("Ups... Ocurrio un error, favor de intentar más tarde");
                }
            }
        }

        private void UpdateSueldo(int userId, decimal nuevoSueldo)
        {
            using (var objActualizaSueldo = new libEmpleados.rnEmpleados() { Sueldo = nuevoSueldo, UserId = userId })
            {
                objActualizaSueldo.ActualizaDatos();
                if (objActualizaSueldo.objError.bError != true)
                {
                    BindGrid();
                }
                else
                {
                    MensajeError("Ups... Ocurrio un error, favor de intentar más tarde");
                }
            }
        }

        public void GenerarCSV(List<Empleado> empleados)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Login,Nombre Completo,Sueldo,Fecha Ingreso");

            foreach (var empleado in empleados)
            {
                sb.AppendLine($"{empleado.Login},{empleado.NombreCompleto},{empleado.Sueldo},{empleado.FechaIngreso:yyyy-MM-dd}");
            }

            string fileName = "empleados.csv";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.AddHeader("Content-Disposition", $"attachment; filename={fileName}");

            HttpContext.Current.Response.Write(sb.ToString());
            HttpContext.Current.Response.End();
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            DataTable dt = FillInformacion();
            List<Empleado> empleados = ConvertirDataTableAEmpleados(dt);

            GenerarCSV(empleados);
        }


        public List<Empleado> ObtenerEmpleados()
        {

            DataTable dt = FillInformacion();
            List<Empleado> empleados = ConvertirDataTableAEmpleados(dt);

            return empleados;
        }

        public class Empleado
        {
            public string Login { get; set; }
            public string NombreCompleto { get; set; }
            public double Sueldo { get; set; }
            public DateTime FechaIngreso { get; set; }
        }

        public List<Empleado> ConvertirDataTableAEmpleados(DataTable dt)
        {
            List<Empleado> empleados = new List<Empleado>();

            foreach (DataRow row in dt.Rows)
            {
                Empleado empleado = new Empleado
                {
                    Login = row["Login"].ToString(),
                    NombreCompleto = row["NombreCompleto"].ToString(),
                    Sueldo = Convert.ToDouble(row["Sueldo"]),
                    FechaIngreso = Convert.ToDateTime(row["FechaIngreso"])
                };

                empleados.Add(empleado);
            }

            return empleados;
        }

        private DataTable FillInformacion()
        {
            DataTable dt = new DataTable();
            using (var objDatos = new libEmpleados.rnEmpleados() { })
            {
                dt = objDatos.ListarDatosReporte();
            }

            dt.TableName = "Empleados";
            return dt;
        }

        private void MensajeError(string mensajeError)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{mensajeError}');", true);
        }
    }
}