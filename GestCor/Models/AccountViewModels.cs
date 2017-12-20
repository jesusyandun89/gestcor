using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestCor.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Usuario")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }
   
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Usuario")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
    }

}
