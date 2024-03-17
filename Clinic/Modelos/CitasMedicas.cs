using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Modelos
{
    public class CitasMedicas
    {
        public int Id { get; set; }
        public string? Paciente { get; set; }
        public string? Doctor { get; set; }
        public DateTime? FechaCita { get; set; }
        public TimeSpan? HoraCita { get; set; }
        public string? Motivos { get; set; }
        public string? Status { get; set; }
    }
}
