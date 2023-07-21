using CursosLuis.Api.DTOs;
using CursosLuis.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CursosLuis.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CursosController : ControllerBase
    {
        private readonly ICursosService cursosService;

        public CursosController(ICursosService cursosService)
        {
            this.cursosService = cursosService;
        }



        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var roles = await cursosService.Obtener();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CursoDTOS cr)
        {
            var rergistrar = await cursosService.Agregar(cr);
            return Ok(rergistrar);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CursoDTOS usuario)
        {
            var Actualizar = await cursosService.Actualizar(usuario);
            return Ok(Actualizar);

        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var Eliminar = await cursosService.Eliminar(id)
;
            if (Eliminar.Valido)
            {
                return Ok(Eliminar);
            }
            return BadRequest(Eliminar.Mensaje);

        }
    }
}
