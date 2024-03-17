using API_CLINICA.Data;
using API_CLINICA.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Construction;
using Microsoft.IdentityModel.Tokens;

namespace API_CLINICA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadosController : Controller
    {
        private readonly IEmpleados _empleadoRepository;
        private readonly CLINICAContext _context;

        public EmpleadosController(IEmpleados empleados,CLINICAContext context)
        {
            _empleadoRepository = empleados;
            _context = context;
        }

       
        [HttpGet("ListaEmpleados")]
        public async Task< IActionResult> GetEmpleados()
        {
            var empleados= await _empleadoRepository.GetAsyncAllEmpleado();
            return Json(empleados);
        }
        [HttpGet("BuscarEmpleadoporCodigo")]
        public async Task<IActionResult> GetEmpleadosporCodigo(string codigo)
        {
            var empleados = await _empleadoRepository.GetAsyncEmpleado(codigo);
            return Json(empleados);
        }
        [HttpGet("BuscarEmpleadoporNombre")]
        public async Task<IActionResult> GetEmpleadosporNombre(string nombre)
        {
            var empleados = await _empleadoRepository.GetEmpleadoByName(nombre);
            return Json(empleados);
        }
        [HttpPut("ActualizarEmpleados")]
        public async Task<IActionResult>UpdateEmpleadobyCodigo(string codigo,Empleado empleadoActualizado)
        {
            var empleados=await _empleadoRepository.UpdateEmpleadobyCodigo(codigo,empleadoActualizado);
            return Json(empleados);
        }
        [HttpDelete("DeleteEmpleados")]
        public async Task<IActionResult>DeleteEmpleadobycodigo(string codigo)
        {
            var empleados=await _empleadoRepository.DeleteEmpleadoByCodigo(codigo);
            return Json(empleados);
        }
        [HttpPost("CrearEmpleado")]
        public async Task<IActionResult>CrearEmpleado([FromBody]Empleado nuevoEmpleado)
        {
            var empleadoNuevo = await _empleadoRepository.CrearEmpleadonuevo(nuevoEmpleado);
            return Json(empleadoNuevo);
        }
    }
}

