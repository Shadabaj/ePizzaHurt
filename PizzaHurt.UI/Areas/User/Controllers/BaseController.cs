
using Microsoft.AspNetCore.Mvc;
using PizzaHurt.Models;
using PizzaHurt.UI.Helpers;
using System.Security.Claims;
using System.Text.Json;

namespace PizzaHurt.UI.Areas.User.Controllers
{

    [CustomAuthorize(Roles = "User")]
    [Area("User")]
    public class BaseController : Controller
    {
        public UserModel CurrentUser
        {

            get
            {
                if (User.Claims.Count() > 0)
                {
                    var userdata = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;

                    var user = JsonSerializer.Deserialize<UserModel>(userdata);

                    return user;
                }
                return null;
            }

        }
    }
}

