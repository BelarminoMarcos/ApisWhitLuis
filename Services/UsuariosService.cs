
using CursosLuis.Api.DTOs;
using CursosLuis.Api.Model;
using CursosLuis.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursosLuis.Api.Services
{
    public class UsuariosService : IUsuariosService
    {
        #region "Atributos"
        private readonly CursoDbContext dtx;
        #endregion

        #region "Constructor"
        public UsuariosService(CursoDbContext dtx)
        {
            this.dtx = dtx;
        }
        #endregion

        #region "Métodos"
        /// <summary>
        /// Comprueba que el usuario exista.
        /// No se actualiza el correo.
        /// </summary>
        public async Task<RespuestaGenerica<object>> Actualizar(UsuariosDTOs modelo)
        {
            RespuestaGenerica<object> respuesta = new()
            {
                Mensaje = "El usuario no existe"
            };
            try
            {
                Usuario? existe = await dtx.Usuarios.Where(x => x.Id == modelo.Id).FirstOrDefaultAsync();
                if (existe == null)
                {
                    return respuesta;
                }

                existe.Nombre = modelo.Nombre;
                existe.IdRol = modelo.IdRol;
                existe.Apellidos = modelo.Apellidos;
                existe.Contrasena = modelo.Contrasena;
                existe.FechaDeNacimiento = modelo.FechaDeNacimiento;
                dtx.Usuarios.Update(existe);
                await dtx.SaveChangesAsync();
                respuesta.Mensaje = "Se actualizo el usuario correctamente";
                respuesta.Objeto = modelo;
                respuesta.Valido = true;
            }
            catch (Exception e)
            {
                respuesta.Mensaje = e.Message;
            }
            return respuesta;
        }

        public async Task<RespuestaGenerica<UsuariosDTOs>> Eliminar(int id)
        {
            RespuestaGenerica<UsuariosDTOs> respuesta = new()
            {
                Mensaje = "El usuario no existe"
            };
            try
            {
                Usuario? existe = await dtx.Usuarios.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (existe == null)
                {
                    return respuesta;
                }

                dtx.Usuarios.Remove(existe);
                await dtx.SaveChangesAsync();
                respuesta.Mensaje = "Se elimino el usuario correctamente";
                respuesta.Objeto = new UsuariosDTOs(existe.Id, existe.IdRol, existe.Nombre, existe.Apellidos, existe.FechaDeNacimiento, existe.Contrasena, existe.Contrasena);
                respuesta.Valido = true;
            }
            catch (Exception e)
            {
                respuesta.Mensaje = e.Message;
            }
            return respuesta;
        }

        public async Task<RespuestaGenerica<object>> Agregar(UsuariosDTOs modelo)
        {
            RespuestaGenerica<object> respuesta = new()
            {
                Mensaje = "Existe un usuario con la misma información"
            };
            try
            {
                Usuario? existe = await dtx.Usuarios
                    .Where(x => x.Correo.ToLower() == modelo.Correo.ToLower()).FirstOrDefaultAsync();
                if (existe != null)
                {
                    return respuesta;
                }

                existe = new Usuario
                {
                    Id = modelo.Id,
                    Apellidos = modelo.Apellidos,
                    Contrasena = modelo.Contrasena,
                    Correo = modelo.Correo,
                    FechaDeNacimiento = modelo.FechaDeNacimiento,
                    IdRol = modelo.IdRol,
                    Nombre = modelo.Nombre
                };
                await dtx.Usuarios.AddAsync(existe);
                await dtx.SaveChangesAsync();
                modelo.Id = existe.Id;
                respuesta.Mensaje = "Se creo el usuario correctamente.";
                respuesta.Valido = true;
            }
            catch (Exception e)
            {
                respuesta.Mensaje = e.InnerException.Message;
            }
            return respuesta;
        }

        public async Task<RespuestaGenerica<List<UsuariosDTOs>>> Obtener()
        {
            RespuestaGenerica<List<UsuariosDTOs>> respuesta = new();
            try
            {
                respuesta.Objeto = await dtx.Usuarios
                    .Select(x => new UsuariosDTOs(x.Id, x.IdRol, x.Nombre, x.Apellidos, x.FechaDeNacimiento, x.Correo, x.Contrasena))
                    .ToListAsync();
                respuesta.Valido = true;
            }
            catch (Exception e)
            {
                respuesta.Mensaje = e.Message;
            }
            return respuesta;
        }
        #endregion
    }
}
