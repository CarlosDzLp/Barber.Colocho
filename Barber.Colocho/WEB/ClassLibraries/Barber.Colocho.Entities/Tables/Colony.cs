using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Colocho.Entities.Tables
{
    [Table("Colony",Schema = "dbo")]
    public class Colony :DefaultColumns
    {
        public Colony()
        {
            this.Address = new HashSet<Address>();
        }

        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CodePostal { get; set; }
        [ForeignKey("City")]
        public int IdCity { get; set; }
        public virtual City City { get; set; }

        public virtual ICollection<Address> Address { get; set; }
    }
}
