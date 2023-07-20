namespace CursosLuis.Api.DTOs
{
    public class RespuestaGenerica<T> where T : new()
    {
        #region
        public T Objeto { get; set; }
        public string Mensaje { get; set; } = null!;
        public bool Valido { get; set; }

    #endregion
}
}
