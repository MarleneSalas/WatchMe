using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchMe.Models;

namespace WatchMe.Catalogos
{
    public class ReseñasCatalogo
    {
        WatchMeContext contenedor = new();

        public IEnumerable<Reseñas> GetAllReseñas()
        {
            return contenedor.Reseñas.Include(x=>x.IdUsuarioNavigation);
        }

        public Reseñas GetReseñaXID(int id)
        {
            return contenedor.Reseñas.Where(x => x.IdReseña == id).FirstOrDefault();
        }
       

        public void Agregar(Reseñas r)
        {
            contenedor.Add(r);
            contenedor.SaveChanges();
        }

        public void Eliminar(Reseñas r)
        {
            contenedor.Remove(r);
            contenedor.SaveChanges();
        }
        public void Editar(Reseñas r)
        {
            contenedor.Update(r);
            contenedor.SaveChanges();
        }

        public bool Validar(Reseñas r, out List<string> Errores)
        {
            Errores = new();
            if (string.IsNullOrWhiteSpace(r.Reseña))
            {
                Errores.Add("Debe dejar alguna opinión para la película.");
            }
            if(r.Valoracion < 0)
            {
                Errores.Add("Debe dejar su valoración entre un rango de 0-100");
            }

            if(r.Reseña != null)
            {
                if (r.Reseña.Length > 250)
                    Errores.Add("La reseña puede contener una longitud máxima de 250 caracteres");
            }


            return Errores.Count == 0;
        }
    }
}
