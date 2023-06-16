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

        public void Recargar(Peliculas p)
        {
            contenedor.Entry(p).Reload();
        }


        public Peliculas GetPeliculaXID(int id)
        {
            return contenedor.Peliculas.Where(x=>x.IdPelicula == id).FirstOrDefault();
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
            if (string.IsNullOrWhiteSpace(p.Nombre) || p.Nombre == "")
                Errores.Add("Debe ingresar el titulo de la película.");

            if (p.FechaLanzamiento.Year < 1988)
                Errores.Add("La fecha de lanzamiento debe ser de una fecha posterior a 1988.");

            if (p.FechaLanzamiento == null)
                Errores.Add("Debe ingresar la fecha de lanzamiento de la película.");

            if (p.DuracionMinutos < 0)
                Errores.Add("La duración de la película debe ser mayor a 0.");

            if (string.IsNullOrWhiteSpace(p.Genero) || p.Genero == "")
                Errores.Add("Debe seleccionar el género de la película.");


            if (string.IsNullOrWhiteSpace(p.Plataformas) || p.Plataformas == "")
                Errores.Add("Debe ingresar las plataformas en las que se encuentra disponible dicha película.");

            if (string.IsNullOrWhiteSpace(p.Urlposter) || p.Urlposter == "")
                Errores.Add("Debe ingresar el póster de la película.");

            if (string.IsNullOrWhiteSpace(p.Sinopsis) || p.Sinopsis == "")
            {
                Errores.Add("Debe añadir una sinópsis para la película.");
                
            }

            if(p.Sinopsis is not null)
            {
                if (p.Sinopsis.Length > 500)
                    Errores.Add("La sinopsis debe ser menor a 500 caracteres.");
            }
                

            

            if (!Uri.TryCreate(p.Urlposter, UriKind.Absolute, out var uri))
            {
                Errores.Add("Escriba una URL válida para la imagen.");
            }


            return Errores.Count == 0;

        }






    }
}
