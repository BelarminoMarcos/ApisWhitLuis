
using CursosLuis.Api.DTOs;
using CursosLuis.Api.Model;
using CursosLuis.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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


        public async Task<bool> Agregar(UsuariosDTOs crearUsuario)
        {
            Usuario registrado = new Usuario
            {
                IdRol = crearUsuario.IdRol,
                Nombre = crearUsuario.Nombre,
                Apellidos = crearUsuario.Apellidos,
                FechaDeNacimiento = crearUsuario.FechaDeNacimiento,
                Correo = crearUsuario.Correo,
                Contrasena = crearUsuario.Contrasena,
           
            };
            var registroUser = dtx.Usuarios.Add(registrado);
            await dtx.SaveChangesAsync();
            return true;
        }


        public async Task<bool> Actualizar(UsuariosDTOs actualizarUsuario)
        {
            Usuario actualizando = new Usuario
            {
                IdRol = actualizarUsuario.IdRol,
                Id = actualizarUsuario.Id,
                Nombre = actualizarUsuario.Nombre,
                Apellidos = actualizarUsuario.Apellidos,
                FechaDeNacimiento = actualizarUsuario.FechaDeNacimiento,
                Correo = actualizarUsuario.Correo,
                Contrasena = actualizarUsuario.Contrasena,
            
            };
            var actualizado = dtx.Usuarios.Update(actualizando);
            await dtx.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(UsuariosDTOs eliminarUsuario)
        {
            Usuario eliminado = new Usuario
            {
                Id = eliminarUsuario.Id
            };
            var eliminar = dtx.Usuarios.Remove(eliminado);
            await dtx.SaveChangesAsync();
            return true;
        }



        //public async Task AgregarUsuarioAsync(Usuario nuevoUsuario)
        //{
        //    dtx.Usuarios.Add(nuevoUsuario);
        //    await dtx.SaveChangesAsync();
        //}
        //public async Task<(int StatusCode, string Mensaje)> Create(UsuariosDTOs usuarios)
        //{
        //    try
        //    {
        //        var newUser = new Usuario
        //        {

        //            Id = usuarios.Id,
        //            IdRol = usuarios.IdRol,
        //            Nombre = usuarios.Nombre,
        //            FechaDeNacimiento = usuarios.FechaDeNacimiento,
        //            Correo = usuarios.Correo,
        //            Contrasena = usuarios.Contrasena,
        //           // IdRolNavigation = usuarios.IdRolNavigation
        //        };
        //        dtx.Add(newUser);
        //        await dtx.SaveChangesAsync();
        //        return (StatusCodes.Status201Created, "Registro exitoso");
        //    }
        //    catch
        //    {
        //        return (StatusCodes.Status500InternalServerError, "Error en el guardado");
        //    }
        //}


        //public async Task<List<Usuario>> create() => await dtx.Usuarios.ToListAsync();




        //public async Task<Usuario> Create(Usuario nuevoUsuario) Este es el bueno si no jala el de arriba
        //{
        //    dtx.Usuarios.Add(nuevoUsuario);
        //    await dtx.SaveChangesAsync();
        //    return (nuevoUsuario);
        //}

        //public async Task<( int StatusCode, string Mensaje)> Create(Usuario usuarios)
        //{
        //    try
        //    {
        //        var newUser = new Usuario
        //        {

        //            Id = usuarios.Id,
        //            IdRol = usuarios.IdRol,
        //            Nombre = usuarios.Nombre,
        //            FechaDeNacimiento = usuarios.FechaDeNacimiento,
        //            Correo = usuarios.Correo,
        //            Contrasena = usuarios.Contrasena,
        //            IdRolNavigation = usuarios.IdRolNavigation
        //        };
        //        dtx.Add(newUser);
        //        await dtx.SaveChangesAsync();
        //        return (StatusCodes.Status201Created, "Registro exitoso");
        //    }
        //    catch
        //    {
        //        return (StatusCodes.Status500InternalServerError, "Error en el guardado");
        //    }
        //}


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
