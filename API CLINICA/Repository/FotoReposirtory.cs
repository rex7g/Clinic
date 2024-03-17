using API_CLINICA.Data;
using Microsoft.EntityFrameworkCore;


namespace API_CLINICA.Repository
{
    public class FotoReposirtory : IFotoRepository
    {
        private readonly CLINICAContext _context;

        public FotoReposirtory(CLINICAContext clinicaContext)
        {
            _context = clinicaContext;
        }

        public Task ActualizarFoto(Foto foto)
        {
            throw new NotImplementedException();
        }

      
        public async Task<Foto> EliminarFoto(string codigo)
        {
            var FotoEliminada = await _context.Fotos.FirstOrDefaultAsync(f => f.Codigo == codigo);
            if(FotoEliminada != null)
            {
                _context.Fotos.Remove(FotoEliminada);
                await _context.SaveChangesAsync();

            }
            return FotoEliminada;

        }

        public async Task<IEnumerable<Foto>> GetAllFotos()
        {
            var ListaFotos= await _context.Fotos.ToListAsync();
            return ListaFotos;

        }

        public async Task<Foto> GetFotobyCodigo(string codigo)
        {
            var FotoEncontrada = await _context.Fotos.FirstOrDefaultAsync(d=>d.Codigo==codigo);
            return FotoEncontrada;

        }


        public async Task<Foto> GuardarFoto( Foto foto ,IFormFile archivo)
        {
            using (var memoryStream = new MemoryStream())
            {
                await archivo.CopyToAsync(memoryStream);
                foto.Foto1 = memoryStream.ToArray(); // Convierte el contenido del archivo a un arreglo de bytes
            }

            _context.Fotos.Add(foto);
            await _context.SaveChangesAsync();
            return foto;

        }

        

        public async Task<bool> VerificarImagenExistente(string codigo, byte[] nuevaImagen)
        {
            var imagenExistente = _context.Fotos.FirstOrDefault(i => i.Codigo == codigo);
            if (imagenExistente != null && imagenExistente.Foto1 != null)
            {
                var imagenExisteEnDb = imagenExistente.Foto1.SequenceEqual(nuevaImagen);
                return imagenExisteEnDb;
            }
            else
            {
                return false;

            }
        }
    }
}
