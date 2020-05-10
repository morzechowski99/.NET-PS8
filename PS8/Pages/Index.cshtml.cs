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
        IProductDB productDB;
        public IndexModel(IProductDB _productDB)
        {
            productDB = _productDB;
        }
        public void OnGet()
        {
            productList = productDB.List();
        }
        private Product XmlNode2Product(XmlNode node)
        {
            Product p = new Product();
            p.id = int.Parse(node.Attributes["id"].Value);
            p.name = node["name"].InnerText;
            p.price = decimal.Parse(node["price"].InnerText);
            return p;
        }
        public IActionResult OnPostDelete()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                productDB.Delete(id);
            }
            
            return RedirectToPage("Index");
        }
    }
}
