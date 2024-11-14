using PizzaHurt.Models;
using PizzaHut.Core.Entities;


namespace PizzaHurt.Repositories.Interfaces
{
    public interface IUserRepository:IRepository<User>
    {

        bool CreateUser(User user, string roles);


        UserModel ValidateUser(string Email, string Password);


        bool emailExists(string email);


        // IEnumerable<Role> GetRoles();

        IEnumerable<string> GetRoles();

        IEnumerable<TblGender> GetGenders();

        UserModel GoogleCheckIn(string Email);

    }
}
