using LogicaNegocio.Excepciones;
using LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Usuario : IValidable
    {
        public int UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Contraseña (No guardarla en texto plano en producción)
        [Required]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "La contraseña debe contener al menos una letra mayúscula, una minúscula, un número y un carácter especial.")]
        public string Password { get; set; }

        [Required]
        public string Rol { get; set; }
        public string FullName { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = true;

        public DateTime? FechaUltimoLogin { get; set; }

        public Usuario() { }

        private void validarMail()
        {
            // Validar formato de email
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(Email))
            {
                throw new UsuarioException("El formato del email no es válido.");
            }
        }

        public void validar()
        {
            if(String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(Password)) 
            {
                throw new UsuarioException("Campos vacíos.");
            }
            validarMail();
        }


        // Falta encriptar la contraseña ingresada por el usuario (!)
        private static string EncriptarPassword(string valor)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(valor));
                StringBuilder passEncriptada = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    passEncriptada.Append(bytes[i].ToString("x2")); // x2 es para guardar carácteres en hexagesimal
                }
                return passEncriptada.ToString();
            }

        }
    }
}
