using System.ComponentModel.DataAnnotations;

namespace PizzaHurt.UI.Helpers
{
    public class ValidateCheckBox : ValidationAttribute
    {

        public override bool IsValid(object? value)
        {
            return (bool)value;
        }

    }
}
