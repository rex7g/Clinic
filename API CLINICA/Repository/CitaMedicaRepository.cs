using API_CLINICA.Data;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace API_CLINICA.Repository
{
    public class CitaMedicaRepository : ICitaMedica
    {
        private readonly CLINICAContext _context;
        public CitaMedicaRepository(CLINICAContext cLINICAContext)
        {
            _context = cLINICAContext;
        }

        public async Task<int> CantidadCitasCanceladas(string estado)
        {
            var numeroCitasCanceladas = await _context.CitasMedicas.Where(e => e.Status == estado).CountAsync();
            return numeroCitasCanceladas;
        }

        public async Task<int> CantidadCitasmedicas()
        {
            var Nocitasmedicas = await _context.CitasMedicas.CountAsync();
            return Nocitasmedicas;
        }

        public async Task<int> CantidadCitasMedicasCompletadas(string estado)
        {
            var numeroCitasCompletadas = await _context.CitasMedicas.Where(e => e.Status == estado).CountAsync();
            return numeroCitasCompletadas;
        }

        public async Task<int> CantidadCitasMedicasPendientes(string estado)
        {
            var numeroCitasPendientes = await _context.CitasMedicas.Where(e => e.Status == estado).CountAsync();
            return numeroCitasPendientes;
        }

        public async Task<CitasMedica> CrearNuevaCita(CitasMedica nuevaCita)
        {
            await _context.AddAsync(nuevaCita);
            await _context.SaveChangesAsync();

            return nuevaCita;
        }

        public async Task<CitasMedica> DeleteCitasMedica(string paciente)
        {
            var CitasMedicaEliminada = await _context.CitasMedicas.FirstOrDefaultAsync(e => e.Paciente == paciente);
            if (CitasMedicaEliminada != null)
            {
                _context.CitasMedicas.Remove(CitasMedicaEliminada);
                await _context.SaveChangesAsync();
            }
            return CitasMedicaEliminada;
        }

        public async Task<IEnumerable<CitasMedica>> GetAsyncAllCitasMedica()
        {
            var ListaCitasMedica = await _context.CitasMedicas.ToListAsync();
            return ListaCitasMedica;
        }

        public async Task<CitasMedica> GetAsyncCitasMedica(string Id)
        {
            throw new NotImplementedException();
        }
        public async Task<CitasMedica> GetCitasMedicaByDoctor(string doctor)
        {
            var citamedica = await _context.CitasMedicas.FirstOrDefaultAsync(e => e.Doctor == doctor);
            return citamedica;
        }
        public async Task<IEnumerable<CitasMedica> >GetCitasMedicaByFecha(DateTime fecha)
        {
            var citasMedicas = await _context.CitasMedicas.Where(e => e.FechaCita == fecha).ToListAsync();
            return citasMedicas;
        }

        public async Task<CitasMedica> GetCitasMedicaByPaciente(string paciente)
        {
            var citamedica = await _context.CitasMedicas.FirstOrDefaultAsync(e => e.Paciente == paciente);
            return citamedica;
        }

        public async Task<IEnumerable<CitasMedica>> GetCitasMedicaByStatus(string Status)
        {
            var citamedica = await _context.CitasMedicas.Where(e=>e.Status==Status).ToListAsync();
            return citamedica;
        }

        public async Task<CitasMedica> UpdateCitasMedica(string paciente, CitasMedica citaMedicaActualizada)
        {
            var citaMedica = await _context.CitasMedicas.FirstOrDefaultAsync(e => e.Paciente == paciente);

            if (citaMedica != null)
            {
                citaMedica.Paciente = citaMedicaActualizada.Paciente;
                citaMedica.Doctor = citaMedicaActualizada.Doctor;
                citaMedica.Status = citaMedicaActualizada.Status;
                citaMedica.Motivos= citaMedicaActualizada.Motivos;
                citaMedica.FechaCita = citaMedicaActualizada.FechaCita;
                // Actualiza otros campos según sea necesario

                _context.CitasMedicas.Update(citaMedica);
                await _context.SaveChangesAsync();
            }

            return citaMedica;
        }
    }
}
