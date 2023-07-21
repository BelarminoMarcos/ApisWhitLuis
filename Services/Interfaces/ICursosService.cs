using CursosLuis.Api.DTOs;

namespace CursosLuis.Api.Services.Interfaces
{
    public interface ICursosService
    {
        Task<RespuestaGenerica<List<CursoDTOS>>> Obtener();
        Task<RespuestaGenerica<object>> Agregar(CursoDTOS rol);
        Task<RespuestaGenerica<object>> Actualizar(CursoDTOS rol);
        Task<RespuestaGenerica<CursoDTOS>> Eliminar(int id);
    }
}
