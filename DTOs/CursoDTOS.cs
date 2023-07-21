using CursosLuis.Api.Model;

namespace CursosLuis.Api.DTOs
{
    public class CursoDTOS
    {
        public CursoDTOS()
        {
                
        }

        public CursoDTOS(
      int id,
        string nombre,
       DateTime fecha,
        byte estatus,
        decimal porcentaje

    )
        {
            Id = id;
            Nombre = nombre;
            Fecha = fecha;
            Estatus = estatus;
            Porcentaje = porcentaje;
          

        }

        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public DateTime Fecha { get; set; }

        public byte Estatus { get; set; }

        public decimal Porcentaje { get; set; }
    }
}
