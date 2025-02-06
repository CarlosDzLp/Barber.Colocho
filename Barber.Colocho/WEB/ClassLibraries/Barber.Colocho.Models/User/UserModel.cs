using Barber.Colocho.Enums;

namespace Barber.Colocho.Models.User
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Id { get; set; }
        public string LastName { get; set; }
        public RolesEnum RolName { get; set; }
        public string Image { get; set; }
    }
}
