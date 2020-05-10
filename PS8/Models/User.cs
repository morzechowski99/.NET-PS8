using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PS8.Models
{
    public class User
    {
        [Display(Name = "Nazwa użytkownika")]
        [Required]
        public string userName { get; set; }
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        [Required]
        public string password { get; set; }
    }
}
