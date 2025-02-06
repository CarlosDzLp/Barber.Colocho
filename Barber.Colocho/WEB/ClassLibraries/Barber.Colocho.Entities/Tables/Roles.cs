using Barber.Colocho.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Colocho.Entities.Tables
{
    [Table("Roles", Schema = "dbo")]
    public class Roles : DefaultColumns
    {
        public Roles()
        {
            this.User = new HashSet<User>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public RolesEnum RolName { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
