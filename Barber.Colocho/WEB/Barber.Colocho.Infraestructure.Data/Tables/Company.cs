using Barber.Colocho.Infraestructure.Data.Columns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Colocho.Infraestructure.Data.Tables
{
    [Table("Company", Schema = "dbo")]
    public class Company : DefaultColumns
    {
        public Company()
        {
            this.CompanyAddress = new HashSet<CompanyAddress>();
            this.CompanyImage = new HashSet<CompanyImage>();
            this.Service = new HashSet<Service>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string RFC { get; set; }

        [Required, ForeignKey("User")]
        public Guid IdUser { get; set; }

        [Required]
        public User User { get; set; }

        public virtual ICollection<CompanyAddress> CompanyAddress { get; set; }
        
        public virtual ICollection<CompanyImage> CompanyImage { get; set; }
        
        public virtual ICollection<Service> Service { get; set; }
    }
}
