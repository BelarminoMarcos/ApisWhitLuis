using CursosLuis.Api.DTOs;
using CursosLuis.Api.Helpers;
using CursosLuis.Api.Model;
using CursosLuis.Api.Services;
using CursosLuis.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CursosLuis.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuariosController : Controller
    {

        private readonly IUsuariosService usuariosServices;
        private readonly IBitacoraService bitacoraService;

        public UsuariosController(IUsuariosService usuariosServices, IBitacoraService bitacoraService)
        {
            this.usuariosServices = usuariosServices;
            this.bitacoraService = bitacoraService;
        }
        [Authorize]
        [HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var usuarios = await usuariosServices.Obtener();
        //    return Ok(usuarios);
        //}
        public async Task<IActionResult> Get()
        {
            var usuarios = await usuariosServices.Obtener();
            if (usuarios.Valido)
            {
                var role = User.Claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault();
                var email = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault();
                var claims = User.Claims.ToList();
                int idUsuario = Convert.ToInt32(User.Identity.Name);
                await bitacoraService.AgregarP(new DTOs.BitacoraDTOs()
                {
                    Accion = (byte)AccionesBitacoraEnum.Autenticarse,
                    Descripcion = $"El usuario{email}consulto la informacion de usuarios",
                    Fecha = DateTime.Now,
                    IdUsuario = idUsuario,
                    Modulo = (byte)ModulosBitacoraEnum.Seguridad
                });
            }

            return Ok(usuarios);
        }



        [HttpPost]
        public async Task<IActionResult> Post(UsuariosDTOs crearUsuario)
        {
            var usuarios = await usuariosServices.Agregar(crearUsuario);
            return Ok(usuarios);
        }

        [Authorize]
        [HttpDelete]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var usuarios = await usuariosServices.Eliminar(id);
        //    if (usuarios.Valido)
        //    {
        //        return Ok(usuarios);
        //    }
        //    return BadRequest(usuarios.Mensaje);
        //}
        public async Task<IActionResult> Delete(int id)
        {
            var usuarios = await usuariosServices.Eliminar(id)
;
            if (usuarios.Valido)

            {
                var claims = User.Claims.ToList();
                int idUsuario = Convert.ToInt32(User.Identity.Name);
                await bitacoraService.AgregarP(new DTOs.BitacoraDTOs()
                {
                    Accion = (byte)AccionesBitacoraEnum.Autenticarse,
                    Descripcion = $"Se elimino el usuario{usuarios.Objeto.Correo}",
                    Fecha = DateTime.Now,
                    IdUsuario = idUsuario,
                    Modulo = (byte)ModulosBitacoraEnum.Seguridad
                });
                return Ok(usuarios);
            }
            return BadRequest(usuarios.Mensaje);
        }


        [HttpPut]
        public async Task<IActionResult> Updated(UsuariosDTOs actualizarUsuario)
        {
            var usuarios = await usuariosServices.Actualizar(actualizarUsuario);
            return Ok(usuarios);
        }

    }
}
