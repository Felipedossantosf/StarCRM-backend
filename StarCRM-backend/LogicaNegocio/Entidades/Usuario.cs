using LogicaNegocio.Excepciones;
using LogicaNegocio.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace LogicaNegocio.Entidades
{

    [Table("Usuario")]
    public class Usuario : IValidable
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Contraseña (No guardarla en texto plano en producción)
        [Required]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]      
        public string Password { get; set; }

        //[NotMapped]
        public string Rol { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }
        public string Cargo { get; set; }

        


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
        //private static string EncriptarPassword(string valor)
        //{
        //    using (SHA256 sha256 = SHA256.Create())
        //    {
        //        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(valor));
        //        StringBuilder passEncriptada = new StringBuilder();
        //        for (int i = 0; i < bytes.Length; i++)
        //        {
        //            passEncriptada.Append(bytes[i].ToString("x2")); // x2 es para guardar carácteres en hexagesimal
        //        }
        //        return passEncriptada.ToString();
        //    }

        //}
        // Encrypt de contraseña
        public string EncriptarPass(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(10));

        }
    }
}
