using CursosLuis.Api.DTOs;
using CursosLuis.Api.Model;

namespace CursosLuis.Api.Services.Interfaces
{
    public interface IUsuariosService
    {
        #region
        Task<List<Usuario>> Obtener();
        Task<bool> Agregar(UsuariosDTOs agregarUsuario);
        Task<bool> Eliminar(UsuariosDTOs eliminarUsuario);
        Task<bool> Actualizar(UsuariosDTOs actualizarUsuario);


        #endregion




    }
}
