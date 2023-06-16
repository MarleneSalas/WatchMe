using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WatchMe.Catalogos;
using WatchMe.Models;
using WatchMe.Views.UsuarioViews;

namespace WatchMe.ViewModels
{
    public class ReseñasViewModel:INotifyPropertyChanged
    {
        public IEnumerable<Reseñas> ReseñasXUsuario
        {
            get { return listareseñas.Where(x => x.IdUsuario == reseña.IdUsuario); }
        }

        private void ActualizarBD()
        {
            listareseñas.Clear();
            var proye = catalogoReseñas.GetAllReseñas();
            foreach (var item in proye)
            {
                listareseñas.Add(item);
            }
            Actualizar();
        }

        private void VerEliminarReseña(int id)
        {
            var temp = catalogoReseñas.GetReseñasXPelicula(id);
            if (temp is not null)
            {
                reseña = temp;
                Vista = eliminarreseña;
                Actualizar();
            }
        }

        private void VerEditarReseña(int id)
        {
            reseña = catalogoReseñas.GetReseñasXPelicula(id);
            if(reseña !=null)
            {
                Vista = editarreseña;
                clon = new()
                {
                    IdReseña = reseña.IdReseña,
                    IdProduccion = reseña.IdProduccion,
                    IdUsuario = reseña.IdUsuario,
                    Reseña = reseña.Reseña,
                    Valoracion = reseña.Valoracion
                };
                reseña = clon;
                Actualizar();
            }
        }

        private void ConfirmarReseña()
        {
            if(reseña != null)
            {
                if(catalogoReseñas.Validar(reseña, out List<string> Errores))
                {
                    catalogoReseñas.Agregar(reseña);
                    ActualizarBD();
                    Actualizar();
                    
                }
                else
                {
                    foreach (var item in Errores)
                    {
                        Error = $"{Error} {item} {Environment.NewLine}";
                    }
                    Actualizar();
                }
                Error = "";
            }
        }

        private void EditarReseña()
        {
            var reseñaexistente = catalogoReseñas.GetReseñasXPelicula(reseña.IdReseña);
            if(reseñaexistente != null)
            {
                if(catalogoReseñas.Validar(reseña, out List<string> Errores))
                {
                    reseñaexistente.IdReseña = reseña.IdReseña;
                    reseñaexistente.IdProduccion = reseña.IdProduccion;
                    reseñaexistente.Reseña = reseña.Reseña;
                    reseñaexistente.Valoracion = reseña.Valoracion;
                    catalogoReseñas.Editar(reseñaexistente);
                    Regresar();
                }
                else
                {
                    foreach (var item in Errores)
                    {
                        Error = $"{Error} {item} {Environment.NewLine}";
                    }
                    Actualizar();
                }
                Error = "";
            }
        }

        private void Regresar()
        {
            Vista = verPelicula;
            Actualizar();
        }

        private void EliminarReseña()
        {
            if(reseña is not null)
            {
                catalogoReseñas.Eliminar(reseña);
                ActualizarBD();
                Regresar();

            }
        }

        private void RegistrarReseña()
        {
            Error = "";
            reseña = new();
            Vista = hacerreseña;
            Actualizar();
        }

        private void Actualizar(string? propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }


    }
}
