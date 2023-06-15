using GalaSoft.MvvmLight.Command;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        public string Error { get; set; } = "";

        public Peliculas pelicula { get; set; } = new();
        
        public Peliculas? clonpelicula { get; set; }

        public string Vista { get; set; }



        //No sé si va en otro VM, pero lo haré mientras aquí
        public Usuarios? Usuario { get; set; }

        /*
        public ICommand VerRegistrarUsuarioCommand { get; set; }
        public ICommand RegistrarUsuarioCommand { get; set; }

        public ICommand VerEditarUsuarioCommand { get; set; }
        public ICommand EditarUsuarioCommand { get; set; }
        */

        public ICommand VerRegistrarPeliculaCommand { get; set; }
        public ICommand RegistrarPeliculaCommand { get; set; }

        public ICommand VerEliminarPeliculaCommand { get; set; }
        public ICommand EliminarPeliculaCommand { get; set; }

        public ICommand VerEditarPeliculaCommand { get; set; }
        public ICommand EditarPeliculaCommand { get; set; }



        public ICommand VerInicioCommand { get; set; }
        public ICommand VerUsuariosCommand { get; set; }
        public ICommand VerPeliculasCommand { get; set; }
        public ICommand VerReseñasCommand { get; set; }

        public ICommand VerRegistrarUsuarioCommand { get; set; }
        public ICommand RegistrarUsuarioCommand { get; set; }

        //Editar

        public ICommand VerEliminarUsuarioCommand { get; set; }
        public ICommand EliminarUsuarioCommand { get; set; }

        public ICommand RegresarCommand { get; set; }

        public ObservableCollection<Peliculas> ListaPeliculas { get; set; } = new();
        public ObservableCollection<Peliculas> ListaPeliculasAnimacion { get; set; } = new();
        public ObservableCollection<Peliculas> ListaPeliculasDrama { get; set; } = new();
        public ObservableCollection<Peliculas> ListaPeliculasMusical { get; set; } = new();
        public ObservableCollection<Peliculas> ListaPeliculasTerror { get; set; } = new();


        public Peliculas GetPeliculaMejorValorada
        {
            get { return ListaPeliculas.OrderBy(x => x.Puntuacion).First(); }
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
            get { return ListaPeliculas.OrderBy(x => x.Puntuacion).Take(10); }
        }


        //No sé si va en otro VM, pero lo haré mientras aquí
        public ObservableCollection<Usuarios> ListaUsuarios { get; set; } = new();

        public PeliculasViewModel()
        {
            //VerRegistrarPeliculaCommand = new RelayCommand(VerRegistrarPelicula);
            //RegistrarPeliculaCommand = new RelayCommand(RegistrarPelicula);
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

            //No sé si va en otro VM, pero lo haré mientras aquí
            VerRegistrarUsuarioCommand = new RelayCommand(VerRegistrarUsuario);
            RegistrarUsuarioCommand = new RelayCommand(RegistrarUsuario);

            //Editar

            VerEliminarUsuarioCommand = new RelayCommand<int>(VerEliminarUsuario);
            EliminarUsuarioCommand = new RelayCommand(EliminarUsuario);

            RegresarCommand = new RelayCommand(Regresar);

            Actualizar();
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
            Vista = "VerReseñas";
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

        private void Actualizar(string? propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
