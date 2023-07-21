using CursosLuis.Api.DTOs;

namespace CursosLuis.Api.Services.Interfaces
{
    public interface IBitacoraService
    {
        Task<RespuestaGenerica<List<BitacoraDTOs>>> Obtener();
        Task<RespuestaGenerica<object>> AgregarP(BitacoraDTOs agregarUsuario);
    }
}
