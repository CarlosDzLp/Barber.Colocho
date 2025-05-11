using Barber.Colocho.Infraestructure.Data.Columns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Colocho.Infraestructure.Data.Tables
{
    [Table("Code",Schema = "dbo")]
    public class Code : DefaultColumns
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity),Required]
        public Guid Id { get; set; }
        
        [Required, ForeignKey("User")]
        public Guid IdUser { get; set; }
        
        [Required]
        public User User { get; set; }
        
        [Required]
        public int Codes { get; set; }
        
        public TypeCode TypeCode { get; set; }
    }
}
