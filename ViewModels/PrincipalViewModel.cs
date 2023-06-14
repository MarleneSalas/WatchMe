using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchMe.Models;
using WatchMe.Catalogos;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using WatchMe.Views;

namespace WatchMe.ViewModels
{
    public class PrincipalViewModel : INotifyPropertyChanged
    {
        UsuariosCatalogo uscatalogo = new UsuariosCatalogo();

        public Usuarios? Usuario { get;set; }

        public UserControl Vista { get; set; }

        IniciarSesionView view;

        public bool UsConectado => Usuario.IdUsuario != 0;

        public string Error { get; set; } = "";

        public ICommand IniciarSesionCommand { get; set; }

        public ICommand CerrarSesionCommand { get; set; }

        public ICommand VerRegistrarUsuarioCommand { get; set; }

        public ICommand RegistrarUsuarioCommand { get; set; }

        public ICommand RegresarCommand { get; set; }




        public PrincipalViewModel()
        {
            IniciarSesionCommand = new RelayCommand(IniciarSesion);
            CerrarSesionCommand = new RelayCommand(CerrarSesion);
            RegresarCommand = new RelayCommand(Regresar);
            VerRegistrarUsuarioCommand = new RelayCommand(VerRegistrarUsuario);
            RegistrarUsuarioCommand = new RelayCommand(RegistrarUsuario);
            Usuario = new();
            view = new IniciarSesionView()
            {
                DataContext = this
            };
            Vista = view;
            Actualizar();
        }

        private void IniciarSesion()
        {
            if (Usuario != null)
            {
                var inicio = uscatalogo.spIniciarSesion(Usuario.CorreoElectronico, Usuario.Contraseña);
                if (inicio == 1)
                {
                    var usconectado = uscatalogo.GetUsuarioXCorreo(Usuario.CorreoElectronico);
                    Usuario = usconectado;
                    Vista = new IndexUsuarioRegularView();
                }
                else if (inicio == 2)
                {
                    Error = "El usuario no se encontró.";
                }
                else
                {
                    Error = "La contraseña es incorrecta.";
                }
                Actualizar();
            }
        }

        private void CerrarSesion()
        {
            Usuario = new();
            Vista = view;
            Actualizar();
        }

        private void Regresar()
        {
            Usuario = new();
            Vista = view;
            Actualizar();
        }

        private void VerRegistrarUsuario()
        {
            Error = "";
            Usuario = new();
            Vista = new RegistrarLoginView();
            Actualizar();
        }

        private void RegistrarUsuario()
        {
            //uscatalogo.Create(Usuario);
            Usuario = new();
            Vista = view;
            Actualizar();
        }

        void Actualizar(string? propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
