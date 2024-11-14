using Microsoft.AspNetCore.Mvc.Razor;
using PizzaHurt.Models;
using System.Security.Claims;
using System.Text.Json;

namespace PizzaHurt.UI.Helpers
{
    public abstract class BaseViewPage<T> : RazorPage<T>
    {
        public UserModel CurrentUser
        {
            get
            {
                // Check if user claims exist and that we have at least one claim
                if (User?.Claims != null && User.Claims.Any())
                {
                    // Attempt to find the user data claim
                    var userDataClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData);

                    // Check if the claim is not null before accessing its value
                    if (userDataClaim != null)
                    {
                        // Deserialize the user data safely
                        var user = JsonSerializer.Deserialize<UserModel>(userDataClaim.Value);

                        // Ensure deserialization was successful
                        if (user != null)
                        {
                            return user;
                        }
                    }
                }

                // Return null if no user is found or if deserialization failed
                return null;
            }
        }
    }
}
