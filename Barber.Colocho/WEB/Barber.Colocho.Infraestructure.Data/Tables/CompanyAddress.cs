using Barber.Colocho.Infraestructure.Data.Columns;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Colocho.Infraestructure.Data.Tables
{
    [Table("CompanyAddress",Schema ="dbo")]
    public class CompanyAddress : DefaultColumns
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        [Required,ForeignKey("Company")]
        public Guid IdCompany { get; set; }

        [Required]
        public Company Company { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string CP { get; set; }

        [Required]
        public string Colony { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public Point Location { get; set; }
    }
}
