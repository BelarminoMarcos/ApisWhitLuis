using CursosLuis.Api.Helpers;
using CursosLuis.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CursosLuis.Api.Controllers
{

    [ApiController]
    [Route("[Controller]")]
    public class SeguridadController : Controller
    {
        private readonly ISeguridadService seguridadService;
        private readonly IBitacoraService bitacoraService;

        public SeguridadController(ISeguridadService seguridadService, IBitacoraService bitacoraService)
        {
            this.seguridadService = seguridadService;
            this.bitacoraService = bitacoraService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string correo, string contraseña)
        {
            var respuesta = await seguridadService.Autenticar(correo,contraseña);
            if (respuesta.Valido) {
                int idUsuario = Convert.ToInt32(User.Identity.Name);
                await bitacoraService.AgregarP(new DTOs.BitacoraDTOs()
                {
                    Accion = (byte)AccionesBitacoraEnum.Autenticarse,
                    Descripcion = $"Se autentico el usuario{correo}",
                    Fecha = DateTime.Now,
                    IdUsuario = idUsuario,
                    Modulo = (byte)ModulosBitacoraEnum.Seguridad
                });

                return Ok(respuesta);
            }
            return BadRequest(respuesta.Mensaje);
        }
    }
}
