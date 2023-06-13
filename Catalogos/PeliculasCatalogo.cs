using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchMe.Models;

namespace WatchMe.Catalogos
{
    public class PeliculasCatalogo
    {
        WatchMeContext contenedor = new();

        public IEnumerable<Peliculas> GetAllPeliculas()
        {
            return contenedor.Peliculas;
        }

        public Peliculas GetPeliculaXID(Peliculas p)
        {
            return contenedor.Peliculas.Find(p.IdPelicula);
        }

        public void Agregar(Peliculas p)
        {
            contenedor.Add(p);
            contenedor.SaveChanges();
        }

        public void Eliminar(Peliculas p)
        {
            contenedor.Remove(p);
            contenedor.SaveChanges();
        }

        public void Editar(Peliculas p)
        {
            contenedor.Update(p);
            contenedor.SaveChanges();
        }


        public bool Validar(Peliculas p, out List<string> Errores)
        {
            Errores = new();
            if (string.IsNullOrWhiteSpace(p.Nombre))
                Errores.Add("Debe ingresar el titulo de la película.");

            if (p.FechaLanzamiento.Year < 1988)
                Errores.Add("La fecha de lanzamiento debe ser de una fecha posterior a 1988.");

            if (p.FechaLanzamiento == null)
                Errores.Add("Debe ingresar la fecha de lanzamiento de la película.");

            if (p.DuracionMinutos < 0)
                Errores.Add("La duración de la película debe ser mayor a 0.");

            if (string.IsNullOrWhiteSpace(p.Genero))
                Errores.Add("Debe seleccionar el género de la película.");

            if (string.IsNullOrWhiteSpace(p.Plataformas))
                Errores.Add("Debe ingresar las plataformas en las que se encuentra disponible dicha película");

            if (string.IsNullOrWhiteSpace(p.Urlposter))
                Errores.Add("Debe ingresar el póster de la película");


            return Errores.Count == 0;

        }






    }
}
