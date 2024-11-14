using Microsoft.AspNetCore.Mvc;
using PizzaHurt.Models;
using System.Security.Claims;
using System.Text.Json;

namespace PizzaHurt.UI.Controllers
{
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
