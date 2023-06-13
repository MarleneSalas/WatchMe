﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WatchMe.Models;

namespace WatchMe.Catalogos
{
    public class UsuariosCatalogo
    {
        WatchMeContext contenedor = new();
        public IEnumerable<Usuarios> GetAllUsuarios() 
        {
            return contenedor.Usuarios.OrderBy(x => x.NombreUsuario);
        }

        public Usuarios? GetUsuarioXCorreo(string correo)
        {
            return contenedor.Usuarios.FirstOrDefault(x => x.CorreoElectronico == correo);
        }



        public void Agregar(Usuarios u)
        {
            contenedor.Add(u);
            contenedor.SaveChanges();
        }

        public void Eliminar(Usuarios u)
        {
            contenedor.Remove(u);
            contenedor.SaveChanges();
        }
        public void Editar(Usuarios u)
        {
            contenedor.Update(u);
            contenedor.SaveChanges();
        }


        public bool Validar(Usuarios u, out List<string> Errores)
        {
            Errores = new();
            string correopatron = @"\A(\w+@(outlook|gmail|hotmail|yahoo)\.)(com|mx)\Z";
            string nombrepatron = @"^[a-zA-Z\s]+[^\s]*$";
            if (u.CorreoElectronico != null)
            {
                if (u.CorreoElectronico is not null)
                {
                    if (!Regex.IsMatch(u.CorreoElectronico, correopatron) && u.CorreoElectronico != "")
                        Errores.Add("El correo electronico tiene un formato inválido.");

                    if (u.CorreoElectronico.Length > 60)
                        Errores.Add("El correo electrónico no debe pasar de los 60 caracteres.");
                }

                if (contenedor.Usuarios.Any(x => x.CorreoElectronico == u.CorreoElectronico && x.IdUsuario != u.IdUsuario))
                    Errores.Add("Este correo ya se encuentra registrado.");
            }
            if (string.IsNullOrWhiteSpace(u.CorreoElectronico))
                Errores.Add("Debe escribir el correo electronico.");

            if (u.NombreUsuario != null)
            {
                if (u.NombreUsuario is not null)
                {
                    if (!Regex.IsMatch(u.NombreUsuario, nombrepatron) && u.NombreUsuario != "")
                        Errores.Add("El nombre de usuario solamente debe tener letras.");

                    if (u.NombreUsuario.Length > 25)
                        Errores.Add("El nombre de usuario puede tener como máximo 25 caracteres.");
                }
            }
            if (string.IsNullOrWhiteSpace(u.NombreUsuario))
                Errores.Add("Debe escribir un nombre de usuario.");

            if (u.Contraseña != null)
            {
                if (u.Contraseña.Length > 20)
                    Errores.Add("La contraseña no puede pasar de los 20 caracteres.");
            }
            if (string.IsNullOrWhiteSpace(u.Contraseña))
                Errores.Add("Debe escribir una contraseña.");

            return Errores.Count == 0;

        }
    }
}
