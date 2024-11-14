using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PizzaHurt.UI.Helpers
{
    public class CustomAuthorize : Attribute, IAuthorizationFilter
    {

        public string Roles { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Checking Authentication
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                if (!context.HttpContext.User.IsInRole(Roles))
                {
                    context.Result = new RedirectToActionResult("UnAuthorized", "Account", new { area = "" });
                }
            }
            else
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "" });
            }
        }
    }
}
