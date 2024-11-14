

namespace PizzaHurt.Models
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public bool EmailConfirmed { get; set; }

        public DateTime CreatedDate { get; set; }


        public RolesDTO Roles { get; set; }
    }

    public class RolesDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
