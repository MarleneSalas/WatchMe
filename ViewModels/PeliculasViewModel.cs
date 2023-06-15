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
        public string Error { get; set; } = "";

        public Peliculas pelicula { get; set; } = new();
        
        public Peliculas? clonpelicula { get; set; }

        public UserControl Vista { get; set; }

        /*ViewModelUsuarios??
        VerEditarUsuarioRView verEditarUsuarioRView;
        VerUsuarioRView verUsuarioRView;
        VerAgregarUsuarioView verAgregarUsuarioView;
        VerEditarUsuarioView verEditarUsView;
        VerEliminarUsuarioView verEliminarUsuarioView;
        VerUsuariosView verUsuariosView;
        VerUsuarioView verUsuarioView;

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

        public ICommand VerPeliculasCommand { get; set; }

        public ICommand RegresarCommand { get; set; }

        public ObservableCollection<Peliculas> ListaPeliculas { get; set; } = new();

        public PeliculasViewModel()
        {
            //VerRegistrarPeliculaCommand = new RelayCommand(VerRegistrarPelicula);
            //RegistrarPeliculaCommand = new RelayCommand(RegistrarPelicula);
            //VerEliminarPeliculaCommand = new RelayCommand(VerEliminarPelicula);
            //EliminarPeliculaCommand = new RelayCommand(EliminarPelicula);
            //VerEditarPeliculaCommand = new RelayCommand(VerEditarPelicula);
            //EditarPeliculaCommand = new RelayCommand(EditarPelicula);
            //RegresarCommand = new RelayCommand(Regresar);
            VerPeliculasCommand = new RelayCommand(VerPeliculas);
            Actualizar();
        }

        [Authorize(Roles = "Usuario")]
        private void VerPeliculasUsuarioComun()
        {
            Vista = new VerPeliculasUView();
            Actualizar(nameof(Vista));
        }

        [Authorize(Roles = "Administrador")]
        private void VerPeliculasUsuarioAdministrador()
        {
            Vista = new VerPeliculasView();
            Actualizar(nameof(Vista));
        }

        private void VerPeliculas()
        {
            if (Thread.CurrentPrincipal.IsInRole("Administrador"))
            {
                VerPeliculasUsuarioAdministrador();
            }
            if (Thread.CurrentPrincipal.IsInRole("Usuario"))
            {
                VerPeliculasUsuarioComun();
            }
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
