using System;
using System.Collections.Generic;

namespace API_CLINICA.Data
{
    public partial class CitasMedica
    {
        public int Id { get; set; }
        public string? Paciente { get; set; }
        public string? Doctor { get; set; }
        public DateTime? FechaCita { get; set; }
        public string? Motivos { get; set; }
        public string? Status { get; set; }
    }
}
