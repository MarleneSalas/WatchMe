using GalaSoft.MvvmLight.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Versioning;
using System.Security.Cryptography;
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
        //Catalogos
        PeliculasCatalogo catalogop = new();
        UsuariosCatalogo catalogou = new();
        private ReseñasCatalogo catalogoReseñas = new();
        PrincipalViewModel pvm = new();

        //Listas
        public ObservableCollection<Peliculas> ListaPeliculas { get; set; } = new();
        public ObservableCollection<Usuarios> ListaUsuarios { get; set; } = new();
        public ObservableCollection<Reseñas> listareseñas { get; set; } = new();
        public ObservableCollection<Peliculas> listapeliculasfiltrado { get; set; } = new();

        //Consultas
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

        public IEnumerable<Reseñas> ReseñasXUsuario
        {
            get { return listareseñas.Where(x => x.IdUsuario == Usuario.IdUsuario); }
        }

        public IEnumerable<Usuarios> GetUsuariosAdmin
        {
            get { return ListaUsuarios.Where(x => x.Rol == "Administrador"); }
        }

        public IEnumerable<Usuarios> GetUsuariosComunes
        {
            get { return ListaUsuarios.Where(x => x.Rol == "Usuario"); }
        }

        public IEnumerable<Reseñas> GetReseñasXPelicula
        {
            get { return listareseñas.Where(x => x.IdProduccion == pelicula.IdPelicula); }

        }

        
        
        //Objetos
        public Reseñas reseña { get; set; } = new();
        public Reseñas clon { get; set; }

        public string Error { get; set; } = "";

        public Peliculas pelicula { get; set; } = new();
        
        public Peliculas? clonpelicula { get; set; }

        public string Vista { get; set; }

        public Usuarios? Usuario { get; set; }
        public Usuarios? clonusuario { get; set; }

        //Películas
        public ICommand VerRegistrarPeliculaCommand { get; set; }
        public ICommand RegistrarPeliculaCommand { get; set; }
        public ICommand VerEliminarPeliculaCommand { get; set; }
        public ICommand EliminarPeliculaCommand { get; set; }
        public ICommand VerEditarPeliculaCommand { get; set; }
        public ICommand EditarPeliculaCommand { get; set; }
        public ICommand VerPeliculaCommand { get; set; }
        public ICommand CancelarEdicionPeliculaCommand { get; set; }

        //IndexNavigation
        public ICommand VerInicioCommand { get; set; }
        public ICommand VerUsuariosCommand { get; set; }
        public ICommand VerPeliculasCommand { get; set; }
        public ICommand VerReseñasCommand { get; set; }
        public ICommand VerPerfilUsuarioCommand { get; set; }
        public ICommand RegresarCommand { get; set; }

        //Usuarios
        public ICommand VerEliminarUsuarioCommand { get; set; }
        public ICommand EliminarUsuarioCommand { get; set; }

        public ICommand VerEditarUsuarioCommand { get; set; }
        public ICommand EditarUsuarioCommand { get; set; }
        public ICommand CerrarSesionCommand { get; set; }

        //Reseñas
        public ICommand RegistrarReseñaCommand { get; set; }
        public ICommand VerEliminarReseñaCommand { get; set; }
        public ICommand EliminarReseñaCommand { get; set; }
        public ICommand VerEditarReseñaCommand { get; set; }
        public ICommand EditarReseñaCommand { get; set; }
        public ICommand ConfirmarReseñaCommand { get; set; }
        public ICommand RegresarAReseñasXUsuarioCommand { get; set; }
        public ICommand RegresarReseñasCommand { get; set; }

        public PeliculasViewModel()
        {
            //Películas
            VerRegistrarPeliculaCommand = new RelayCommand(VerRegistrarPelicula);
            RegistrarPeliculaCommand = new RelayCommand(RegistrarPelicula);
            VerEliminarPeliculaCommand = new RelayCommand(VerEliminarPelicula);
            EliminarPeliculaCommand = new RelayCommand(EliminarPelicula);
            VerEditarPeliculaCommand = new RelayCommand(VerEditarPelicula);
            EditarPeliculaCommand = new RelayCommand(EditarPelicula);
            VerPeliculaCommand = new RelayCommand<Peliculas>(VerPelicula);
            CancelarEdicionPeliculaCommand = new RelayCommand(CancelarEdicionPelicula);

            //IndexNavigation
            VerInicioCommand = new RelayCommand(VerInicio);
            VerUsuariosCommand = new RelayCommand(VerUsuarios);
            VerPeliculasCommand = new RelayCommand(VerPeliculas);
            VerReseñasCommand = new RelayCommand(VerReseñas);
            VerPerfilUsuarioCommand = new RelayCommand(VerPerfilUsuario);
            RegresarCommand = new RelayCommand(Regresar);
            FiltroBuscadorPeliculasCommand = new RelayCommand<string>(FiltroBuscadorPeliculas);

            //Usuarios
            VerEliminarUsuarioCommand = new RelayCommand<Usuarios>(VerEliminarUsuario);
            EliminarUsuarioCommand = new RelayCommand(EliminarUsuario);

            //Reseñas
            RegistrarReseñaCommand = new RelayCommand(RegistrarReseña);
            VerEliminarReseñaCommand = new RelayCommand<int>(VerEliminarReseña);
            EliminarReseñaCommand = new RelayCommand(EliminarReseña);
            VerEditarReseñaCommand = new RelayCommand<int>(VerEditarReseña);
            EditarReseñaCommand = new RelayCommand(EditarReseña);
            ConfirmarReseñaCommand = new RelayCommand(ConfirmarReseña);
            RegresarAReseñasXUsuarioCommand = new RelayCommand(RegresarAReseñasXUsuario);
            RegresarReseñasCommand = new RelayCommand(RegresarReseñas);
            if (Application.Current.Properties.Contains("UsuarioActual"))
            {
                Usuario = Application.Current.Properties["UsuarioActual"] as Usuarios;
            }

            //Actualizar listas
            ActualizarReseñas();
            ActualizarUsuarios();
            ActualizarBD();
            Actualizar();
        }

        private void EditarUsuario()
        {
            Error = "";
            var usuarioexistente = catalogou.GetUsuarioID(Usuario.IdUsuario);
            if (usuarioexistente != null)
            {
                if (catalogou.Validar(Usuario, out List<string> Errores))
                {
                    usuarioexistente.IdUsuario = Usuario.IdUsuario;
                    usuarioexistente.NombreUsuario = Usuario.NombreUsuario;
                    usuarioexistente.CorreoElectronico = Usuario.CorreoElectronico;
                    usuarioexistente.Contraseña = Usuario.Contraseña;
                    usuarioexistente.Rol = Usuario.Rol;

                    catalogou.Editar(usuarioexistente);
                    if (Thread.CurrentPrincipal != null)
                    {
                        if (Thread.CurrentPrincipal.IsInRole("Administrador"))
                        {
                            Vista = "VerUsuario";
                            ActualizarReseñas();
                        }
                        if (Thread.CurrentPrincipal.IsInRole("Usuario"))
                        {
                            Vista = "VerUsuarioR";
                        }
                    }
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
            }
        }

        private void VerEditarUsuario()
        {
            if (Usuario != null)
            {
                if (Thread.CurrentPrincipal != null)
                {
                    if (Thread.CurrentPrincipal.IsInRole("Administrador"))
                    {
                        Vista = "VerEditarUsuario";
                    }
                    if (Thread.CurrentPrincipal.IsInRole("Usuario"))
                    {
                        Vista = "VerEditarUsuarioR";
                    }
                }
                clonusuario = new Usuarios(); // Reemplaza Usuario con el tipo real de clonusuario
                clonusuario.IdUsuario = Usuario.IdUsuario;
                clonusuario.NombreUsuario = Usuario.NombreUsuario;
                clonusuario.CorreoElectronico = Usuario.CorreoElectronico;
                clonusuario.Contraseña = Usuario.Contraseña;
                clonusuario.Rol = Usuario.Rol;
                Usuario = clonusuario;
                Actualizar();
            }
        }

        private void CerrarSesion()
        {
            pvm.CerrarSesion();
        }

        private void FiltroBuscadorPeliculas(string busqueda)
        {
            listapeliculasfiltrado.Clear();
            var peliculasEncontradas = from pelicula in ListaPeliculas
                                       where pelicula.Nombre.Contains(busqueda)
                                       select pelicula;

            foreach (var item in peliculasEncontradas)
            {
                listapeliculasfiltrado.Add(item);
            }
        }

        //Películas
        public void VerRegistrarPelicula()
        {
            Error = "";
            pelicula = new();
            Vista = "VerAgregarPelicula";
            Actualizar();
        }

        private void RegistrarPelicula()
        {
            if (pelicula != null)
            {
                if (catalogop.Validar(pelicula, out List<string> Errores))
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

        private void VerEliminarPelicula()
        {
            Vista = "VerEliminarPelicula";
            Actualizar();
        }

        private void EliminarPelicula()
        {
            if (pelicula is not null)
            {
                catalogop.Eliminar(pelicula);
                ActualizarBD();
                Regresar();
            }
        }

        private void VerEditarPelicula()
        {
            if (pelicula != null)
            {
                Vista = "VerEditarPelicula";
                clonpelicula = new()
                {
                    DuracionMinutos = pelicula.DuracionMinutos,
                    FechaLanzamiento = pelicula.FechaLanzamiento,
                    Genero = pelicula.Genero,
                    IdPelicula = pelicula.IdPelicula,
                    Nombre = pelicula.Nombre,
                    Plataformas = pelicula.Plataformas,
                    Puntuacion = pelicula.Puntuacion,
                    Reseñas = pelicula.Reseñas,
                    Sinopsis = pelicula.Sinopsis,
                    Urlposter = pelicula.Urlposter
                };
                pelicula = clonpelicula;
                Actualizar();
            }
        }

        private void EditarPelicula()
        {

            var peliculaexistente = catalogop.GetPeliculaXID(pelicula.IdPelicula);
            if (peliculaexistente != null)
            {
                if (catalogop.Validar(pelicula, out List<string> Errores))
                {
                    peliculaexistente.DuracionMinutos = pelicula.DuracionMinutos;
                    peliculaexistente.IdPelicula = pelicula.IdPelicula;
                    peliculaexistente.Urlposter = pelicula.Urlposter;
                    pelicula.Sinopsis = pelicula.Sinopsis;
                    peliculaexistente.FechaLanzamiento = pelicula.FechaLanzamiento;
                    peliculaexistente.Genero = pelicula.Genero;
                    peliculaexistente.Nombre = pelicula.Nombre;
                    peliculaexistente.Plataformas = pelicula.Plataformas;
                    peliculaexistente.Puntuacion = pelicula.Puntuacion;
                    peliculaexistente.Sinopsis = pelicula.Sinopsis;

                    catalogop.Editar(peliculaexistente);
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

        private void CancelarEdicionPelicula()
        {
            Vista = "VerPeliculaR";
            Actualizar();
        }

        //IndexNavigation
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

        private void VerPerfilUsuario()
        {
            if (Thread.CurrentPrincipal != null)
            {
                if (Thread.CurrentPrincipal.IsInRole("Administrador"))
                {
                    Vista = "VerUsuario";
                    ActualizarReseñas();
                }
                if (Thread.CurrentPrincipal.IsInRole("Usuario"))
                {
                    Vista = "VerUsuarioR";
                }
            }
            Actualizar();
        }

        private void Regresar()
        {
            Vista = "VerInicio";
            Actualizar();
        }

        //Usuarios
        private void VerEliminarUsuario(Usuarios u)
        {
            Usuario = u;
            var temp = catalogou.GetUsuarioID(u.IdUsuario);
            if (temp is not null)
            {
                Usuario = temp;
                Vista = "VerEliminarUsuario";
                Actualizar();
            }
        }

        private void EliminarUsuario()
        {
            if (Usuario != null)
            {
                catalogou.Eliminar(Usuario);
                Usuario = Application.Current.Properties["UsuarioActual"] as Usuarios;
                ActualizarUsuarios();
                Regresar();
            }
        }

        //Reseñas
        private void RegistrarReseña()
        {
            Error = "";
            reseña = new();
            Vista = "VerHacerReseña";
            Actualizar();
        }

        private void VerEliminarReseña(int id)
        {
            var temp = catalogoReseñas.GetReseñaXID(id);
            if (temp is not null)
            {
                reseña = temp;
                if (Thread.CurrentPrincipal != null)
                {
                    if (Thread.CurrentPrincipal.IsInRole("Administrador"))
                    {
                        Vista = "VerEliminarReseña";
                        ActualizarReseñas();
                    }
                    if (Thread.CurrentPrincipal.IsInRole("Usuario"))
                    {
                        Vista = "VerEliminarReseñaU";
                    }
                }
                Actualizar();
            }
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

        private void RegresarAReseñasXUsuario()
        {
            Vista = "VerReseñasU";
            Actualizar();
        }

        private void RegresarReseñas()
        {
            Vista = "VerInicioU";
            Actualizar();
        }

        //Actualizar listas
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

        private void ActualizarUsuarios()
        {
            ListaUsuarios.Clear();
            foreach (var item in catalogou.GetAllUsuarios())
            {
                ListaUsuarios.Add(item);
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

        private void Actualizar(string? propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
