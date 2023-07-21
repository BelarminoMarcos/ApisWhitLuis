using CursosLuis.Api.DTOs;
using CursosLuis.Api.Services;
using CursosLuis.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CursosLuis.Api.Controllers
{

    [ApiController]
    [Route("[Controller]")]
    public class RoleController : ControllerBase
    {

        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }



        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var roles = await roleService.Obtener();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> Post(RolesDTOs cr)
        {
            var rergistrar = await roleService.Agregar(cr);
            return Ok(rergistrar);
        }

        [HttpPut]
        public async Task<IActionResult> Update(RolesDTOs usuario)
        {
            var Actualizar = await roleService.Actualizar(usuario);
            return Ok(Actualizar);

        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var Eliminar = await roleService.Eliminar(id)
;
            if (Eliminar.Valido)
            {
                return Ok(Eliminar);
            }
            return BadRequest(Eliminar.Mensaje);

        }
    }
}
