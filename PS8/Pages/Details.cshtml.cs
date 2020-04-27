using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PS8.DAL;
using PS8.Models;

namespace PS8.Pages
{
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public Product product { get; set; }
        IProductDB productDB;
        public DetailsModel(IProductDB _productDB)
        {
            productDB = _productDB;
        }

        public void OnGet(int id)
        {
            product = productDB.Get(id);
        }
    }
}