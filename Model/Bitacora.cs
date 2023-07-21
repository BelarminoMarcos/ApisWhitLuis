using System;
using System.Collections.Generic;

namespace CursosLuis.Api.Model;

public partial class Bitacora
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public DateTime Fecha { get; set; }

    public byte Accion { get; set; }

    public byte Modulo { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
