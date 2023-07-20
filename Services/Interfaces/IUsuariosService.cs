using CursosLuis.Api.DTOs;
using CursosLuis.Api.Model;

namespace CursosLuis.Api.Services.Interfaces
{
    public interface IUsuariosService
    {
        #region
        Task<RespuestaGenerica<List<UsuariosDTOs>>> Obtener();
        Task<RespuestaGenerica<object>> Agregar(UsuariosDTOs agregarUsuario);

        //Task<bool> Eliminar(UsuariosDTOs eliminarUsuario);
        Task<RespuestaGenerica<UsuariosDTOs>> Eliminar(int id);
        Task<RespuestaGenerica<object>> Actualizar(UsuariosDTOs actualizarUsuario);




        #endregion




    }
}
