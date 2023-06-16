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
using Microsoft.AspNetCore.Authorization;
using System.Threading;
using System.Windows;
using System.Net.Mail;

namespace WatchMe.ViewModels
{
    public class PrincipalViewModel : INotifyPropertyChanged
    {
        UsuariosCatalogo uscatalogo = new UsuariosCatalogo();

        public Usuarios? Usuario { get; set; }
        PeliculasViewModel peliculasvm { get; set; }

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
                    if (Thread.CurrentPrincipal != null)
                    {
                        Application.Current.Properties["UsuarioActual"] = Usuario;
                        if (Thread.CurrentPrincipal.IsInRole("Administrador"))
                        {
                            AccionesUsuarioAdministrador();
                        }
                        if (Thread.CurrentPrincipal.IsInRole("Usuario"))
                        {
                            AccionesUsuarioComun();
                        }
                    }
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

        [Authorize(Roles = "Administrador")]
        private void AccionesUsuarioAdministrador()
        {
            Vista = new IndexAdministradorView();
        }

        [Authorize(Roles = "Usuario")]
        private void AccionesUsuarioComun()
        {
            Vista = new IndexUsuarioRegularView();
        }

        public void CerrarSesion()
        {
            Usuario = new();
            Vista = view;
            Error = "";
            Actualizar();
        }

        private void Regresar()
        {
            Error = "";
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
            if (Usuario != null)
            {
                if(uscatalogo.Validar(Usuario, out List<string> Errores))
                {
                    uscatalogo.Agregar(Usuario);
                    EnviarCorreo(Usuario);
                    Vista = view;
                    Usuario = new();
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


        public void EnviarCorreo(Usuarios userregistrado)
        {
            try
            {
                MailMessage mail = new()
                {
                    From = new MailAddress("201G0246@rcarbonifera.tecnm.mx", "Registro en WatchMe"),
                    Subject = "¡Bienvenido a WatchMe!"
                };
                mail.IsBodyHtml = true;
                mail.Body = $"<h1 style=\"font-family:Arial\"> ¡Bienvenido al sistema!</h1>\r\n    <h3 style=\"font-family:Arial\">Estimado {userregistrado.NombreUsuario}</h3>\r\n    <p style=\"font-family:Arial\">\r\n        Mediante este correo le informamos que su registro ha sido exitoso. <br /> A partir de ahora puede acceder a su cuenta con las credenciales que ha brindado durante el registro.<br />\r\n        Le recomendamos que revise nuestros Términos y Condiciones de uso y nuestra Política de Privacidad para conocer más sobre cómo se manejan sus datos personales.<br />\r\n        Si tiene alguna pregunta o necesita asistencia adicional, no dude en ponerse en contacto con nuestro equipo de soporte.<br /><br />\r\n        Gracias por unirse a nuestra plataforma.<br /><br />\r\n        Atentamente, Rodrigo Elias Castillo.\r\n    </p>"; //El cuerpo del correo
                mail.Bcc.Add($"{userregistrado.CorreoElectronico}"); 
                SmtpClient cliente = new("smtp.outlook.office365.com");
                cliente.Port = 587;
                cliente.EnableSsl = true;
                cliente.UseDefaultCredentials = false;
                System.Net.NetworkCredential cred = new("201G0246@rcarbonifera.tecnm.mx", "rec2003@7"); //La del usuario a la que le vamos a mandar el correo
                cliente.Credentials = cred;
                cliente.Send(mail);
            }
            catch (Exception)
            {
                Error = "Ha ocurrido un error inesperado. Comuniquese con servicio al cliente de WatchMe.";
            }
        }

        void Actualizar(string? propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
