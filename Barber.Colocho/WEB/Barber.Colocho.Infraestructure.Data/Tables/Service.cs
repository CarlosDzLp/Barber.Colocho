using Barber.Colocho.Infraestructure.Data.Columns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Colocho.Infraestructure.Data.Tables
{
    [Table("Service", Schema = "dbo")]
    public class Service : DefaultColumns
    {
        public Service()
        {
            this.ServiceImage = new HashSet<ServiceImage>();
        }

        [Required, Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required, ForeignKey("Company")]
        public Guid IdCompany { get; set; }

        [Required]
        public Company Company { get; set; }

        public virtual ICollection<ServiceImage> ServiceImage { get; set; }
    }
}
