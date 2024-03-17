using API_CLINICA.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace API_CLINICA.Repository
{
    public class UsuarioRepository : IUser
    {
        private readonly CLINICAContext _context;

        public UsuarioRepository(CLINICAContext cLINICAContext)
        {
            _context = cLINICAContext;
        }

        public async Task<Usuario> CrearUsuarionuevo(Usuario nuevoUsuario)
        {
            await _context.Usuario.AddAsync(nuevoUsuario);
            await _context.SaveChangesAsync();

            return nuevoUsuario;
        }

       


        public async Task<Usuario> DeleteUsuarioByEmail(string email)
        {
            var usuario= await _context.Usuario.FirstOrDefaultAsync(e => e.Email == email);
            if (usuario != null)
            {
                _context.Usuario.Remove(usuario);
                await _context.SaveChangesAsync();
            }
            return usuario;
        }

        public async Task<IEnumerable<Usuario>> GetAsyncAllUsuario()
        {
            var usuario = await _context.Usuario.ToListAsync();
            return usuario;
        }

        public async Task<Usuario> GetAsyncUsuariobyEmail(string email)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(e => e.Email == email);
            return usuario;

        }

        public async Task<Usuario> GetAsyncUsuariobyTelefono(string telefono)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(e => e.Telefono == telefono);
            return usuario;
        }

        public async Task<Usuario> GetUsuarioByName(string nombre)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(e => e.Nombre == nombre);
            return usuario;
        }
        public async Task<Usuario> GetUsuarioByCodigo(string cedula)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(e => e.Codigo == cedula);
            return usuario;
        }

        public async Task<Usuario> UpdateUsuariobyEmail(string email, Usuario UsuarioActualizado)
        {
            var usuarioActual = await _context.Usuario.FirstOrDefaultAsync(e => e.Email == email);

            if (usuarioActual != null)
            {
                // Actualiza los campos necesarios del empleado con los valores del empleadoActualizado
                usuarioActual.Nombre = UsuarioActualizado.Nombre;
                usuarioActual.Apellido = UsuarioActualizado.Apellido;               
                usuarioActual.Email = UsuarioActualizado.Email;
                usuarioActual.Telefono = UsuarioActualizado.Telefono;
                usuarioActual.Contraseña = UsuarioActualizado.Contraseña;
                // Actualiza otros campos según sea necesario

                _context.Usuario.Update(usuarioActual);
                await _context.SaveChangesAsync();
            }

            return usuarioActual;
        }
       
        public async Task<string> GetPasswordbyUser(string user)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(e => e.Nombre == user);
            return usuario.Contraseña;  
        }

        public async Task<Usuario> ResetContraseñabyEmail(string email, string NuevaContraseña)
        {
            var usuarioActual = await _context.Usuario.FirstOrDefaultAsync(e => e.Email == email);

            if (usuarioActual != null)
            {
                usuarioActual.Contraseña = NuevaContraseña;
                _context.Usuario.Update(usuarioActual);
                await _context.SaveChangesAsync();
            }

            return usuarioActual;
        }
    }
}
