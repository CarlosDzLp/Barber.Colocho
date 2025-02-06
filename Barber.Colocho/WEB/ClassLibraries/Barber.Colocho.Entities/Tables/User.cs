using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Colocho.Entities.Tables
{
    [Table("User", Schema = "dbo")]
    public class User : DefaultColumns
    {
        public User()
        {
            this.Address = new HashSet<Address>();
            this.Code = new HashSet<Code>();
            this.Company = new HashSet<Company>();
            this.RefreshToken = new HashSet<RefreshToken>();
            this.Favorite = new HashSet<Favorite>();
            this.RatingService = new HashSet<RatingService>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Roles")]
        public int RolId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsConfirmed { get; set; }
        public string Image {  get; set; }

        public virtual Roles Roles { get; set; }

        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<Company> Company { get; set; }
        public virtual ICollection<Code> Code { get; set; }
        public virtual ICollection<RefreshToken> RefreshToken { get; set; }
        public virtual ICollection<Favorite> Favorite { get; set; }
        public virtual ICollection<RatingService> RatingService { get; set; }
    }
}
