
using CursosLuis.Api.Model;
using CursosLuis.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CursosLuis.Api.Services
{
    public class UsuariosService : IUsuariosService
    {
        #region "Atributos"
        private readonly CursoDbContext dtx;
        #endregion

        #region "Contructor"
        public UsuariosService(CursoDbContext dtx) {
            this.dtx = dtx;
        }
        #endregion


        #region "Metodo"
        public async Task<List<Usuario>> Obtener() => await dtx.Usuarios.ToListAsync();
        //public async Task<List<Usuario>> Obtener()
        //{
        //    try
        //    {
        //        await Task.Delay(1000);
        //        return dtx.Usuarios.ToList();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //        return new List<Usuario>();
        //    }
        //}
        //{
        //    return await dtx.Usuarios.ToListAsync();
        //}
        #endregion
    }
}
