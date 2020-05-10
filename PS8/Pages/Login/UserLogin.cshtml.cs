using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using PS8.DAL;
using PS8.Models;

namespace PS8.Pages.Login
{
    public class UserLoginModel : PageModel
    {
        
        
        [BindProperty]
        public User user { get; set; }
        IUserDB _userDB;
        public UserLoginModel(IUserDB userDB)
        {
            _userDB = userDB;
        }
        private bool ValidateUser(User user)
        {
            List<User> users = _userDB.List();
            User toCheck = users.Find(x => x.userName.Trim(' ') == user.userName);
            if (toCheck == null) return false;
            var passwordHasher = new PasswordHasher<string>();
            if (passwordHasher.VerifyHashedPassword(toCheck.userName,toCheck.password,user.password) ==
                PasswordVerificationResult.Success)
            {
                return true;
            }
            return false;
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = "/index")
        {
            if (ModelState.IsValid)
            {
                if (ValidateUser(user))
                {
                    var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.userName)
                };
                    var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
                    await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity));
                    return Redirect(returnUrl);
                }
            }
            return Page();
        }
    }
}