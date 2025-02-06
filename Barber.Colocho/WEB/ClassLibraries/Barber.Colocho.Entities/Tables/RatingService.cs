using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Colocho.Entities.Tables
{
    [Table("RatingService", Schema = "dbo")]
    public class RatingService : DefaultColumns
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        [ForeignKey("Service")]
        public int ServiceId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual Service Service { get; set; }
        public virtual User User { get; set; }
    }
}
