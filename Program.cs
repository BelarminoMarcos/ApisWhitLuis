using CursosLuis.Api.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddIdentityCore<Secion>()//este se comenta para que jale
//.AddRoles<IdentityRole>();//Este se comenta

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
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
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