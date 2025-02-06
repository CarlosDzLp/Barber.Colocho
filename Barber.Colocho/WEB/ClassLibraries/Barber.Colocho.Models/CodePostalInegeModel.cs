using Barber.Colocho.Models.Generic;

namespace Barber.Colocho.Models
{
    public class CodePostalInegeModel
    {
        public GenericModel State { get; set; }
        public GenericModel City { get; set; }
        public List<GenericModel> ListColony { get; set; }

        public CodePostalInegeModel()
        {
            ListColony = new List<GenericModel>();
        }
    }
}
