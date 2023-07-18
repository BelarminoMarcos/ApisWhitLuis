using CursosLuis.Api.Model;

namespace CursosLuis.Api.Services.Interfaces
{
    public interface IUsuariosService
    {
        #region
        Task<List<Usuario>> Obtener();
        #endregion
        
       
        

    }
}
