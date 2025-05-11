using Barber.Colocho.Infraestructure.Data.Columns;
using Barber.Colocho.Transversal.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Colocho.Infraestructure.Data.Tables
{
    [Table("Password", Schema = "dbo")]
    public class Password : DefaultColumns
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public Guid Id { get; set; }

        [ForeignKey("User"), Required]
        public Guid UserId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public DateTime CodeExpired { get; set; }

        [Required]
        public TypePlatform TypeCodeGenerate { get; set; }
    }
}
