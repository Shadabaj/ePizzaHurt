using Microsoft.AspNetCore.Mvc;

namespace PizzaHurt.UI.Areas.User.Controllers
{

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
