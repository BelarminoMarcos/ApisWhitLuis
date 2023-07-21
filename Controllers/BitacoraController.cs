using CursosLuis.Api.DTOs;
using CursosLuis.Api.Services;
using CursosLuis.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CursosLuis.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class BitacoraController : ControllerBase
    {

        private readonly IBitacoraService bitacoraService;

        public BitacoraController(IBitacoraService bitacoraService)
        {
            this.bitacoraService = bitacoraService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuarios = await bitacoraService.Obtener();
            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> Post(BitacoraDTOs creaBitacora)
        {
            
            var usuarios = await bitacoraService.AgregarP(creaBitacora);
            return Ok(usuarios);
        }
    }
}
