using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Colocho.Entities.Tables
{
    [Table("Suscription", Schema = "dbo")]
    public class Suscription : DefaultColumns
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Plan")]
        public int PlanId { get; set; }
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public DateTime InitSuscription {  get; set; }
        public DateTime FinishSuscription { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public Company Company { get; set; }
        public virtual Plan Plan { get; set; }  
    }
}
