namespace CursosLuis.Api.DTOs
{
    public class BitacoraDTOs
    {
        public BitacoraDTOs()
        {
                
        }

        public BitacoraDTOs(
              int id,
                int idUsuario,
               DateTime fecha,
                 byte accion,
                byte modulo,
                string descripcion

            )
        {
            Id = id;
            IdUsuario = idUsuario;
            Fecha = fecha;
            Accion = accion;
            Modulo = modulo;
            Descripcion = descripcion;
          
        }

        public int Id { get; set; }

        public int IdUsuario { get; set; }

        public DateTime Fecha { get; set; }

        public byte Accion { get; set; }

        public byte Modulo { get; set; }

        public string Descripcion { get; set; } = null!;
    }
}
