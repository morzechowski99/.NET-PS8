using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PS8.DAL;
using PS8.Models;

namespace PS8.Pages
{
    public class IndexModel : PageModel
    {
        public List<Product> productList = new List<Product>();
        [BindProperty]
        public int id { get; set; }
        [BindProperty(SupportsGet = true)]
        public string message { get; set; }
        IProductDB productDB;
        public IndexModel(IProductDB _productDB)
        {
            productDB = _productDB;
            
        }
        public void OnGet()
        {
            productList = productDB.List();
            
        }
        
        public IActionResult OnPostDelete()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                productDB.Delete(id);
                productList = productDB.List();
                return Page();
            }
            
            return RedirectToPage("Index",new { message = "Musisz sie zalogowac" });
        }
    }
}
