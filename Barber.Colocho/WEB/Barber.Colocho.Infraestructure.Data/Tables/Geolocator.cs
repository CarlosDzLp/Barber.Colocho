using Barber.Colocho.Infraestructure.Data.Columns;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Colocho.Infraestructure.Data.Tables
{
    [Table("Geolocator", Schema = "dbo")]
    public class Geolocator : DefaultColumns
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("User"),Required]
        public Guid IdUser { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Point Location { get; set; }
    }
}
