using API_CLINICA.Data;

namespace API_CLINICA.Repository
{
    public interface IDoctores
    {
        Task<IEnumerable<Doctore>> GetasyncDoctorbyEspecialidad(string especialidad);
        Task<IEnumerable<Doctore>> GetAsyncAllDoctores();
        Task<Doctore> GetDoctoresByName(string nombre);
        Task<Doctore> DeleteDoctoresByNombre(string codigo);
        Task<Doctore> UpdateDoctoresbyNombre(string codigo, Doctore DoctoresActualizado);
        Task<Doctore> CrearDoctoresnuevo(Doctore nuevoDoctores);
        Task<Foto>GetFotoDoctorbyCodigo(string codigo);
        


 
    }
}