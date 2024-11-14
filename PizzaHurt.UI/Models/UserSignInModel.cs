

using PizzaHurt.UI.Helpers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PizzaHurt.UI.Models
{
    public class UserSignInModel
    {

        [Required(ErrorMessage = "Enter the Email")]
        [EmailAddress(ErrorMessage = "Enter Correct Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Enter the Name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Enter the PassWord")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one numeric digit, and one special character.")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Enter the ConfirmPassword")]
        [Compare("Password", ErrorMessage = "Password Not Matched")]
        public string ConfirmPassword { get; set; }


        [RegularExpression("^[6789]\\d{9}$", ErrorMessage = "Enter Valid Mobile Number")]
        [Required(ErrorMessage = "Enter Valid Contact Number")]
        public string PhoneNumber { get; set; }

        [DisplayName("Gender")]
        [Required(ErrorMessage ="Choose the Gender")]
        public int Gender { get; set; }

        [Display(Name = "I agree to the terms and conditions")]
        [ValidateCheckBox(ErrorMessage = "Accept Terms")]
        public bool AcceptTerms {get;set;}

    }

  


}
