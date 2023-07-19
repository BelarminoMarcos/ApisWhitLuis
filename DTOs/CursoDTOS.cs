namespace CursosLuis.Api.DTOs
{
    public class CursoDTOS
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public DateTime Fecha { get; set; }

        public byte Estatus { get; set; }

        public decimal Porcentaje { get; set; }
    }
}
