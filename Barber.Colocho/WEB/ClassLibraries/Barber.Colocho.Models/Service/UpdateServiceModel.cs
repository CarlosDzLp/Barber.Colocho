namespace Barber.Colocho.Models.Service
{
    public class UpdateServiceModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Duration { get; set; }
        public bool IsHomeService { get; set; }
    }
}
