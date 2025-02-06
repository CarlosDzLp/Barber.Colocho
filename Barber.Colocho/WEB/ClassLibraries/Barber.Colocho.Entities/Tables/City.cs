using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Colocho.Entities.Tables
{
    [Table("City", Schema = "dbo")]
    public class City : DefaultColumns
    {
        public City()
        {
            this.Colony = new HashSet<Colony>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("State")]
        public int IdState { get; set; }
        public virtual State State { get; set; }
        public virtual ICollection<Colony> Colony { get; set; }
    }
}
