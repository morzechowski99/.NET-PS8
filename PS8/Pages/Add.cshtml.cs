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
    public class AddModel : PageModel
    {
        [BindProperty]
        public Product newProduct { get; set; }
        IProductDB productDB;
        public AddModel(IProductDB _productDB)
        {
            productDB = _productDB;
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            productDB.Add(newProduct);

            return RedirectToPage("Index");
        }
    }
}