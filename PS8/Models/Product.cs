using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PS8.Models
{
    public class Product
    {
        [Display(Name = "Id")]
        [Required]
        public int id { get; set; }
        [Display(Name = "Nazwa produktu")]
        [Required]
        public string name { get; set; }
        [Display(Name = "Cena")]
        [Required]
        public decimal price { get; set; }

    }
}
