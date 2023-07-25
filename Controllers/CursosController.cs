using CursosLuis.Api.DTOs;
using CursosLuis.Api.Helpers;
using CursosLuis.Api.Services;
using CursosLuis.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CursosLuis.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CursosController : ControllerBase
    {
        private readonly ICursosService cursosService;
        private readonly IBitacoraService bitacoraService;//esta es la linea que agrege

        public CursosController(ICursosService cursosService, IBitacoraService bitacoraService)//agregue la bitacora
        {
            this.cursosService = cursosService;
            this.bitacoraService = bitacoraService;
        }



        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var roles = await cursosService.Obtener();
            //return Ok(roles);
            var usuarios = await cursosService.Obtener();
            if (usuarios.Valido)
            {
                var role = User.Claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault();
                var email = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault();
                var claims = User.Claims.ToList();
                int idUsuario = Convert.ToInt32(User.Identity.Name);
                await bitacoraService.AgregarP(new DTOs.BitacoraDTOs()
                {
                    Accion = (byte)AccionesBitacoraEnum.Obtener,
                    Descripcion = $"El usuario{email}consulto la informacion de usuarios",
                    Fecha = DateTime.Now,
                    IdUsuario = idUsuario,
                    Modulo = (byte)ModulosBitacoraEnum.Cursos
                });
            }

            return Ok(usuarios);

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

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            //            var Eliminar = await cursosService.Eliminar(id)
            //;
            //            if (Eliminar.Valido)
            //            {
            //                return Ok(Eliminar);
            //            }
            //            return BadRequest(Eliminar.Mensaje);
        
            var usuarios = await cursosService.Eliminar(id);
            if (usuarios.Valido)

            {
                var claims = User.Claims.ToList();
                int idUsuario = Convert.ToInt32(User.Identity.Name);
                await bitacoraService.AgregarP(new DTOs.BitacoraDTOs()
                {
                    Accion = (byte)AccionesBitacoraEnum.Eliminar,
                    Descripcion = $"Se elimino el usuario{usuarios.Objeto.Id}",
                    Fecha = DateTime.Now,
                    IdUsuario = idUsuario,
                    Modulo = (byte)ModulosBitacoraEnum.Cursos
                });
                return Ok(usuarios);
            }
            return BadRequest(usuarios.Mensaje);


        }
    }
}
