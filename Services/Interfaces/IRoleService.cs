using CursosLuis.Api.DTOs;

namespace CursosLuis.Api.Services.Interfaces
{
    public interface IRoleService
    {
        Task<RespuestaGenerica<List<RolesDTOs>>> Obtener();
        Task<RespuestaGenerica<object>> Agregar(RolesDTOs rol);
        Task<RespuestaGenerica<object>> Actualizar(RolesDTOs rol);
        Task<RespuestaGenerica<RolesDTOs>> Eliminar(int id);
    }
}
