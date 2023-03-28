using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PracticaMvcCore2JPL.Filters
{
    public class AuthorizeUsuarios : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //NOS DA IGUAL QUIEN SE HA VALIDADO POR AHORA
            var user = context.HttpContext.User;
            if (user.Identity.IsAuthenticated == false)
            {
                context.Result = this.GetRoute("Libros", "Index");
            }
            else
            {

                context.Result = this.GetRoute("Usuarios", "AccesoDenegado");


            }
        }

        private RedirectToRouteResult GetRoute
        (string controller, string action)
        {
            RouteValueDictionary ruta = new RouteValueDictionary(new
            {
                controller = controller,
                action = action
            });
            RedirectToRouteResult result = new RedirectToRouteResult(ruta);
            return result;
        }


    }
}
