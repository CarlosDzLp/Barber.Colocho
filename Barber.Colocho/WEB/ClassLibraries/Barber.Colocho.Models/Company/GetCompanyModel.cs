using Barber.Colocho.Models.Address;
using Barber.Colocho.Models.Generic;

namespace Barber.Colocho.Models.Company
{
    public class GetCompanyModel
    {
        public string Name { get; set; }
        public string RFC { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public List<GenericModel> ListImage { get; set; }
        public DateTime Created { get; set; }
        public AddressModel Address { get; set; }
        public double Rating { get; set; }
        public bool IsSuscription { get; set; }
        public SuscriptionModel Suscription { get; set; }

        public GetCompanyModel()
        {
            ListImage = new List<GenericModel>();
        }
    }
}
