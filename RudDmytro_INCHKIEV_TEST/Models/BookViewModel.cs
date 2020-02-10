using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RudDmytro_INCHKIEV_TEST.Models
{
    public class BookViewModel
    {
      
        [Required]
        [DisplayName("Назва")]
        public string title { get; set; }
        [DisplayName("Рік")]
        public string year { get; set; }
        [DisplayName("Автор")]
        public string author { get; set; }
        [DisplayName("Книга")]
               [Required]
       public IFormFile bookFile { get; set; }
    }
}
