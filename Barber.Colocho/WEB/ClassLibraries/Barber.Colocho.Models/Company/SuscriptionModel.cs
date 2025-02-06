using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Colocho.Models.Company
{
    public class SuscriptionModel
    {
        public decimal Price { get; set; }
        public DateTime FinishSuscription { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }
        public int Id { get; set; }
        public DateTime InitSuscription { get; set; }
        public string PlanName { get; set; }
    }
}
