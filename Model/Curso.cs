using System;
using System.Collections.Generic;

namespace CursosLuis.Api.Model;

public partial class Curso
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public byte Estatus { get; set; }

    public decimal Porcentaje { get; set; }
}
