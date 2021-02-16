using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#region ForSignIn
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
#endregion

namespace testPureLogin.Pages
{
    public class LoginModel : PageModel
    {
        public string Msg = "";

        [BindProperty]
        public string fieldAccount { get; set; }
        [BindProperty]
        public string fieldPwd { get; set; }


        public void OnGet()
        {
            HttpContext.SignOutAsync();
        }

        public void OnPostLogin()
        {

            #region SignIn
            var claims = new List<Claim>
                {
                    //use email or LINE user ID as login name
                    new Claim(ClaimTypes.Name, fieldAccount),
                    //use LINE displayName as FullName
                    new Claim("FullName",fieldAccount),
                    new Claim(ClaimTypes.Role, "nobody"),
                };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
            };

            HttpContext.SignInAsync(
               CookieAuthenticationDefaults.AuthenticationScheme,
               new ClaimsPrincipal(claimsIdentity),
               authProperties);
            #endregion

            Msg = $"±b: {fieldAccount} ±K: {fieldPwd} login:{User.Identity.IsAuthenticated}";

        }


    }
}
