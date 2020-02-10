using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RudDmytro_INCHKIEV_TEST.Models
{
    public class User:IdentityUser
    {


       [Key]
         public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.]+@[a-zA-Z0-9]+\.[a-zA-Z0-9]+$", ErrorMessage = "Invalid email")]
        [DisplayName("Login (Email)")]
        [MaxLength(50, ErrorMessage = "Too long")]
        public string Login { get; set; }
        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Pass { get; set; }
        [Required]
        [DefaultValue("admin")]
        public string Role { get; set; }
    }
}
