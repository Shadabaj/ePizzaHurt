using PizzaHurt.Models;
using PizzaHurt.Repositories.Interfaces;
using PizzaHut.Core;
using PizzaHut.Core.Entities;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace PizzaHurt.Repositories.Implementations
{
    public class UserRepository : Repository<User>,IUserRepository
    {
        public UserRepository(AppDbContext _db) : base(_db) {}



        public bool CreateUser(User user, string roles)
        {
            try
            {
              Role  role = _Db.Roles.Where(r => r.Name == roles).FirstOrDefault();
                if (roles.Any())
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    user.Roles.Add(role);
                    _Db.Users.Add(user);
                    _Db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public bool emailExists(string email)
        {
            User user=_Db.Users.Where(u=>u.Email==email).FirstOrDefault();
            if (user != null)
                return true;
                return false;
        }

     

        public UserModel ValidateUser(string Email, string Password)
        {
            User user=_Db.Users.Include(r=>r.Roles).Where(u=>u.Email==Email).FirstOrDefault();
            if (user!=null)
            {
                bool isVerified = BCrypt.Net.BCrypt.Verify(Password, user.Password);
                if (isVerified)
                {
                    UserModel model = new UserModel
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Roles = user.Roles.Select(s => s.Name).ToArray()

                    };
                    return model;
                }
              
            }
            return null;
        }

        public IEnumerable<string> GetRoles()
        {
            return _Db.Roles.Select(r => r.Name).ToArray();
        }

        public IEnumerable<TblGender> GetGenders()
        {
            return _Db.TblGenders.ToList();
        }

        public UserModel GoogleCheckIn(string Email)
        {
          User user = _Db.Users.Include(r=>r.Roles).Where(u => u.Email == Email).FirstOrDefault();
            if (user != null)
            {

                UserModel model = new UserModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Roles = user.Roles.Select(s => s.Name).ToArray()

                };
                return model;
            }
            return null;
        }
    }
}
