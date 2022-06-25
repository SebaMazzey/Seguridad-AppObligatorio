using System.ComponentModel.DataAnnotations;

namespace DemoSeguridad.Models
{
    public class UserRegisterModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Debe ingresar un email")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        public string Password { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe agregar su Nombre")]
        public string FirstName { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Debe agregar su Apellido")]
        public string LastName { get; set; }
    }
}