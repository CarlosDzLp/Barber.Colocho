using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Colocho.Entities.Tables
{
    [Table("Service", Schema = "dbo")]
    public class Service : DefaultColumns
    {
        public Service()
        {
            this.ImageService = new HashSet<ImageService>();
            this.RatingService = new HashSet<RatingService>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public double Duration { get; set; }
        public bool IsHomeService { get; set; }
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<ImageService> ImageService { get; set; }
        public virtual ICollection<RatingService> RatingService { get; set; }
    }
}
