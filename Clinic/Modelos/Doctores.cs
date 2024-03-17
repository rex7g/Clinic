using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Modelos
{
    public class Doctores
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Especialidad { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? Consultorio { get; set; }
        public string? Sexo { get; set; }
        public int? Edad { get; set; }
        public string? Jornada { get; set; }
    }
}
