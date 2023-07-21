using Microsoft.AspNetCore.Identity;

namespace CursosLuis.Api.Helpers
{
    public class Secion : IdentityUser
    {
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
    }
}
