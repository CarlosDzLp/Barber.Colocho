using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Colocho.Models.Address
{
    public class AddAddressModel
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string CodePostal { get; set; }
        public int IdColony { get; set; }
        public string Observations { get; set; }
        public bool IsDefault { get; set; }
        public string NumExt { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
