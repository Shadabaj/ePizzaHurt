using PizzaHurt.Models;
using PizzaHut.Core.Entities;


namespace PizzaHurt.Services.Interface
{
    public interface IAuthService 
    {
        bool CreateUser(User user, string roles);
        UserModel ValidateUser(string Email, string Password);
        bool emailExists(string email);
        IEnumerable<string> GetRoles();

        IEnumerable<TblGender> GetGenders();

        UserModel GoogleCheckIn(string Email);

    }
}
