using Barber.Colocho.Application.DTO.Columns;
using Barber.Colocho.Transversal.Common.Enums;

namespace Barber.Colocho.Application.DTO.User
{
    public class UserApplicationDto : DefaultColumns
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Image { get; set; }
        public TypePlatform TypePlatform { get; set; }
        public TypeRegister TypeRegister { get; set; }
        public string IdTypeRegister { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsPhoneConfirmed { get; set; }
        public bool IsAccept { get; set; }
    }
}
