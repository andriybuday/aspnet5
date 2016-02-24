using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class Stop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Longitude { get; set; }
        public long Latitude { get; set; }
        public DateTime Arrival { get; set; }
        public int Order { get; set; }
    }
}