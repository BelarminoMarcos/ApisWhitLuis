using CursosLuis.Api.DTOs;

namespace CursosLuis.Api.Services.Interfaces
{
    public interface ISeguridadService
    {//implementaciones
        Task<RespuestaGenerica<UsuariosDTOs>> Autenticar(string correo, string contrasena);
    }
}
