namespace Barber.Colocho.Models.Service
{
    public class AddServiceModel
    {
        public string Description { get; set; }
        public double Duration { get; set; }
        public bool IsHomeService { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
