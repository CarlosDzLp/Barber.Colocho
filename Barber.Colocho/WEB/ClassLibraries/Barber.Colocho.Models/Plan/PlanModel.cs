namespace Barber.Colocho.Models.Plan
{
    public class PlanModel
    {
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public bool IsFree { get; set; }
        public string SKU { get; set; }
        public int QuantityUser { get; set; }
    }
}
