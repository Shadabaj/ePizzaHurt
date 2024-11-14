using PizzaHurt.Models;
using PizzaHurt.Repositories.Interfaces;
using PizzaHurt.Services.Interface;
using PizzaHut.Core.Entities;

namespace PizzaHurt.Services.Implementation
{
    public class AuthService : IAuthService
    {

       IUserRepository _userRepository;

       public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public bool CreateUser(User user, string roles) 
        {
            return _userRepository.CreateUser(user, roles); 
        }

        public UserModel ValidateUser(string Email, string Password)
        {
            return _userRepository.ValidateUser(Email, Password);
        }

        public bool emailExists(string email)
        {
           return _userRepository.emailExists(email);
        }

        public IEnumerable<TblGender> GetGenders()
        {
            return _userRepository.GetGenders();
        }

        public IEnumerable<string> GetRoles()
        {
          return  _userRepository.GetRoles();
        }

        public UserModel GoogleCheckIn(string Email)
        {
            return _userRepository.GoogleCheckIn(Email);
        }
    }
}
