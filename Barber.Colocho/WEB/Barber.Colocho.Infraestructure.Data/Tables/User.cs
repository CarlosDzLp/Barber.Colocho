using Barber.Colocho.Infraestructure.Data.Columns;
using Barber.Colocho.Transversal.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Colocho.Infraestructure.Data.Tables
{
    [Table("User", Schema = "dbo")]
    public class User : DefaultColumns
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Image {  get; set; }

        [Required]
        public TypePlatform TypePlatform { get; set; }

        [Required]
        public TypeRegister TypeRegister { get; set; }

        public string IdTypeRegister { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public bool IsPhoneConfirmed { get; set; }

        public bool IsAccept { get; set; }
    }
}
