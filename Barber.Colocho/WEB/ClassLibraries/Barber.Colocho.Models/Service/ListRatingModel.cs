using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Colocho.Models.Service
{
    public class ListRatingModel
    {
        public string Comment { get; set; }
        public double Rating { get; set; }
        public int ServiceId { get; set; }
    }
}
