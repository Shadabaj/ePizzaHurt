using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace PizzaHurt.UI.Helpers
{
    public class CustomExceptionFilter :IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Log the exception if needed
            var exception = context.Exception;

            // Redirect to the Error action in the Admin area
            context.Result = new RedirectToActionResult("Error", "Home", new { area = "" });
            context.ExceptionHandled = true; // Mark the exception as handled
        }
    }
}
