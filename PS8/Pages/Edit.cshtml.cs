using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PS8.DAL;
using PS8.Models;

namespace PS8.Pages
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Product product { get; set; }
        IProductDB productDB;
        public EditModel(IProductDB _productDB)
        {
            productDB = _productDB;
        }
        public void OnGet(int id)
        {
            product = productDB.Get(id);
        }
       
        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            productDB.Update(product);
            return RedirectToPage("Index");
        }
        
    }
}