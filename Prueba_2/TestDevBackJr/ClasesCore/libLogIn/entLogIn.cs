using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libLogIn
{
    public class entLogIn
    {
        public int IdLogIn { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public DateTime? FechaAlta { get; set; }
        public bool Activo { get; set; }
    }
}
