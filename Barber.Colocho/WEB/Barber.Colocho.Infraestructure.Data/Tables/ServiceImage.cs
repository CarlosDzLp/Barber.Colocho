using Barber.Colocho.Infraestructure.Data.Columns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Colocho.Infraestructure.Data.Tables
{
    [Table("ServiceImage", Schema = "dbo")]
    public class ServiceImage : DefaultColumns
    {
        [Key, Required]
        public Guid Id { get; set; }

        [Required, ForeignKey("Service")]
        public Guid IdService { get; set; }

        [Required]
        public Service Service { get; set; }

        public string Path { get; set; }
    }
}
