using System;
using System.Collections.Generic;

namespace CursosLuis.Api.Model;

public partial class Usuario
{
    public int Id { get; set; }

    public byte IdRol { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public DateTime FechaDeNacimiento { get; set; }

    public string Correo { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public virtual ICollection<Bitacora> Bitacoras { get; set; } = new List<Bitacora>();

    public virtual Role IdRolNavigation { get; set; } = null!;
}
