using CursosLuis.Api.DTOs;
using CursosLuis.Api.Model;
using CursosLuis.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CursosLuis.Api.Services
{
    public class RoleService : IRoleService
    {
        private readonly CursoDbContext dtx; //crea un atributo de la clase
        public RoleService(CursoDbContext dtx)
        {
            this.dtx = dtx;
        }

        #region "Métodos"
        public async Task<RespuestaGenerica<List<RolesDTOs>>> Obtener()
        {
            RespuestaGenerica<List<RolesDTOs>> respuesta = new();
            try
            {
                respuesta.Objeto = await dtx.Roles.Select(x => new RolesDTOs(x.Id, x.Nombre))
                    .ToListAsync();
                respuesta.Valido = true;
            }
            catch (Exception e)
            {
                respuesta.Mensaje = e.Message;
            }
            return respuesta;
        }
        public async Task<RespuestaGenerica<object>> Actualizar(RolesDTOs rol)
        {
            RespuestaGenerica<object> respuesta = new()
            {
                Mensaje = "El rol no existe"
            };
            try
            {
                Role? existe = await dtx.Roles.Where(x => x.Id == rol.Id).FirstOrDefaultAsync();
                if (existe == null)
                {
                    return respuesta;
                }
                existe.Id = rol.Id;
                existe.Nombre = rol.Nombre;

                dtx.Roles.Update(existe);
                await dtx.SaveChangesAsync();
                respuesta.Mensaje = "Se actualizo el rol correctamente";
                respuesta.Objeto = rol;
                respuesta.Valido = true;
            }
            catch (Exception e)
            {
                respuesta.Mensaje = e.Message;
            }
            return respuesta;
        }

        public async Task<RespuestaGenerica<RolesDTOs>> Eliminar(int id)
        {
            RespuestaGenerica<RolesDTOs> respuesta = new()
            {
                Mensaje = "El usuario no existe"
            };
            try
            {
                Role? existe = await dtx.Roles.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (existe == null)
                {
                    return respuesta;
                }

                dtx.Roles.Remove(existe);
                await dtx.SaveChangesAsync();
                respuesta.Mensaje = "Se elimino el rol correctamente";
                respuesta.Objeto = new RolesDTOs(existe.Id, existe.Nombre);
                respuesta.Valido = true;
            }
            catch (Exception e)
            {
                respuesta.Mensaje = e.Message;
            }
            return respuesta;
        }

        public async Task<RespuestaGenerica<object>> Agregar(RolesDTOs modelo)
        {
            RespuestaGenerica<object> respuesta = new()
            {
                Mensaje = "Existe un rol con la misma información"
            };
            try
            {
                Role? existe = await dtx.Roles.Where(x => x.Nombre.ToLower() == modelo.Nombre.ToLower()).FirstOrDefaultAsync();
                if (existe != null)
                {
                    return respuesta;
                }

                existe = new Role
                {
                    Id = modelo.Id,
                    Nombre = modelo.Nombre
                };
                await dtx.Roles.AddAsync(existe);
                await dtx.SaveChangesAsync();
                modelo.Id = existe.Id;
                respuesta.Mensaje = "Se creo el rol correctamente";
                respuesta.Valido = true;
            }
            catch (Exception e)
            {
                respuesta.Mensaje = e.InnerException.Message;
            }
            return respuesta;
        }
        #endregion
    }
}
