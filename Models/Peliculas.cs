using System;
using System.Collections.Generic;

namespace WatchMe.Models;

public partial class Peliculas
{
    public int IdPelicula { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime FechaLanzamiento { get; set; }

    public int DuracionMinutos { get; set; }

    public string Genero { get; set; } = null!;

    public string Plataformas { get; set; } = null!;

    public string Urlposter { get; set; } = null!;

    public int? Puntuacion { get; set; }

    public virtual ICollection<Reseñas> Reseñas { get; set; } = new List<Reseñas>();

    public virtual ICollection<Vermastarde> Vermastarde { get; set; } = new List<Vermastarde>();
}
