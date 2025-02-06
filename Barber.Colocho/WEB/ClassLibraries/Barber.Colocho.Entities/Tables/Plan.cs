using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Colocho.Entities.Tables
{
    [Table("Plan", Schema = "dbo")]
    public class Plan : DefaultColumns
    {
        public Plan()
        {
            this.Suscription = new HashSet<Suscription>();
        }
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public bool IsFree { get; set; }
        public string SKU { get; set; }
        public int QuantityUser {  get; set; }
        public virtual ICollection<Suscription> Suscription { get; set; }
    }
}
