namespace Barber.Colocho.Models.Address
{
    public class AddressModel
    {
        public int Id { get; set; }
        public bool IsDefault { get; set; }
        public string Name { get; set; }
        public string NumExt { get; set; }
        public string CodePostal { get; set; }
        public string Street { get; set; }
        public string ColonyName { get; set; }
        public string Observations { get; set; }
        public DateTime Created { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int IdColony { get; set; }
    }
}
