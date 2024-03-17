using API_CLINICA.Data;
using API_CLINICA.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API_CLINICA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FotosController : Controller
    {
        private readonly IFotoRepository _fotoRepository;
        private readonly CLINICAContext _context;


        public FotosController(IFotoRepository fotoRepository, CLINICAContext context)
        {
            _fotoRepository = fotoRepository;

            _context = context;

        }
        [HttpPost("GuardarFoto")]
        public async Task<IActionResult> GuardarFoto(string codigo, IFormFile archivo)
        {
            try
            {
                if (string.IsNullOrEmpty(codigo) || archivo == null || archivo.Length == 0)
                {
                    return Json(new { success = false });
                }

                using (var memoryStream = new MemoryStream())
                {
                    await archivo.CopyToAsync(memoryStream);
                    byte[] imageData = memoryStream.ToArray();
                    var base64String = Convert.ToBase64String(imageData);

                    bool isSameImage = await _fotoRepository.VerificarImagenExistente(codigo, imageData);

                    if (isSameImage)
                    {
                        // La imagen es la misma, no es necesario guardarla ni actualizarla
                        return Json(new { showMessage = true, message = "La imagen seleccionada ya ha sido guardada." });
                    }

                    var nuevaFoto = new Foto
                    {
                        Foto1 = imageData,
                        Codigo = codigo,
                        // Otras propiedades si es necesario
                    };

                    _context.Fotos.Add(nuevaFoto);
                    await _context.SaveChangesAsync();

                    return Ok(new { success = true, showMessage = true, message = $"Foto con código {codigo} subida correctamente." });
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return Json(new { success = false, showMessage = true, message = "Error al subir la foto."+ ex });
            }
        }

        [HttpGet("ObtenerFoto")]
        public async Task<IActionResult> ObtenerFoto(string codigo)
        {
            if (!string.IsNullOrEmpty(codigo))
            {
                var img = _context.Fotos.FirstOrDefault(i => i.Codigo == codigo);
                string base64Image = Convert.ToBase64String(img.Foto1);
                //string imageSrc = $"data:image/png;base64,{base64Image}";
                string imageSrc = base64Image;
                return Json(imageSrc);
            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpGet("ObtenerTodasLasFotos")]
        public async Task<IActionResult>ObtenerTodasLasFotos()
        {
            var ListaFotos= await _fotoRepository.GetAllFotos();
            return Json(ListaFotos);    
        }
                
        
        [HttpDelete("EliminarFotoPorCodigo")]
        public async Task<IActionResult>EliminarFoto(string codigo)
        {
            var fotoParaEliminar= await _fotoRepository.EliminarFoto(codigo);
            return Ok(fotoParaEliminar);
        }


    }
}
