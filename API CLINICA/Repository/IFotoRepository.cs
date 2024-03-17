using API_CLINICA.Data;

namespace API_CLINICA.Repository
{
    public interface IFotoRepository
    {
        Task<Foto> GetFotobyCodigo(string codigo);
        Task<IEnumerable<Foto>> GetAllFotos(); // Método para obtener todas las fotos (si es necesario)
        Task<Foto> GuardarFoto(Foto foto, IFormFile archivo); // Método para guardar una nueva foto
        Task ActualizarFoto(Foto foto); // Método para actualizar una foto existente
        Task<Foto> EliminarFoto(string codigo); // Méto
        Task<bool> VerificarImagenExistente(string codigo, byte[] nuevaImagen);
    }
}
