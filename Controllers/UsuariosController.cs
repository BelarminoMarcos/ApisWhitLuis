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
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuarios = await usuariosServices.Obtener();
            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> Post(UsuariosDTOs crearUsuario)
        {
            var usuarios = await usuariosServices.Agregar(crearUsuario);
            return Ok(usuarios);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var usuarios = await usuariosServices.Eliminar(id);
            if (usuarios.Valido)
            {
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
