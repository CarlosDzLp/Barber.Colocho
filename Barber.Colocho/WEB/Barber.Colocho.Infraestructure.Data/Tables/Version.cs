using Barber.Colocho.Infraestructure.Data.Columns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Colocho.Infraestructure.Data.Tables
{
    [Table("Version", Schema = "dbo")]
    public class Version : DefaultColumns
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public decimal VersionApi { get; set; }
    }
}
