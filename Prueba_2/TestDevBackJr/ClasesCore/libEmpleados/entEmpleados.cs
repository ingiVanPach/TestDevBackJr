using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libEmpleados
{
    public class entEmpleados
    {
        public int IdLogIn { get; set; }
        public int UserId { get; set; }
        public decimal Sueldo { get; set; }
        public string Login { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaAlta { get; set; }
        public bool Activo { get; set; }
    }
}
