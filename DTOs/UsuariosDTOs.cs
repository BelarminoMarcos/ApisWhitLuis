namespace CursosLuis.Api.DTOs
{
    public class UsuariosDTOs
    {
        public int Id { get; set; }

        public byte IdRol { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellidos { get; set; } = null!;

        public DateTime FechaDeNacimiento { get; set; }

        public string Correo { get; set; } = null!;

        public string Contrasena { get; set; } = null!;
    }
}
