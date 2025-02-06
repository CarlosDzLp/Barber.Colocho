using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Colocho.Entities.Tables
{
    [Table("Favorite", Schema = "dbo")]
    public class Favorite : DefaultColumns
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual Company Company { get; set; }
        public virtual User User { get; set; }
    }
}
