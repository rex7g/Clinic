using API_CLINICA.Data;
using API_CLINICA.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API_CLINICA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctoresController : Controller
    {
        private readonly IDoctores _DoctoresRepository;
        private readonly CLINICAContext _context;

        public DoctoresController(IDoctores doctores, CLINICAContext context)
        {
            _DoctoresRepository = doctores;
            _context = context;
        }
        [HttpGet("ListaDoctores")]
        public async Task<IActionResult> GetDoctores()
        {
            var Doctoress = await _DoctoresRepository.GetAsyncAllDoctores();
            return Json(Doctoress);
        }
        [HttpGet("BuscarDoctoresporEspecialidad")]
        public async Task<IActionResult> GetDoctores(string especialidad)
        {
            var Doctoress = await _DoctoresRepository.GetasyncDoctorbyEspecialidad(especialidad);
            return Json(Doctoress);
        }
        [HttpGet("BuscarDoctoresporNombre")]
        public async Task<IActionResult> GetDoctoresporNombre(string nombre)
        {
            var Doctoress = await _DoctoresRepository.GetDoctoresByName(nombre);
            return Json(Doctoress);
        }
        [HttpGet("BuscarFotoDoctorporCodigo")]
        public async Task<IActionResult> GetFotoDoctor(string codigo)
        {
            var Doctoress = await _DoctoresRepository.GetFotoDoctorbyCodigo(codigo);
            return Json(Doctoress);
        }

        [HttpPut("ActualizarDoctores")]
        public async Task<IActionResult> UpdateDoctoresbyCodigo(string nombre, Doctore DoctoresActualizado)
        {
            var Doctores = await _DoctoresRepository.UpdateDoctoresbyNombre(nombre, DoctoresActualizado);
            return Json(Doctores);
        }
        [HttpDelete("DeleteDoctores")]
        public async Task<IActionResult> DeleteDoctoresbycodigo(string nombre)
        {
            var Doctores = await _DoctoresRepository.DeleteDoctoresByNombre(nombre);
            return Json(Doctores);
        }
        [HttpPost("CrearDoctores")]
        public async Task<IActionResult> CrearDoctores( Doctore doctore)
        {
            var DoctoresNuevo = await _DoctoresRepository.CrearDoctoresnuevo(doctore);
            return Json(DoctoresNuevo);
        }
    }
}
