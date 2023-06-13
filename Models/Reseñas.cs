using System;
using System.Collections.Generic;

namespace WatchMe.Models;

public partial class Reseñas
{
    public int IdReseña { get; set; }

    public int IdUsuario { get; set; }

    public int IdProduccion { get; set; }

    public int Valoracion { get; set; }

    public string Reseña { get; set; } = null!;

    public virtual Peliculas IdProduccionNavigation { get; set; } = null!;

    public virtual Usuarios IdUsuarioNavigation { get; set; } = null!;
}
