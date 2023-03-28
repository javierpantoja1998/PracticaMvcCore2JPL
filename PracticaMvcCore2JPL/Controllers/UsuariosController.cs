using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2JPL.Models;
using PracticaMvcCore2JPL.Repositories;
using System.Security.Claims;

namespace PracticaMvcCore2JPL.Controllers
{
    public class UsuariosController : Controller
    {
        private RepositoryUsuarios repo;

        public UsuariosController(RepositoryUsuarios repo)
        {
            this.repo = repo;
        }
        public IActionResult Login()
        {
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> Login(string email, string password)
		{
            Usuario user = this.repo.GetUserByNamePass(email, password);

            if(user != null)
            {
                ClaimsIdentity identity =
                new ClaimsIdentity
                (CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimTypes.Name, ClaimTypes.Role);

                Claim claimEmail = new Claim(ClaimTypes.Name, user.Email);
                identity.AddClaim(claimEmail);

                Claim claimID =
                new Claim("ID", user.IdUsuario.ToString());
                identity.AddClaim(claimID);

                ClaimsPrincipal userPrincipal =
                new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync
                (CookieAuthenticationDefaults.AuthenticationScheme
                , userPrincipal);



                return RedirectToAction("Index", "Libros");
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
		}

		public IActionResult AccesoDenegado()
        {
			return View();
        }
	}
}
