﻿using EcoplusDoctorSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoplusDoctorSync.Helpers
{
    public static class UserHelper
    {
        public static  List<Usuario> GetUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            Usuario adm = new Usuario { Id = 1, UserName = "tesiadmin", Password = "nautilus", Admin = true };
            Usuario fleury = new Usuario { Id = 2, UserName = "fleury", Password = "tp4859", Admin = false};

            usuarios.Add(adm);
            usuarios.Add(fleury);

            return usuarios;

        }
    }
}
