namespace CursosLuis.Api.DTOs
{
    public class UsuariosDTOs
    {
        public UsuariosDTOs()
        {
                
        }
        public UsuariosDTOs(
                    int id,
                    byte idRol,
                    string nombre,
                    string apellidos,
                    DateTime fechaNacimiento,
                    string correo,
                    string contrasena)
        {
            Id = id;
            IdRol = idRol;
            Nombre = nombre;
            Apellidos = apellidos;
            FechaDeNacimiento = fechaNacimiento;
            Correo = correo;
            Contrasena = contrasena;
        }
        public int Id { get; set; }

        public byte IdRol { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellidos { get; set; } = null!;

        public DateTime FechaDeNacimiento { get; set; }

        public string Correo { get; set; } = null!;

        public string Contrasena { get; set; } = null!;
    }
}
