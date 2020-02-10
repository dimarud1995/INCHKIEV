using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RudDmytro_INCHKIEV_TEST.Models
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(50)]
        [EmailAddress]
        
        public string login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string pass { get; set; }
    }
}
