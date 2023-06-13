using System;
using System.Collections.Generic;

namespace WatchMe.Models;

public partial class Vermastarde
{
    public int IdVerMasTarde { get; set; }

    public int IdUsuario { get; set; }

    public int IdProduccion { get; set; }

    public virtual Peliculas IdProduccionNavigation { get; set; } = null!;

    public virtual Usuarios IdUsuarioNavigation { get; set; } = null!;
}
