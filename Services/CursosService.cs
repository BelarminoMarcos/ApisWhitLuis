using CursosLuis.Api.DTOs;
using CursosLuis.Api.Model;
using CursosLuis.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CursosLuis.Api.Services
{
    public class CursosService : ICursosService
    {
        private readonly CursoDbContext dtx; //crea un atributo de la clase
        public CursosService(CursoDbContext dtx)
        {
            this.dtx = dtx;
        }

        #region "Métodos"
        public async Task<RespuestaGenerica<List<CursoDTOS>>> Obtener()
        {
            RespuestaGenerica<List<CursoDTOS>> respuesta = new();
            try
            {
                respuesta.Objeto = await dtx.Cursos.Select(x => new CursoDTOS(x.Id, x.Nombre, x.Fecha, x.Estatus, x.Porcentaje))
                    .ToListAsync();
                respuesta.Valido = true;
            }
            catch (Exception e)
            {
                respuesta.Mensaje = e.Message;
            }
            return respuesta;
        }
        public async Task<RespuestaGenerica<object>> Actualizar(CursoDTOS cursoss)
        {
            RespuestaGenerica<object> respuesta = new()
            {
                Mensaje = "El rol no existe"
            };
            try
            {
                Curso? existe = await dtx.Cursos.Where(x => x.Id == cursoss.Id).FirstOrDefaultAsync();
                if (existe == null)
                {
                    return respuesta;
                }
                existe.Nombre = cursoss.Nombre;
                existe.Fecha = cursoss.Fecha;
                existe.Estatus = cursoss.Estatus;
                existe.Porcentaje = cursoss.Porcentaje;

                dtx.Cursos.Update(existe);
                await dtx.SaveChangesAsync();
                respuesta.Mensaje = "Se actualizo el rol correctamente";
                respuesta.Objeto = cursoss;
                respuesta.Valido = true;
            }
            catch (Exception e)
            {
                respuesta.Mensaje = e.Message;
            }
            return respuesta;
        }

        public async Task<RespuestaGenerica<CursoDTOS>> Eliminar(int id)
        {
            RespuestaGenerica<CursoDTOS> respuesta = new()
            {
                Mensaje = "El usuario no existe"
            };
            try
            {
                Curso? existe = await dtx.Cursos.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (existe == null)
                {
                    return respuesta;
                }

                dtx.Cursos.Remove(existe);
                await dtx.SaveChangesAsync();
                respuesta.Mensaje = "Se elimino el rol correctamente";
                respuesta.Objeto = new CursoDTOS(existe.Id, existe.Nombre, existe.Fecha, existe.Estatus, existe.Porcentaje);
                respuesta.Valido = true;
            }
            catch (Exception e)
            {
                respuesta.Mensaje = e.Message;
            }
            return respuesta;
        }

        public async Task<RespuestaGenerica<object>> Agregar(CursoDTOS modelo)
        {
            RespuestaGenerica<object> respuesta = new()
            {
                Mensaje = "Existe un rol con la misma información"
            };
            try
            {
                Curso? existe = await dtx.Cursos.Where(x => x.Nombre.ToLower() == modelo.Nombre.ToLower()).FirstOrDefaultAsync();
                if (existe != null)
                {
                    return respuesta;
                }

                existe = new Curso
                {
                    //Id = modelo.Id,
                    Nombre = modelo.Nombre,
                    Fecha = modelo.Fecha,
                    Estatus = modelo.Estatus,
                    Porcentaje = modelo.Porcentaje
                };
                await dtx.Cursos.AddAsync(existe);
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
    } 
}

#endregion