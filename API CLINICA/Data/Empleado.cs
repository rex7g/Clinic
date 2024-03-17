using System;
using System.Collections.Generic;

namespace API_CLINICA.Data
{
    public partial class Empleado
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? puesto { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Contraseña { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? Codigo { get; set; }
        public string? Status { get; set; }
    }
}
