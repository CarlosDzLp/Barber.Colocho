using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Colocho.Entities.Tables
{
    [Table("Address", Schema = "dbo")]
    public class Address : DefaultColumns
    {
        public Address()
        {
            this.Company = new HashSet<Company>();
        }

        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string NumExt { get; set; }
        public string CodePostal { get; set; }
        [ForeignKey("Colony")]
        public int IdColony { get; set; }
        public string Observations { get; set; }
        public bool IsDefault { get; set; }
        public virtual User User { get; set; }
        public virtual Colony Colony { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public virtual ICollection<Company> Company { get; set; }
    }
}
