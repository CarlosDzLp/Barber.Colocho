using Barber.Colocho.Infraestructure.Data.Columns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Colocho.Infraestructure.Data.Tables
{
    [Table("Image", Schema = "dbo")]
    public class CompanyImage : DefaultColumns
    {
        [Key, Required]
        public Guid Id { get; set; }

        [Required, ForeignKey("Company")]
        public Guid IdCompany { get; set; }

        [Required]
        public Company Company { get; set; }

        public string Path { get; set; }
    }
}
