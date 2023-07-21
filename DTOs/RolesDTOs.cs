using CursosLuis.Api.Model;

namespace CursosLuis.Api.DTOs
{
    public class RolesDTOs
    {
        public RolesDTOs()
        {
                
        }

        public RolesDTOs(
           byte id,
            string nombre
         )
        {
            Id = id;
            Nombre = nombre;
        }



        public byte Id { get; set; }

        public string Nombre { get; set; } = null!;
    }
}
