namespace API_CLINICA.Data
{
    public class DetalleDoctores
    {
        public byte[]? FotoPerfil { get; set; }
        public string? Experiencia { get; set; }
        public string? Direccion { get; set; }
        public string? Origen { get; set; }
        public string? Colegiatura { get; set; }
        public string? Idiomas { get; set; }
        public string? HospitalAfiliado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public TimeSpan? HoraInicio { get; set; }
        public TimeSpan? HoraFin { get; set; }
    }
}
