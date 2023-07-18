using CursosLuis.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CursosLuis.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : Controller
    {
        private readonly IUsuariosService UsuariosService;

        public UsuariosController(IUsuariosService usuariosService) {
            this.UsuariosService = usuariosService;
        
        }

        [HttpGet]
        public async Task <IActionResult> Get() {
         var usuarios= await  UsuariosService.Obtener();
            return Ok(usuarios);
                }
    }
}
