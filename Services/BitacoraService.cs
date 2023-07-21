using CursosLuis.Api.DTOs;
using CursosLuis.Api.Model;
using CursosLuis.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CursosLuis.Api.Services
{
    public class BitacoraService : IBitacoraService
    {
        #region "Atributos"
        private readonly CursoDbContext dtx;
        #endregion

        #region "Constructor"
        public BitacoraService(CursoDbContext dtx)
        {
            this.dtx = dtx;
        }
        #endregion




        public async Task<RespuestaGenerica<List<BitacoraDTOs>>> Obtener()
        {
            RespuestaGenerica<List<BitacoraDTOs>> respuesta = new();
            try
            {
                respuesta.Objeto = await dtx.Bitacoras
                    .Select(x => new BitacoraDTOs(x.Id, x.IdUsuario, x.Fecha, x.Accion, x.Modulo, x.Descripcion))
                    .ToListAsync();
                respuesta.Valido = true;
            }
            catch (Exception e)
            {
                respuesta.Mensaje = e.Message;
            }
            return respuesta;
        }


        public async Task<RespuestaGenerica<object>> AgregarP(BitacoraDTOs modelo)
        {
            RespuestaGenerica<object> respuesta = new()
            {
                Mensaje = "Existe un usuario con la misma información"
            };
            try
            {
              
               Bitacora bitacora = new()
                {
                   
                    IdUsuario = modelo.IdUsuario,
                    Fecha = modelo.Fecha,
                    Accion = modelo.Accion,
                    Modulo = modelo.Modulo,
                    Descripcion = modelo.Descripcion
                };
                await dtx.Bitacoras.AddAsync(bitacora);
                await dtx.SaveChangesAsync();
                
                respuesta.Mensaje = "Se creo la bitacora.";
                respuesta.Valido = true;
            }
            catch (Exception es)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                respuesta.Mensaje = es.InnerException.Message;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
            return respuesta;
        }


    }
}
