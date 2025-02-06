using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Colocho.Entities.Tables
{
    [Table("Company", Schema = "dbo")]
    public class Company : DefaultColumns
    {
        public Company()
        {
            this.Image = new HashSet<Image>();
            this.Service = new HashSet<Service>();
            this.Favorite = new HashSet<Favorite>();
            this.Suscription = new HashSet<Suscription>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? RFC {  get; set; }
        [ForeignKey("User")]
        public int IdUser { get; set; }
        [ForeignKey("Address")]
        public int IdAddress { get; set; }
        public virtual Address Address { get; set; }
        public virtual User User { get; set; }
        public double Rating { get; set; }

        public virtual ICollection<Image> Image { get; set; }
        public virtual ICollection<Service> Service { get; set; }
        public virtual ICollection<Favorite> Favorite { get; set; }
        public virtual ICollection<Suscription> Suscription { get; set; }
    }
}
