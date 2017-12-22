using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestCor.Models
{  
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Usuario")]
        public string User { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
    }
}
