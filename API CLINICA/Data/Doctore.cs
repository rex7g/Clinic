using System;
using System.Collections.Generic;

namespace API_CLINICA.Data
{
    public partial class Doctore
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Especialidad { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Consultorio { get; set; }
        public string? Sexo { get; set; }
        public int? Edad { get; set; }
        public string? Jornada { get; set; }
        public string? Status { get; set; }
    }
}
