using CursosLuis.Api.DTOs;
using CursosLuis.Api.Model;
using CursosLuis.Api.Services;
using CursosLuis.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CursosLuis.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuariosController : Controller
    {

        private readonly IUsuariosService usuariosServices;

        public UsuariosController(IUsuariosService usuariosServices)
        {
            this.usuariosServices = usuariosServices;
        }
        [HttpGet("ObtenerUsuario")]
        public async Task<IActionResult> Get()
        {
            var usuarios = await usuariosServices.Obtener();
            return Ok(usuarios);
        }

        [HttpPost("CrearUsuario")]
        public async Task<IActionResult> Post(UsuariosDTOs crearUsuario)
        {
            var usuarios = await usuariosServices.Agregar(crearUsuario);
            return Ok(usuarios);
        }

        [HttpDelete("EliminarUsuario")]
        public async Task<IActionResult> Delete(UsuariosDTOs eliminarUser)
        {
            var usuarios = await usuariosServices.Eliminar(eliminarUser);
            return Ok(usuarios);
        }


        [HttpPut("ActualizarUsuario")]
        public async Task<IActionResult> Updated(UsuariosDTOs actUser)
        {
            var usuarios = await usuariosServices.Actualizar(actUser);
            return Ok(usuarios);
        }

    }
}
