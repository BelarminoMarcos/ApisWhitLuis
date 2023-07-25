using CursosLuis.Api.DTOs;
using CursosLuis.Api.Model;
using CursosLuis.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CursosLuis.Api.Services
{
    public class SeguridadService : ISeguridadService
    {
        private readonly IConfiguration configuration;
        private readonly CursoDbContext dtx;
        public SeguridadService(CursoDbContext dtx, IConfiguration configuration)
        {
            this.dtx = dtx;
            this.configuration = configuration;
        }
        public async Task<RespuestaGenerica<UsuariosDTOs>> Autenticar(string correo, string contraseña)
        {
            RespuestaGenerica<UsuariosDTOs> respuesta = new();
            try
            {
                Usuario? usuario = await dtx.Usuarios.Where(x => x.Correo.ToLower()
                == correo.ToLower() && x.Contrasena == contraseña
                ).FirstOrDefaultAsync();
                if (usuario != null)
                {
                    var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
                    var credenciales = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature);

                    var subject = new ClaimsIdentity(
                        new[] {
                        new Claim(ClaimTypes.Name, usuario.Id.ToString()),
                        new Claim(ClaimTypes.Role, usuario.IdRol.ToString()),
                        new Claim(ClaimTypes.Email, usuario.Correo)
                        });
                    var experies = DateTime.UtcNow.AddMinutes(10);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = subject,
                        Expires = experies,
                        SigningCredentials = credenciales
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    string jwtToken = tokenHandler.WriteToken(token);
                    respuesta.Objeto = new()
                    {
                        Nombre = usuario.Nombre,
                        Apellidos = usuario.Apellidos,
                        FechaDeNacimiento = usuario.FechaDeNacimiento,
                        IdRol = usuario.IdRol,
                        Token = jwtToken
                    };
                    respuesta.Valido = true;
                }
                else
                {
                    respuesta.Mensaje = "Usuario o Contraseña Incorrectos";
                }
            }
            catch (Exception ex) {
                respuesta.Mensaje = ex.InnerException.Message;
            }
            return respuesta;
        } 
        }    

    }

