using System.ComponentModel.DataAnnotations;

namespace PizzaHurt.UI.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage ="Enter the Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Enter the Password")]
        public string Password { get; set; }

    }
}
