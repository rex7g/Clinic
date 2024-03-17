using Microsoft.EntityFrameworkCore;

namespace API_CLINICA.Data
{
    public partial class CLINICAContext : DbContext
    {
        public CLINICAContext()
        {
        }

        public CLINICAContext(DbContextOptions<CLINICAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CitasMedica> CitasMedicas { get; set; } = null!;
        public virtual DbSet<Doctore> Doctores { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<Especialidade> Especialidades { get; set; } = null!;
        public virtual DbSet<Foto> Fotos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuario { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=DESKTOP-H4ICEER\\SQLEXPRESS;Initial Catalog=CLINICA;Integrated Security=True");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CitasMedica>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Doctor).HasMaxLength(100);


                entity.Property(e => e.FechaCita).HasColumnType("datetime");

                entity.Property(e => e.Paciente).HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<Doctore>(entity =>
            {
                entity.Property(e => e.Codigo).HasMaxLength(20);

                entity.Property(e => e.Consultorio).HasMaxLength(20);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Especialidad).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.Property(e => e.Jornada).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.Sexo).HasMaxLength(10);

                entity.Property(e => e.Telefono).HasMaxLength(15);
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Apellido).HasMaxLength(100);

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.Contraseña).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.puesto).HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Telefono).HasMaxLength(20);
            });

            modelBuilder.Entity<Especialidade>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.NombreEspecialidad).HasMaxLength(100);
            });

            modelBuilder.Entity<Foto>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Codigo).HasMaxLength(50);

                entity.Property(e => e.Foto1).HasColumnName("Foto");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Apellido).HasMaxLength(100);

                entity.Property(e => e.Codigo)
                    .HasMaxLength(10)
                    .HasColumnName("codigo");

                entity.Property(e => e.Contraseña).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.Telefono).HasMaxLength(20);

                entity.Property(e => e.Usuario1)
                    .HasMaxLength(30)
                    .HasColumnName("usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
