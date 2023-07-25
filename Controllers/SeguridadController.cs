using CursosLuis.Api.Helpers;
using CursosLuis.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
                //int idUsuario = Convert.ToInt32(User.Identity.Name);
               // var role = User.Claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault();//esta y
                //var email = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault();//Esta las agregue si probocan problema  las elimino
                await bitacoraService.AgregarP(new DTOs.BitacoraDTOs()
                {
                    Accion = (byte)AccionesBitacoraEnum.Autenticarse,
                    Descripcion = $"Se autentico el usuario {correo}",
                    //Descripcion = $"El usuario{correo} con el id {idUsuario} y el rol {role.Value} inserto un curso",//esta la agurege si causa problema la borro
                    Fecha = DateTime.Now,
                    IdUsuario = respuesta.Objeto.Id,//Se agrego esta linea para ver la autenticacion
                    //Modulo = (byte)ModulosBitacoraEnum.Seguridad,
                    Modulo = (byte)ModulosBitacoraEnum.Usuarios
                });

                return Ok(respuesta);
            }
            return BadRequest(respuesta.Mensaje);
        }
    }
}
