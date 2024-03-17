using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API_CLINICA.Data;
using API_CLINICA.Repository;

namespace API_CLINICA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class usuariosController : Controller
    {
        private readonly CLINICAContext _context;
        private readonly IUser _usuarioRepository;

        public usuariosController(IUser usuario, CLINICAContext context)
        {
            _context = context;
            _usuarioRepository = usuario;
        }
        [HttpGet("ListaUsuario")]
        public async Task<IActionResult> GetUsuarios()
        {
            var Usuario = await _usuarioRepository.GetAsyncAllUsuario();
            return Json(Usuario);
        }
        [HttpGet("BuscarUsuarioporCodigo")]
        public async Task<IActionResult> GetUsuarioporCodigo(string codigo)
        {
            var Usuario = await _usuarioRepository.GetUsuarioByCodigo(codigo);
            return Json(Usuario);
        }
        [HttpGet("BuscarUsuarioporNombre")]
        public async Task<IActionResult> GetUsuarioporNombre(string nombre)
        {
            var Usuario = await _usuarioRepository.GetUsuarioByName(nombre);
            return Json(Usuario);
        }
        [HttpGet("BuscarPasswordporNombre")]
        public async Task<IActionResult> GetPassword(string user)
        {
            var Usuario = await _usuarioRepository.GetPasswordbyUser(user);
            return Json(Usuario);
        }
        [HttpPut("ActualizarUsuario")]
        public async Task<IActionResult> UpdateEmpleadobyCodigo(string codigo, Usuario empleadoActualizado)
        {
            var Usuario = await _usuarioRepository.UpdateUsuariobyEmail(codigo, empleadoActualizado);
            return Json(Usuario);
        }
        [HttpPut("ActualizarUsuarioPorEmail")]
        public async Task<IActionResult> UpdateEmpleadobyEmail(string email, Usuario empleadoActualizado)
        {
            var Usuario = await _usuarioRepository.UpdateUsuariobyEmail(email, empleadoActualizado);
            return Json(Usuario);
        }
        [HttpPut("ActualizarContraseñaUsuario")]
        public async Task<IActionResult> UpdateContraseña(string email,string NewPassword)
        {
            var Usuario = await _usuarioRepository.ResetContraseñabyEmail(email, NewPassword);
            return Json(Usuario);
        }


        [HttpDelete("DeleteUsuario")]
        public async Task<IActionResult> DeleteUsuarioByEmail(string email)
        {
            var Usuario = await _usuarioRepository.DeleteUsuarioByEmail(email);
            return Json(Usuario);
        }
        [HttpPost("CrearUsuario")]
        public async Task<IActionResult> CrearUsuario([FromBody] Usuario nuevoUsuario)
        {
            var empleadoNuevo = await _usuarioRepository.CrearUsuarionuevo(nuevoUsuario);
            return Ok(empleadoNuevo);
        }


    }
}
