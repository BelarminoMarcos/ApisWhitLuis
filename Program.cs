using CursosLuis.Api.Helpers;
using CursosLuis.Api.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client.Extensibility;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddIdentityCore<Secion>()//este se comenta para que jale
//.AddRoles<IdentityRole>();//Este se comenta


//builder.Services.AddIdentityCore<Secion>()         Estas tambien podemos borarlas si es que no jalan
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<CursoDbContext>();


//builder.Services.AddIdentity<Secion, IdentityRole>()//Esta y_______________En la configuración del servicio, utiliza AddIdentity en lugar de AddIdentityCore para configurar la autenticación y autorización, y luego usa AddEntityFrameworkStores para especificar el contexto de la base de datos:
//    .AddEntityFrameworkStores<CursoDbContext>();//Esta debor borrarlas si no jala es mi implementacion
//builder.Services.AddAuthorization(o =>//esta Tambien se quita si no jala
//{
//    o.AddPolicy("SoloAdministrador", policy => policy.RequireRole("Administrador"));//esta igual se quita si no jala
//});



builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).AddEnvironmentVariables();
//CONTROLA LOS ROLES DE LA SESSION DE UN USUARIO
builder.Services.AgregarDependencias(builder.Configuration);
//controla las solicitudes
builder.Services.AddHttpContextAccessor();
//controla la autorizacion en los controladores
builder.Services.AddAuthorization();
//controla el esquema de auntetificacion
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //configura la estructura y validacion de un token de un token 
    .AddJwtBearer(o => {
        o.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ClockSkew = TimeSpan.Zero
        };
        o.Events = new JwtBearerEvents
        {
            OnTokenValidated = ctx =>
            {
                if (ctx.SecurityToken is JwtSecurityToken accessToken)
                {
                    if (ctx.Principal.Identities is ClaimsIdentity identity)
                    {
                        identity.AddClaim(new Claim("access_Token", accessToken.RawData));
                    }
                }

                return Task.CompletedTask;
            },
            OnAuthenticationFailed = ctx =>
            {
                if (ctx.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    ctx.Response.Headers.Add("Token-Expired", "true");
                }
                return Task.CompletedTask;
            }
        };

    });
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

