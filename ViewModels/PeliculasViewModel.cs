using GalaSoft.MvvmLight.Command;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WatchMe.Catalogos;
using WatchMe.Models;
using WatchMe.Views;
using WatchMe.Views.AdministradorViews;
using WatchMe.Views.UsuarioViews;

namespace WatchMe.ViewModels
{
    public class PeliculasViewModel : INotifyPropertyChanged
    {
        PeliculasCatalogo catalogop = new();
        UsuariosCatalogo catalogou = new();
        private ReseñasCatalogo catalogoReseñas = new();

        VerEditarReseñaUView editarreseña;
        VerEliminarReseñasUView eliminarreseña;
        VerHacerReseñaView hacerreseña;
        VerPeliculaUView verPelicula;


        public IEnumerable<Reseñas> ReseñasXUsuario
        {
            get { return listareseñas.Where(x => x.IdUsuario == Usuario.IdUsuario); }
        }




        public Reseñas reseña { get; set; } = new();
        public Reseñas clon { get; set; }

        public string Error { get; set; } = "";

        public Peliculas pelicula { get; set; } = new();
        
        public Peliculas? clonpelicula { get; set; }

        public string Vista { get; set; }



        //No sé si va en otro VM, pero lo haré mientras aquí
        public Usuarios? Usuario { get; set; }

        /*
        public ICommand VerRegistrarUsuarioCommand { get; set; }
        public ICommand RegistrarUsuarioCommand { get; set; }

        
        */

        public ICommand VerEditarUsuarioCommand { get; set; }
        public ICommand EditarUsuarioCommand { get; set; }

        public ICommand VerInicioCommand { get; set; }
        public ICommand VerUsuariosCommand { get; set; }
        public ICommand VerPeliculasCommand { get; set; }
        public ICommand VerReseñasCommand { get; set; }

        public ICommand VerPeliculaCommand { get; set; }

        //public ICommand VerRegistrarUsuarioCommand { get; set; }
        //public ICommand RegistrarUsuarioCommand { get; set; }

        ////Editar

        public ICommand VerEliminarUsuarioCommand { get; set; }
        public ICommand EliminarUsuarioCommand { get; set; }


        public ICommand VerRegistrarPeliculaCommand { get; set; }
        public ICommand RegistrarPeliculaCommand { get; set; }

        public ICommand VerEliminarPeliculaCommand { get; set; }
        public ICommand EliminarPeliculaCommand { get; set; }

        public ICommand VerEditarPeliculaCommand { get; set; }
        public ICommand EditarPeliculaCommand { get; set; }

        public ICommand RegresarCommand { get; set; }


        public ICommand RegistrarReseñaCommand { get; set; }

        public ICommand VerEliminarReseñaCommand { get; set; }
        public ICommand EliminarReseñaCommand { get; set; }

        public ICommand VerEditarReseñaCommand { get; set; }
        public ICommand EditarReseñaCommand { get; set; }
        public ICommand VerPerfilUsuarioCommand { get; set; }

        public ICommand ConfirmarReseñaCommand { get; set; }
        public ICommand RegresarReseñasCommand { get; set; }
        public ICommand RegresarAReseñasXUsuarioCommand { get; set; }

        public ObservableCollection<Peliculas> ListaPeliculas { get; set; } = new();

        public Peliculas GetPeliculaMejorValorada
        {
            get { return ListaPeliculas.OrderByDescending(x => x.Puntuacion).First(); }
        }

        public IEnumerable<Peliculas> GetPeliculasGeneroAnimacion
        {
            get { return ListaPeliculas.Where(x => x.Genero == "Animación"); }
        }

        public IEnumerable<Peliculas> GetPeliculasGeneroDrama
        {
            get { return ListaPeliculas.Where(x => x.Genero == "Drama"); }
        }
        public IEnumerable<Peliculas> GetPeliculasGeneroMusical
        {
            get { return ListaPeliculas.Where(x => x.Genero == "Musical"); }
        }

        public IEnumerable<Peliculas> GetPeliculasGeneroTerror
        {
            get { return ListaPeliculas.Where(x => x.Genero == "Terror"); }
        }
        public IEnumerable<Peliculas> GetPeliculasMejorValoradas
        {
            get { return ListaPeliculas.OrderByDescending(x => x.Puntuacion).Take(10); }
        }

        //No sé si va en otro VM, pero lo haré mientras aquí
        public ObservableCollection<Usuarios> ListaUsuarios { get; set; } = new();
        private ObservableCollection<Reseñas> listareseñas { get; set; } = new();

        public IEnumerable<Reseñas> GetReseñasXPelicula
        {
            get { return listareseñas.Where(x => x.IdProduccion == pelicula.IdPelicula); }
           
        }
        public PeliculasViewModel()
        {
            
            VerRegistrarPeliculaCommand = new RelayCommand(VerRegistrarPelicula);
            RegistrarPeliculaCommand = new RelayCommand(RegistrarPelicula);
            //VerEliminarPeliculaCommand = new RelayCommand(VerEliminarPelicula);
            //EliminarPeliculaCommand = new RelayCommand(EliminarPelicula);
            //VerEditarPeliculaCommand = new RelayCommand(VerEditarPelicula);
            //EditarPeliculaCommand = new RelayCommand(EditarPelicula);
            ActualizarBD();
            Vista = "Peliculas";
            VerInicioCommand = new RelayCommand(VerInicio);
            VerUsuariosCommand = new RelayCommand(VerUsuarios);
            VerPeliculasCommand = new RelayCommand(VerPeliculas);
            VerReseñasCommand = new RelayCommand(VerReseñas);
            VerPerfilUsuarioCommand = new RelayCommand(VerPerfilUsuario);
            VerPeliculaCommand = new RelayCommand<Peliculas>(VerPelicula);

            //No sé si va en otro VM, pero lo haré mientras aquí
            //VerRegistrarUsuarioCommand = new RelayCommand(VerRegistrarUsuario);
            //RegistrarUsuarioCommand = new RelayCommand(RegistrarUsuario);

            //Editar

            //VerEliminarUsuarioCommand = new RelayCommand<int>(VerEliminarUsuario);
            //EliminarUsuarioCommand = new RelayCommand(EliminarUsuario);

            RegistrarReseñaCommand = new RelayCommand(RegistrarReseña);
            VerEliminarReseñaCommand = new RelayCommand<int>(VerEliminarReseña);
            EliminarReseñaCommand = new RelayCommand(EliminarReseña);
            VerEditarReseñaCommand = new RelayCommand<int>(VerEditarReseña);
            EditarReseñaCommand = new RelayCommand(EditarReseña);
            ConfirmarReseñaCommand = new RelayCommand(ConfirmarReseña);
            RegresarAReseñasXUsuarioCommand = new RelayCommand(RegresarAReseñasXUsuario);

            RegresarCommand = new RelayCommand(Regresar);
            RegresarReseñasCommand = new RelayCommand(RegresarReseñas);

            if (Application.Current.Properties.Contains("UsuarioActual"))
            {
                Usuario = Application.Current.Properties["UsuarioActual"] as Usuarios;
            }
            ActualizarReseñas();
            Actualizar();
            
        }

        private void VerPerfilUsuario()
        {
            Vista = "VerUsuarioR";
            Actualizar();
        }

        private void RegresarAReseñasXUsuario()
        {
            Vista = "VerReseñasU";
            Actualizar();
        }

        private void VerPelicula(Peliculas p)
        {
            if (Thread.CurrentPrincipal != null)
            {
                pelicula = p;
                if (Thread.CurrentPrincipal.IsInRole("Administrador"))
                {
                    Vista = "VerPeliculaR";
                }
                if (Thread.CurrentPrincipal.IsInRole("Usuario"))
                {
                    Vista = "VerPeliculaU";
                }
            }
            Actualizar();
        }

        public void VerRegistrarPelicula()
        {
            Error = "";
            Vista = "VerAgregarPelicula";
            Actualizar();
        }

        private void RegistrarPelicula()
        {
            if (pelicula != null)
            {
                if (catalogop.Validar(pelicula,out List<string> Errores))
                {
                    catalogop.Agregar(pelicula);
                    ActualizarBD();
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
            }
        }

        private void VerEliminarUsuario(int obj)
        {
            Usuario = catalogou.GetUsuarioID(obj);
            Vista = "VerEliminarUsuario";
            Actualizar();
        }

        private void EliminarUsuario()
        {
            if (Usuario != null)
            {
                catalogou.Eliminar(Usuario);
                ActualizarBD();
                Regresar();
            }
        }

        private void Regresar()
        {
            Vista = "VerInicio";
            Actualizar();
        }

        private void VerRegistrarUsuario()
        {
            Usuario = new();
            Vista = "VerRegistrar";
            Actualizar();
        }

        private void RegistrarUsuario()
        {
            throw new NotImplementedException();
        }

        private void VerInicio()
        {
            if (Thread.CurrentPrincipal != null)
            {
                if (Thread.CurrentPrincipal.IsInRole("Administrador"))
                {
                    Vista = "VerInicio";
                }
                if (Thread.CurrentPrincipal.IsInRole("Usuario"))
                {
                    Vista = "VerInicioU";
                }
            }
            Actualizar();
        }

        private void VerUsuarios()
        {
            Vista = "VerUsuarios";
            Actualizar();
        }

        private void VerPeliculas()
        {
            if (Thread.CurrentPrincipal != null)
            {
                if (Thread.CurrentPrincipal.IsInRole("Administrador"))
                {
                    Vista = "VerPeliculas";
                }
                if (Thread.CurrentPrincipal.IsInRole("Usuario"))
                {
                    Vista = "VerPeliculasU";
                }
            }
            Actualizar();
        }

        private void VerReseñas()
        {
            if (Thread.CurrentPrincipal != null)
            {
                if (Thread.CurrentPrincipal.IsInRole("Administrador"))
                {
                    Vista = "VerReseñas";
                    ActualizarReseñas();
                }
                if (Thread.CurrentPrincipal.IsInRole("Usuario"))
                {
                    Vista = "VerReseñasU";
                }
            }
            Actualizar();
        }

        private void ActualizarBD()
         {
            ListaPeliculas.Clear();
            foreach (var item in catalogop.GetAllPeliculas())
            {
                ListaPeliculas.Add(item);
            }
            Actualizar();
        }


        private void ActualizarReseñas()
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
            var temp = catalogoReseñas.GetReseñaXID(id);
            if (temp is not null)
            {
                reseña = temp;
                Vista = "VerEliminarReseñaU";
                Actualizar();
            }
        }

        private void VerEditarReseña(int id)
        {
            reseña = catalogoReseñas.GetReseñaXID(id);
            if (reseña != null)
            {
                Vista = "VerEditarReseña";
                clon = new()
                {
                    IdReseña = reseña.IdReseña,
                    IdProduccion = reseña.IdProduccion,
                    IdUsuario = reseña.IdUsuario,
                    Reseña = reseña.Reseña,
                    Valoracion = reseña.Valoracion,
                    IdProduccionNavigation = reseña.IdProduccionNavigation,
                    IdUsuarioNavigation = reseña.IdUsuarioNavigation
                };
                reseña = clon;
                Actualizar();
            }
        }

        private void ConfirmarReseña()
        {
            if (reseña != null)
            {
                reseña.IdUsuario = Usuario.IdUsuario;
                reseña.IdProduccion = pelicula.IdPelicula;
                if (catalogoReseñas.Validar(reseña, out List<string> Errores))
                {
                    catalogoReseñas.Agregar(reseña);
                    Vista = "VerPeliculaU";
                    ActualizarReseñas();
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
            var reseñaexistente = catalogoReseñas.GetReseñaXID(reseña.IdReseña);
            if (reseñaexistente != null)
            {
                if (catalogoReseñas.Validar(reseña, out List<string> Errores))
                {
                    reseñaexistente.IdReseña = reseña.IdReseña;
                    reseñaexistente.IdProduccion = reseña.IdProduccion;
                    reseñaexistente.Reseña = reseña.Reseña;
                    reseñaexistente.Valoracion = reseña.Valoracion;
                    catalogoReseñas.Editar(reseñaexistente);
                    RegresarAReseñasXUsuario();
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

        private void RegresarReseñas()
        {
            Vista = "VerPeliculasU";
            Actualizar();
        }

        private void EliminarReseña()
        {
            if (reseña is not null)
            {
                catalogoReseñas.Eliminar(reseña);
                ActualizarReseñas();
                RegresarReseñas();

            }
        }

        private void RegistrarReseña()
        {
            Error = "";
            reseña = new();
            Vista = "VerHacerReseña";
            Actualizar();
        }

        private void Actualizar(string? propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
