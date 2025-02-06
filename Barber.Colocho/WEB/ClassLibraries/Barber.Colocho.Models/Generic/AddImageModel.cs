using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Colocho.Models.Generic
{
    public class AddImageModel
    {
        public IFormFile File { get; set; }
    }
}
