using System.ComponentModel.DataAnnotations;

namespace DemoSeguridad.Models
{
    public class UserLoginModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Debe ingresar un email")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        public string Password { get; set; }
    }
}