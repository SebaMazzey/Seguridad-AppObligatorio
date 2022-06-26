using System.ComponentModel.DataAnnotations;

namespace DemoSeguridad.Models
{
    public class UserRegisterModel
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe agregar su Nombre")]
        public string FirstName { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Debe agregar su Apellido")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Debe ingresar un email")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,20}$", ErrorMessage = "La contraseña debe tener entre 6 y 20 caracteres, contener mayusculas, minúsculas, digitos y caracteres especiales.")]
        public string Password { get; set; }
    }
}