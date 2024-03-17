using System;
using System.Collections.Generic;

namespace API_CLINICA.Data
{
    public partial class Foto
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public byte[]? Foto1 { get; set; }
    }
}
