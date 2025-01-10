﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Usuarios
{
    public class DTOUsuarioRegistro
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cargo { get; set; }
        public string ContraseñaActual { get; set; }

        // id de usuario admin para registro de actividad
        public int usuario_id { get; set; }
    }
}