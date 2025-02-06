using Barber.Colocho.Models.Generic;

namespace Barber.Colocho.Models.Service
{
    public class GetServiceModel
    {
        public List<GenericModel> ListImage { get; set; }
        public double Duration { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsHomeService { get; set; }
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }
        public double Rating { get; set; }

        public GetServiceModel()
        {
            ListImage = new List<GenericModel>();
        }
    }
}
