using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestCor.Models
{
    public class LogModel
    {
        [Display(Name = "Elija fecha")]
        [DataType(DataType.Date)]
        [RequiredAttribute(ErrorMessage = "La fecha es requerida")]
        public DateTime fecha { get; set; }
    }
}