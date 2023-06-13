using System;
using System.Collections.Generic;

namespace WatchMe.Models;

public partial class Usuarios
{
    public int IdUsuario { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public virtual ICollection<Reseñas> Reseñas { get; set; } = new List<Reseñas>();

    public virtual ICollection<Vermastarde> Vermastarde { get; set; } = new List<Vermastarde>();
}
